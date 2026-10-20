Imports System.Data
Imports System.IO
Imports System.Windows.Forms
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports OfficeOpenXml

''' <summary>مساعد مشترك لتصدير التقارير PDF و Excel.</summary>
Public NotInheritable Class ReportHelper
    Private Sub New()
    End Sub

    Public Shared Function GetReportFolder() As String
        Dim folderPath As String = Path.Combine(Application.StartupPath, "Report")
        Directory.CreateDirectory(folderPath)
        Return folderPath
    End Function

    Public Shared Sub ExportDataGridViewToPdf(dgv As DataGridView, fileName As String)
        If dgv Is Nothing OrElse dgv.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim fullPath As String = Path.Combine(GetReportFolder(), fileName)
            Dim pdfDoc As New Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
            Using writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(fullPath, FileMode.Create))
                pdfDoc.Open()
                Dim table As New PdfPTable(dgv.Columns.Count)

                For Each column As DataGridViewColumn In dgv.Columns
                    table.AddCell(New Phrase(column.HeaderText))
                Next

                For Each row As DataGridViewRow In dgv.Rows
                    If Not row.IsNewRow Then
                        For Each cell As DataGridViewCell In row.Cells
                            Dim cellValue As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                            table.AddCell(New Phrase(cellValue))
                        Next
                    End If
                Next

                pdfDoc.Add(table)
                pdfDoc.Close()
            End Using

            MessageBox.Show($"تم التصدير إلى PDF بنجاح:{Environment.NewLine}{fullPath}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError($"ReportHelper.ExportDataGridViewToPdf: {ex.Message}")
            MessageBox.Show("خطأ في التصدير إلى PDF: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub ExportDataGridViewToExcel(dgv As DataGridView, fileName As String)
        Dim dt As DataTable = TryCast(dgv.DataSource, DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim fullPath As String = Path.Combine(GetReportFolder(), fileName)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Using pck As New ExcelPackage()
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("التقرير")
                ws.Cells("A1").LoadFromDataTable(dt, True)
                pck.SaveAs(New FileInfo(fullPath))
            End Using

            MessageBox.Show($"تم التصدير إلى Excel بنجاح:{Environment.NewLine}{fullPath}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError($"ReportHelper.ExportDataGridViewToExcel: {ex.Message}")
            MessageBox.Show("خطأ في التصدير إلى Excel: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function BuildExportFileName(prefix As String, searchType As String, searchValue As String, extension As String) As String
        Dim safeValue As String = If(String.IsNullOrWhiteSpace(searchValue), "All", searchValue)
        For Each c As Char In Path.GetInvalidFileNameChars()
            safeValue = safeValue.Replace(c, "_"c)
        Next
        Return $"{prefix}_{searchType}_{safeValue}.{extension}"
    End Function

    Public Shared Sub ShowReportViewer(dt As DataTable, reportTitle As String, Optional searchType As String = "", Optional searchValue As String = "")
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للعرض. قم بالبحث أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Using viewer As New frmReportViewer()
            viewer.LoadReport(dt, reportTitle, searchType, searchValue)
            viewer.ShowDialog()
        End Using
    End Sub

    Private Class ReportButtonHandler
        Private _dgv As DataGridView
        Private _reportTitle As String
        Private _getSearchType As Func(Of String)
        Private _getSearchValue As Func(Of String)
        Public Sub New(dgv As DataGridView, reportTitle As String, getSearchType As Func(Of String), getSearchValue As Func(Of String))
            _dgv = dgv
            _reportTitle = reportTitle
            _getSearchType = getSearchType
            _getSearchValue = getSearchValue
        End Sub
        Public Sub OnClick(sender As Object, e As EventArgs)
            Try
                Dim dt As DataTable = TryCast(_dgv.DataSource, DataTable)
                If dt Is Nothing Then
                    MessageBox.Show("DataSource is Nothing.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                If dt.Rows.Count = 0 Then
                    MessageBox.Show("Rows.Count = 0.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                ShowReportViewer(dt, _reportTitle, _getSearchType(), _getSearchValue())
            Catch ex As Exception
                MessageBox.Show("Exception: " & ex.ToString(), "Debug", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
    End Class

    Public Shared Sub AddViewReportButton(parentForm As Form, dgv As DataGridView, reportTitle As String, getSearchType As Func(Of String), getSearchValue As Func(Of String))
        Dim btn As New Button()
        btn.Text = "عرض تقرير"
        btn.Font = New System.Drawing.Font("Times New Roman", 11, FontStyle.Bold)
        btn.BackColor = Color.FromArgb(0, 168, 150)
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.Size = New Size(120, 36)
        btn.Location = New Point(parentForm.ClientSize.Width - 130, parentForm.ClientSize.Height - 50)
        btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Dim handler As New ReportButtonHandler(dgv, reportTitle, getSearchType, getSearchValue)
        AddHandler btn.Click, AddressOf handler.OnClick
        parentForm.Controls.Add(btn)
        btn.BringToFront()
    End Sub
End Class
