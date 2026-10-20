Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports OfficeOpenXml

Public Class frmReportViewer
    Private _reportData As DataTable
    Private _reportTitle As String
    Private _searchType As String = ""
    Private _searchValue As String = ""
    Private _printDoc As PrintDocument
    Private _currentPrintRow As Integer = 0

    Private Shared _pdfBaseFont As iTextSharp.text.pdf.BaseFont = Nothing
    Private Shared Function GetPdfFont(size As Single, Optional bold As Boolean = False) As iTextSharp.text.Font
        If _pdfBaseFont Is Nothing Then
            Try
                _pdfBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont("c:\windows\fonts\arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            Catch
                _pdfBaseFont = iTextSharp.text.pdf.BaseFont.CreateFont("c:\windows\fonts\arial.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            End Try
        End If
        Dim style As Integer = iTextSharp.text.Font.NORMAL
        If bold Then style = iTextSharp.text.Font.BOLD
        Return New iTextSharp.text.Font(_pdfBaseFont, size, style)
    End Function

    Public Sub LoadReport(data As DataTable, title As String, Optional searchType As String = "", Optional searchValue As String = "")
        _reportData = data
        _reportTitle = title
        _searchType = searchType
        _searchValue = searchValue
        lblTitle.Text = title
        dgvReport.DataSource = data
        ApplyGridStyle()
    End Sub

    Private Sub ApplyGridStyle()
        If dgvReport.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In dgvReport.Columns
            col.HeaderText = GetArabicHeader(col.HeaderText)
            col.DefaultCellStyle.Font = New Font("Times New Roman", 11)
            col.HeaderCell.Style.Font = New Font("Times New Roman", 11, FontStyle.Bold)
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next
    End Sub

    Private Shared Function GetArabicHeader(english As String) As String
        Select Case english.ToLower()
            Case "doctorid" : Return "رقم الطبيب"
            Case "firstname" : Return "الاسم الأول"
            Case "lastname" : Return "اسم العائلة"
            Case "phone" : Return "رقم الهاتف"
            Case "email" : Return "البريد الإلكتروني"
            Case "specialty" : Return "التخصص"
            Case "userid" : Return "رقم المستخدم"
            Case "username" : Return "اسم المستخدم"
            Case "fullname" : Return "الاسم الكامل"
            Case "rolename" : Return "الصلاحية"
            Case "isactive" : Return "نشط"
            Case "branchid" : Return "رقم الفرع"
            Case "branchname" : Return "اسم الفرع"
            Case "patientid" : Return "رقم المريض"
            Case "appointmentid" : Return "رقم الموعد"
            Case "appointmentdate" : Return "تاريخ الموعد"
            Case "diagnosis" : Return "التشخيص"
            Case "totalamount" : Return "المبلغ الإجمالي"
            Case "paidamount" : Return "المبلغ المدفوع"
            Case "duedate" : Return "تاريخ الاستحقاق"
            Case "createdat" : Return "تاريخ الإنشاء"
            Case "status" : Return "الحالة"
            Case "quantity" : Return "الكمية"
            Case "price" : Return "السعر"
            Case "description" : Return "الوصف"
            Case "itemname" : Return "اسم الصنف"
            Case "category" : Return "التصنيف"
            Case "date" : Return "التاريخ"
            Case Else : Return english
        End Select
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        If _reportData Is Nothing OrElse _reportData.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim fileName As String = ReportHelper.BuildExportFileName(_reportTitle, _searchType, _searchValue, "pdf")
        Dim fullPath As String = Path.Combine(ReportHelper.GetReportFolder(), fileName)
        Try
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10)
            Using writer As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, New FileStream(fullPath, FileMode.Create))
                pdfDoc.Open()
                Dim titlePar As New iTextSharp.text.Paragraph(_reportTitle, GetPdfFont(18, True))
                titlePar.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                pdfDoc.Add(titlePar)
                pdfDoc.Add(New iTextSharp.text.Paragraph(" "))
                Dim pdfTable As New iTextSharp.text.pdf.PdfPTable(_reportData.Columns.Count)
                For Each col As DataColumn In _reportData.Columns
                    pdfTable.AddCell(New iTextSharp.text.Phrase(GetArabicHeader(col.ColumnName), GetPdfFont(10, True)))
                Next
                For Each row As DataRow In _reportData.Rows
                    For Each col As DataColumn In _reportData.Columns
                        Dim val As String = If(row(col).IsDBNull, "", row(col).ToString())
                        pdfTable.AddCell(New iTextSharp.text.Phrase(val, GetPdfFont(9)))
                    Next
                Next
                pdfDoc.Add(pdfTable)
                pdfDoc.Close()
            End Using
            MessageBox.Show($"تم التصدير إلى PDF بنجاح: {fullPath}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError($"frmReportViewer.ExportPdf: {ex.Message}")
            MessageBox.Show("خطأ في التصدير إلى PDF: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        If _reportData Is Nothing OrElse _reportData.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim fileName As String = ReportHelper.BuildExportFileName(_reportTitle, _searchType, _searchValue, "xlsx")
        Dim fullPath As String = Path.Combine(ReportHelper.GetReportFolder(), fileName)
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Using pck As New ExcelPackage()
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(_reportTitle)
                Dim dt As DataTable = _reportData.Copy()
                For Each col As DataColumn In dt.Columns
                    col.ColumnName = GetArabicHeader(col.ColumnName)
                Next
                ws.Cells("A1").LoadFromDataTable(dt, True)
                If ws.Dimension IsNot Nothing Then
                    ws.Cells(ws.Dimension.Address).AutoFitColumns()
                End If
                pck.SaveAs(New FileInfo(fullPath))
            End Using
            MessageBox.Show($"تم التصدير إلى Excel بنجاح: {fullPath}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError($"frmReportViewer.ExportExcel: {ex.Message}")
            MessageBox.Show("خطأ في التصدير إلى Excel: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If _reportData Is Nothing OrElse _reportData.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        _currentPrintRow = 0
        _printDoc = New PrintDocument()
        AddHandler _printDoc.PrintPage, AddressOf PrintDoc_PrintPage
        _printDoc.Print()
    End Sub

    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click
        If _reportData Is Nothing OrElse _reportData.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للمعاينة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        _currentPrintRow = 0
        _printDoc = New PrintDocument()
        AddHandler _printDoc.PrintPage, AddressOf PrintDoc_PrintPage
        Using preview As New PrintPreviewDialog()
            preview.Document = _printDoc
            preview.WindowState = FormWindowState.Maximized
            preview.ShowDialog()
        End Using
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs)
        Try
            Dim font As New Font("Times New Roman", 10)
            Dim boldFont As New Font("Times New Roman", 10, FontStyle.Bold)
            Dim titleFont As New Font("Times New Roman", 14, FontStyle.Bold)
            Dim left As Single = e.MarginBounds.Left
            Dim top As Single = e.MarginBounds.Top
            Dim width As Single = e.MarginBounds.Width
            Dim rowHeight As Single = font.GetHeight(e.Graphics) + 4
            Dim colWidth As Single = width / _reportData.Columns.Count

            e.Graphics.DrawString(_reportTitle, titleFont, Brushes.Black, left + (width - e.Graphics.MeasureString(_reportTitle, titleFont).Width) / 2, top)
            Dim y As Single = top + rowHeight * 2

            For c As Integer = 0 To _reportData.Columns.Count - 1
                e.Graphics.DrawString(GetArabicHeader(_reportData.Columns(c).ColumnName), boldFont, Brushes.White, left + c * colWidth + 2, y + 2)
                e.Graphics.FillRectangle(Brushes.DimGray, left + c * colWidth, y, colWidth, rowHeight)
                e.Graphics.DrawRectangle(Pens.Black, left + c * colWidth, y, colWidth, rowHeight)
            Next
            y += rowHeight

            Dim linesPerPage As Integer = CInt((e.MarginBounds.Bottom - y) / rowHeight) - 1
            If linesPerPage < 1 Then linesPerPage = 1
            Dim printed As Integer = 0
            While _currentPrintRow < _reportData.Rows.Count AndAlso printed < linesPerPage
                Dim row As DataRow = _reportData.Rows(_currentPrintRow)
                For c As Integer = 0 To _reportData.Columns.Count - 1
                    Dim cellText As String = If(row(c).IsDBNull, "", row(c).ToString())
                    e.Graphics.DrawString(cellText, font, Brushes.Black, left + c * colWidth + 2, y + 2)
                    e.Graphics.DrawRectangle(Pens.LightGray, left + c * colWidth, y, colWidth, rowHeight)
                Next
                _currentPrintRow += 1
                printed += 1
                y += rowHeight
            End While

            e.HasMorePages = (_currentPrintRow < _reportData.Rows.Count)
        Catch ex As Exception
            MessageBox.Show("خطأ في الطباعة: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"frmReportViewer.Print: {ex.Message}")
        End Try
    End Sub

    Private Sub frmReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "عرض التقرير"
        Me.BackColor = Color.FromArgb(240, 244, 250)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
End Class