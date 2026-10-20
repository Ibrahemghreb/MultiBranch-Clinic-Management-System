Imports System.Data

Public Class PaymentBAL
    Private ReadOnly _dal As New PaymentDAL()

    Public Function GetAllPayments() As DataTable
        Return _dal.GetAll()
    End Function

    Public Function GetPayableInvoices() As DataTable
        Return _dal.GetPayableInvoices()
    End Function

    Public Sub AddPayment(invoiceId As Integer, amount As Decimal, paymentMethod As String, referenceNo As String)
        If invoiceId <= 0 Then Throw New ArgumentException("اختر الفاتورة.")
        If amount <= 0D Then Throw New ArgumentException("المبلغ يجب أن يكون أكبر من صفر.")
        If String.IsNullOrWhiteSpace(paymentMethod) Then paymentMethod = "Cash"

        _dal.Insert(invoiceId, amount, paymentMethod.Trim(), If(referenceNo, String.Empty).Trim())
        AuditLogDAL.WriteLog("Insert", "Payments", invoiceId, amount.ToString("0.00"))
    End Sub
End Class
