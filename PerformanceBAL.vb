Imports System.Data

''' <summary>
''' طبقة منطق الأعمال - تقويم الأداء الشهري
''' </summary>
Public Class PerformanceBAL

    Private ReadOnly _dal As New PerformanceDAL()

    ''' <summary>
    ''' يُرجع Dictionary من (يوم → عدد المواعيد) لشهر كامل
    ''' </summary>
    Public Function GetMonthlyCountMap(year As Integer, month As Integer,
                                       branchId As Integer, doctorId As Integer) As Dictionary(Of Integer, Integer)
        Dim map As New Dictionary(Of Integer, Integer)()
        Try
            Dim dt = _dal.GetMonthlyAppointmentCounts(year, month, branchId, doctorId)
            For Each row As DataRow In dt.Rows
                Dim d = CDate(row("AppDay")).Day
                map(d) = CInt(row("AppCount"))
            Next
        Catch ex As Exception
            Logger.LogError($"PerformanceBAL.GetMonthlyCountMap: {ex.Message}")
            Throw
        End Try
        Return map
    End Function

    ''' <summary>
    ''' يُرجع قائمة مواعيد يوم معين مُنسَّقة للعرض
    ''' </summary>
    Public Function GetDayAppointmentList(targetDate As Date,
                                          branchId As Integer,
                                          doctorId As Integer) As List(Of PerformanceAppointmentItem)
        Dim list As New List(Of PerformanceAppointmentItem)()
        Try
            Dim dt = _dal.GetDayAppointments(targetDate, branchId, doctorId)
            For Each row As DataRow In dt.Rows
                list.Add(New PerformanceAppointmentItem With {
                    .AppointmentID = CInt(row("AppointmentID")),
                    .AppTime       = row("AppTime").ToString(),
                    .PatientName   = row("PatientName").ToString(),
                    .Status        = row("Status").ToString()
                })
            Next
        Catch ex As Exception
            Logger.LogError($"PerformanceBAL.GetDayAppointmentList: {ex.Message}")
            Throw
        End Try
        Return list
    End Function

    ''' <summary>
    ''' يجلب الفروع للـ ComboBox مع إضافة "الكل" أولاً
    ''' </summary>
    Public Function GetBranchesForFilter() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("BranchID",   GetType(Integer))
        dt.Columns.Add("BranchName", GetType(String))
        dt.Rows.Add(0, "-- All Branches --")
        Try
            Dim src = _dal.GetActiveBranches()
            For Each row As DataRow In src.Rows
                dt.Rows.Add(CInt(row("BranchID")), row("BranchName").ToString())
            Next
        Catch ex As Exception
            Logger.LogError($"PerformanceBAL.GetBranchesForFilter: {ex.Message}")
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يجلب الأطباء للـ ComboBox مع إضافة "الكل" أولاً
    ''' </summary>
    Public Function GetDoctorsForFilter(Optional branchId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("DoctorID",   GetType(Integer))
        dt.Columns.Add("DoctorName", GetType(String))
        dt.Rows.Add(0, "-- All Doctors --")
        Try
            Dim src = _dal.GetActiveDoctors(branchId)
            For Each row As DataRow In src.Rows
                dt.Rows.Add(CInt(row("DoctorID")), row("DoctorName").ToString())
            Next
        Catch ex As Exception
            Logger.LogError($"PerformanceBAL.GetDoctorsForFilter: {ex.Message}")
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يُرجع ملخص إحصائيات الشهر
    ''' </summary>
    Public Function GetMonthSummary(year As Integer, month As Integer,
                                    branchId As Integer, doctorId As Integer) As PerformanceMonthSummary
        Dim summary As New PerformanceMonthSummary()
        Try
            Dim row = _dal.GetMonthSummary(year, month, branchId, doctorId)
            If row IsNot Nothing Then
                summary.TotalAppointments = CInt(row("TotalAppointments"))
                summary.TotalPatients     = CInt(row("TotalPatients"))
                summary.TotalRevenue      = CDec(row("TotalRevenue"))
            End If
        Catch ex As Exception
            Logger.LogError($"PerformanceBAL.GetMonthSummary: {ex.Message}")
        End Try
        Return summary
    End Function

    ''' <summary>
    ''' يتحقق من صحة نطاق التاريخ
    ''' </summary>
    Public Function IsValidMonth(year As Integer, month As Integer) As Boolean
        Return year >= 2000 AndAlso year <= 2100 AndAlso month >= 1 AndAlso month <= 12
    End Function

End Class

' ─── نماذج البيانات ───────────────────────────────────────────────────────

''' <summary>موعد واحد في قائمة اليوم</summary>
Public Class PerformanceAppointmentItem
    Public Property AppointmentID As Integer
    Public Property AppTime       As String
    Public Property PatientName   As String
    Public Property Status        As String
    Public ReadOnly Property DisplayText As String
        Get
            Return $"{AppTime} - {PatientName}"
        End Get
    End Property
End Class

''' <summary>ملخص إحصائيات الشهر</summary>
Public Class PerformanceMonthSummary
    Public Property TotalAppointments As Integer = 0
    Public Property TotalPatients     As Integer = 0
    Public Property TotalRevenue      As Decimal = 0D
End Class
