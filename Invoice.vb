Public Class Invoice
    Public Property InvoiceID As Integer
    Public Property InvoiceNo As String
    Public Property PatientID As Integer
    Public Property AppointmentID As Integer?
    Public Property BranchID As Integer
    Public Property InvoiceDate As DateTime
    Public Property SubTotal As Decimal
    Public Property DiscountAmount As Decimal
    Public Property TaxRate As Decimal
    Public Property TaxAmount As Decimal
    Public Property NetAmount As Decimal
    Public Property Status As String
    Public Property Notes As String
End Class
