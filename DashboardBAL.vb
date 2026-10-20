Imports System.Data

Public Class DashboardSummary
    Public Property TodayAppointments As Integer
    Public Property WeekAppointments As Integer
    Public Property NewPatientsMonth As Integer
    Public Property MonthRevenue As Decimal
    Public Property CancelledMonth As Integer
End Class

Public Class DashboardBAL
    Private ReadOnly _dal As New DashboardDAL()

    Public Function GetSummary(Optional branchId As Integer = 0) As DashboardSummary
        Return New DashboardSummary With {
            .TodayAppointments = _dal.GetTodayAppointmentsCount(branchId),
            .WeekAppointments = _dal.GetWeekAppointmentsCount(branchId),
            .NewPatientsMonth = _dal.GetNewPatientsThisMonth(branchId),
            .MonthRevenue = _dal.GetMonthRevenue(branchId),
            .CancelledMonth = _dal.GetCancelledAppointmentsThisMonth(branchId)
        }
    End Function

    Public Function GetTodayAppointments(Optional branchId As Integer = 0, Optional doctorId As Integer = 0) As DataTable
        Return _dal.GetTodayAppointments(branchId, doctorId)
    End Function

    Public Function GetLast7DaysCounts(Optional branchId As Integer = 0) As DataTable
        Return _dal.GetLast7DaysAppointmentCounts(branchId)
    End Function
End Class
