Imports System.Data.SqlClient

Public Class reportdoctors
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtDoctorid.Enabled = rbdoctorid.Checked
        txtfirstname.Enabled = rbfirstname.Checked
        txtphonedoctor.Enabled = rbphone.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles _
        rbdoctorid.CheckedChanged,
        rbfirstname.CheckedChanged,
        rbphone.CheckedChanged

        UpdateInputControls()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String = "SELECT * FROM Doctors WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            ' حالة البحث حسب معرّف الطبيب
            If rbdoctorid.Checked Then
                If String.IsNullOrEmpty(txtDoctorid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الطبيب")
                    Return
                End If
                conditions.Add("DoctorID = @DoctorID")
                parameters.Add(New SqlParameter("@DoctorID", SqlDbType.Int) With {.Value = Integer.Parse(txtDoctorid.Text)})
                currentSearchType = "DoctorID"
                currentSearchValue = txtDoctorid.Text
            End If

            ' حالة البحث حسب الاسم الأول
            If rbfirstname.Checked Then
                If String.IsNullOrEmpty(txtfirstname.Text) Then
                    MessageBox.Show("الرجاء إدخال الاسم الأول")
                    Return
                End If
                conditions.Add("FirstName LIKE @FirstName")
                parameters.Add(New SqlParameter("@FirstName", SqlDbType.NVarChar) With {.Value = "%" & txtfirstname.Text.Trim() & "%"})
                currentSearchType = "FirstName"
                currentSearchValue = txtfirstname.Text
            End If

            ' حالة البحث حسب رقم الهاتف
            If rbphone.Checked Then
                If String.IsNullOrEmpty(txtphonedoctor.Text) Then
                    MessageBox.Show("الرجاء إدخال رقم الهاتف")
                    Return
                End If
                conditions.Add("Phone LIKE @Phone")
                parameters.Add(New SqlParameter("@Phone", SqlDbType.NVarChar) With {.Value = "%" & txtphonedoctor.Text.Trim() & "%"})
                currentSearchType = "Phone"
                currentSearchValue = txtphonedoctor.Text
            End If

            ' التحقق من وجود شروط بحث
            If conditions.Count = 0 Then
                MessageBox.Show("الرجاء اختيار معيار بحث وإدخال قيمة صحيحة")
                Return
            End If

            Dim fullQuery As String = baseQuery & String.Join(" AND ", conditions)

            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(fullQuery, conn)
                    cmd.Parameters.AddRange(parameters.ToArray())
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using

            btnprintexcel.Enabled = True
            btnprintpdf.Enabled = True

        Catch ex As Exception
            MessageBox.Show("خطأ في البحث: " & ex.Message)
        End Try
    End Sub

    Private Sub btnprintexcel_Click(sender As Object, e As EventArgs) Handles btnprintexcel.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Doctors", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(DataGridView1, fileName)
    End Sub

    Private Sub btnprintpdf_Click(sender As Object, e As EventArgs) Handles btnprintpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Doctors", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(DataGridView1, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        'FrmMain.Show()
        Me.Hide()
    End Sub

    Private Sub reportdoctors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyReportForm(Me, "تقرير الأطباء")
        ReportHelper.AddViewReportButton(Me, DataGridView1, "تقرير الأطباء", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub reportdoctors_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
End Class