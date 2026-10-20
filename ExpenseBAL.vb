Imports System.Data

Public Class ExpenseBAL
    Private ReadOnly _dal As New ExpenseDAL()

    Public Function GetAllExpenses() As DataTable
        Return _dal.GetAll()
    End Function

    Public Function GetBranches() As DataTable
        Return _dal.GetBranches()
    End Function

    Public Function GetCategories() As DataTable
        Return _dal.GetCategories()
    End Function

    Public Sub AddExpense(branchId As Integer, categoryId As Integer, amount As Decimal, expenseDate As Date, description As String)
        If branchId <= 0 Then Throw New ArgumentException("اختر الفرع.")
        If categoryId <= 0 Then Throw New ArgumentException("اختر تصنيف المصروف.")
        If amount <= 0D Then Throw New ArgumentException("المبلغ يجب أن يكون أكبر من صفر.")
        If expenseDate.Date > Date.Today Then Throw New ArgumentException("تاريخ المصروف لا يمكن أن يكون في المستقبل.")

        _dal.Insert(branchId, categoryId, amount, expenseDate, If(description, String.Empty).Trim())
        AuditLogDAL.WriteLog("Insert", "Expenses", branchId, amount.ToString("0.00"))
    End Sub
End Class
