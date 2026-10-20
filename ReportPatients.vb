Imports System.Data.SqlClient

Public Class ReportPatients
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtPatientD.Enabled = rbPatientD.Checked
        txtfirstname.Enabled = rbfirstname.Checked
        txtphonedoctor.Enabled = rbphone.Checked
        dateofbirthpatient.Enabled = rbDateofBirth.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles _
        rbPatientD.CheckedChanged,
        rbfirstname.CheckedChanged,
        rbphone.CheckedChanged,
        rbDateofBirth.CheckedChanged

        UpdateInputControls()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String = "SELECT * FROM Patients WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            If rbPatientD.Checked Then
                If String.IsNullOrEmpty(txtPatientD.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف المريض")
                    Return
                End If
                conditions.Add("PatientID = @PatientID")
                parameters.Add(New SqlParameter("@PatientID", SqlDbType.Int) With {.Value = Integer.Parse(txtPatientD.Text)})
                currentSearchType = "PatientID"
                currentSearchValue = txtPatientD.Text
            End If

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

            If rbDateofBirth.Checked Then
                Dim selectedDate As DateTime = dateofbirthpatient.Value.Date
                conditions.Add("DateOfBirth = @DateOfBirth")
                parameters.Add(New SqlParameter("@DateOfBirth", SqlDbType.Date) With {.Value = selectedDate})
                currentSearchType = "DateOfBirth"
                currentSearchValue = selectedDate.ToString("yyyyMMdd")
            End If

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
            Logger.LogError($"ReportPatients.btnsearch: {ex.Message}")
            MessageBox.Show("خطأ في البحث: " & ex.Message)
        End Try
    End Sub

    Private Sub btnprintexcel_Click(sender As Object, e As EventArgs) Handles btnprintexcel.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Patients", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(DataGridView1, fileName)
    End Sub

    Private Sub btnprintpdf_Click(sender As Object, e As EventArgs) Handles btnprintpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Patients", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(DataGridView1, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Hide()
    End Sub

    Private Sub ReportPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyReportForm(Me, "تقرير المرضى")
        ReportHelper.AddViewReportButton(Me, DataGridView1, "تقرير المرضى", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub ReportPatients_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
End Class
