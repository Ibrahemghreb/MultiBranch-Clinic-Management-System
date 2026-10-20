Imports System.Data.SqlClient

Public Class ReportBranches
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtBranchid.Enabled = rbBranchID.Checked
        txtbranchname.Enabled = rbbranchname.Checked
        txtcity.Enabled = rbcity.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles _
        rbBranchID.CheckedChanged,
        rbbranchname.CheckedChanged,
        rbcity.CheckedChanged

        UpdateInputControls()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String = "SELECT * FROM Branches WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            ' حالة البحث حسب معرّف الفرع
            If rbBranchID.Checked Then
                If String.IsNullOrEmpty(txtBranchid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الفرع")
                    Return
                End If
                conditions.Add("BranchID = @BranchID")
                parameters.Add(New SqlParameter("@BranchID", SqlDbType.Int) With {.Value = Integer.Parse(txtBranchid.Text)})
                currentSearchType = "BranchID"
                currentSearchValue = txtBranchid.Text
            End If

            ' حالة البحث حسب اسم الفرع
            If rbbranchname.Checked Then
                If String.IsNullOrEmpty(txtbranchname.Text) Then
                    MessageBox.Show("الرجاء إدخال اسم الفرع")
                    Return
                End If
                conditions.Add("BranchName LIKE @BranchName")
                parameters.Add(New SqlParameter("@BranchName", SqlDbType.NVarChar) With {.Value = "%" & txtbranchname.Text.Trim() & "%"})
                currentSearchType = "BranchName"
                currentSearchValue = txtbranchname.Text
            End If

            ' حالة البحث حسب المدينة (يفترض أن المدينة مخزنة في Address)
            If rbcity.Checked Then
                If String.IsNullOrEmpty(txtcity.Text) Then
                    MessageBox.Show("الرجاء إدخال اسم المدينة")
                    Return
                End If
                conditions.Add("Address LIKE @City")
                parameters.Add(New SqlParameter("@City", SqlDbType.NVarChar) With {.Value = "%" & txtcity.Text.Trim() & "%"})
                currentSearchType = "City"
                currentSearchValue = txtcity.Text
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
        Dim fileName As String = ReportHelper.BuildExportFileName("Branches", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(DataGridView1, fileName)
    End Sub

    Private Sub btnprintpdf_Click(sender As Object, e As EventArgs) Handles btnprintpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Branches", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(DataGridView1, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        'FrmMain.Show()
        Me.Hide()
    End Sub

    Private Sub ReportBranches_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyReportForm(Me, "تقرير الفروع")
        ReportHelper.AddViewReportButton(Me, DataGridView1, "تقرير الفروع", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub ReportBranches_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
End Class