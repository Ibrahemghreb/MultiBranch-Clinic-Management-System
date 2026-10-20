Imports System.Data

Public Class FinanceReportBAL
    Private ReadOnly _dal As New FinanceReportDAL()

    Public Function GetBranchProfit(startDate As Date, endDate As Date) As DataTable
        ValidatePeriod(startDate, endDate)
        Return _dal.GetBranchProfit(startDate, endDate)
    End Function

    Public Function GetDoctorPerformance(startDate As Date, endDate As Date) As DataTable
        ValidatePeriod(startDate, endDate)
        Return _dal.GetDoctorPerformance(startDate, endDate)
    End Function

    Private Shared Sub ValidatePeriod(startDate As Date, endDate As Date)
        If startDate.Date > endDate.Date Then Throw New ArgumentException("تاريخ البداية يجب أن يكون قبل تاريخ النهاية.")
    End Sub
End Class
