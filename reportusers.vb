Imports System.Data.SqlClient

Public Class reportusers
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtusername.Enabled = Rbusername.Checked
        txtuserid.Enabled = rbuserid.Checked
        txtbranchid.Enabled = rbbranchid.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles _
        Rbusername.CheckedChanged,
        rbuserid.CheckedChanged,
        rbbranchid.CheckedChanged

        UpdateInputControls()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String = "SELECT * FROM Users WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            ' حالة البحث حسب اسم المستخدم
            If Rbusername.Checked Then
                If String.IsNullOrEmpty(txtusername.Text) Then
                    MessageBox.Show("الرجاء إدخال اسم المستخدم")
                    Return
                End If
                conditions.Add("Username LIKE @Username")
                parameters.Add(New SqlParameter("@Username", SqlDbType.NVarChar) With {.Value = "%" & txtusername.Text.Trim() & "%"})
                currentSearchType = "Username"
                currentSearchValue = txtusername.Text
            End If

            ' حالة البحث حسب معرّف المستخدم
            If rbuserid.Checked Then
                If String.IsNullOrEmpty(txtuserid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف المستخدم")
                    Return
                End If
                conditions.Add("UserID = @UserID")
                parameters.Add(New SqlParameter("@UserID", SqlDbType.Int) With {.Value = Integer.Parse(txtuserid.Text)})
                currentSearchType = "UserID"
                currentSearchValue = txtuserid.Text
            End If

            ' حالة البحث حسب معرّف الفرع
            If rbbranchid.Checked Then
                If String.IsNullOrEmpty(txtbranchid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الفرع")
                    Return
                End If
                conditions.Add("BranchID = @BranchID")
                parameters.Add(New SqlParameter("@BranchID", SqlDbType.Int) With {.Value = Integer.Parse(txtbranchid.Text)})
                currentSearchType = "BranchID"
                currentSearchValue = txtbranchid.Text
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
        Dim fileName As String = ReportHelper.BuildExportFileName("Users", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(DataGridView1, fileName)
    End Sub

    Private Sub btnprintpdf_Click(sender As Object, e As EventArgs) Handles btnprintpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Users", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(DataGridView1, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        'FrmMain.Show()
        Me.Hide()
    End Sub

    Private Sub reportusers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyReportForm(Me, "تقرير المستخدمين")
        ReportHelper.AddViewReportButton(Me, DataGridView1, "تقرير المستخدمين", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub reportusers_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
End Class