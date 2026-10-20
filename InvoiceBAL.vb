Public Class InvoiceBAL
    Private ReadOnly _dal As New InvoiceDAL()

    Public Function GetPatients() As List(Of PatientListItem)
        Return _dal.GetPatients()
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Return _dal.GetBranches()
    End Function

    Public Function GetAppointmentsByPatient(patientId As Integer) As List(Of PatientListItem)
        Return _dal.GetAppointmentsByPatient(patientId)
    End Function

    Public Function GetAllInvoices() As DataTable
        Return _dal.GetAll()
    End Function

    Public Sub AddInvoice(invoice As Invoice)
        ValidateInvoice(invoice, False)
        invoice.InvoiceNo = If(String.IsNullOrWhiteSpace(invoice.InvoiceNo), GenerateInvoiceNo(), invoice.InvoiceNo.Trim())
        CalculateAmounts(invoice)
        _dal.Insert(invoice)
        AuditLogDAL.WriteLog("Insert", "Invoices", 0, invoice.InvoiceNo)
    End Sub

    Public Sub UpdateInvoice(invoice As Invoice)
        ValidateInvoice(invoice, True)
        CalculateAmounts(invoice)
        _dal.Update(invoice)
        AuditLogDAL.WriteLog("Update", "Invoices", invoice.InvoiceID, invoice.InvoiceNo)
    End Sub

    Public Sub CancelInvoice(invoiceId As Integer)
        If invoiceId <= 0 Then Throw New ArgumentException("رقم الفاتورة غير صحيح.")
        _dal.Delete(invoiceId)
        AuditLogDAL.WriteLog("Delete", "Invoices", invoiceId, "Cancelled")
    End Sub

    Private Shared Sub ValidateInvoice(invoice As Invoice, requireId As Boolean)
        If invoice Is Nothing Then Throw New ArgumentNullException("invoice")
        If requireId AndAlso invoice.InvoiceID <= 0 Then Throw New ArgumentException("رقم الفاتورة غير صحيح.")
        If invoice.PatientID <= 0 Then Throw New ArgumentException("اختر المريض.")
        If invoice.BranchID <= 0 Then Throw New ArgumentException("اختر الفرع.")
        If invoice.SubTotal < 0 Then Throw New ArgumentException("المبلغ غير صحيح.")
    End Sub

    Private Shared Sub CalculateAmounts(invoice As Invoice)
        If invoice.TaxRate <= 0 Then invoice.TaxRate = 14D
        Dim taxable = Math.Max(0D, invoice.SubTotal - invoice.DiscountAmount)
        invoice.TaxAmount = Math.Round(taxable * invoice.TaxRate / 100D, 2)
        invoice.NetAmount = Math.Round(taxable + invoice.TaxAmount, 2)
        If String.IsNullOrWhiteSpace(invoice.Status) Then invoice.Status = "Open"
    End Sub

    Private Shared Function GenerateInvoiceNo() As String
        Return "INV-" & DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function
End Class
