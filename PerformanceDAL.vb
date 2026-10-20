Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' طبقة الوصول للبيانات - تقويم الأداء الشهري
''' </summary>
Public Class PerformanceDAL

    ''' <summary>
    ''' يجلب عدد المواعيد لكل يوم في شهر معين مع فلترة اختيارية بالفرع والطبيب
    ''' </summary>
    Public Function GetMonthlyAppointmentCounts(year As Integer,
                                                month As Integer,
                                                Optional branchId As Integer = 0,
                                                Optional doctorId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT CAST(AppointmentStart AS DATE) AS AppDay,")
                sb.AppendLine("       COUNT(*) AS AppCount")
                sb.AppendLine("FROM   dbo.Appointments")
                sb.AppendLine("WHERE  YEAR(AppointmentStart)  = @Year")
                sb.AppendLine("  AND  MONTH(AppointmentStart) = @Month")
                sb.AppendLine("  AND  Status NOT IN (N'Cancelled', N'NoShow')")
                If branchId > 0 Then sb.AppendLine("  AND  BranchID = @BranchID")
                If doctorId > 0 Then sb.AppendLine("  AND  DoctorID = @DoctorID")
                sb.AppendLine("GROUP  BY CAST(AppointmentStart AS DATE)")
                sb.AppendLine("ORDER  BY AppDay")

                Using cmd As New SqlCommand(sb.ToString(), conn)
                    cmd.Parameters.AddWithValue("@Year", year)
                    cmd.Parameters.AddWithValue("@Month", month)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    If doctorId > 0 Then cmd.Parameters.AddWithValue("@DoctorID", doctorId)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetMonthlyAppointmentCounts: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يجلب مواعيد يوم محدد مع اسم المريض والوقت للعرض في القائمة الجانبية
    ''' </summary>
    Public Function GetDayAppointments(targetDate As Date,
                                       Optional branchId As Integer = 0,
                                       Optional doctorId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT FORMAT(a.AppointmentStart, 'HH:mm') AS AppTime,")
                sb.AppendLine("       p.FirstName + ' ' + p.LastName            AS PatientName,")
                sb.AppendLine("       a.Status,")
                sb.AppendLine("       a.AppointmentID")
                sb.AppendLine("FROM   dbo.Appointments a")
                sb.AppendLine("JOIN   dbo.Patients     p ON p.PatientID = a.PatientID")
                sb.AppendLine("WHERE  CAST(a.AppointmentStart AS DATE) = @TargetDate")
                sb.AppendLine("  AND  a.Status NOT IN (N'Cancelled', N'NoShow')")
                If branchId > 0 Then sb.AppendLine("  AND  a.BranchID = @BranchID")
                If doctorId > 0 Then sb.AppendLine("  AND  a.DoctorID = @DoctorID")
                sb.AppendLine("ORDER  BY a.AppointmentStart")

                Using cmd As New SqlCommand(sb.ToString(), conn)
                    cmd.Parameters.AddWithValue("@TargetDate", targetDate.Date)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    If doctorId > 0 Then cmd.Parameters.AddWithValue("@DoctorID", doctorId)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetDayAppointments: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يجلب قائمة الفروع النشطة للـ ComboBox
    ''' </summary>
    Public Function GetActiveBranches() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim query As String =
                    "SELECT BranchID, BranchName FROM dbo.Branches " &
                    "WHERE IsActive = 1 ORDER BY BranchName"
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetActiveBranches: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يجلب قائمة الأطباء النشطين للـ ComboBox (مع فلترة اختيارية بالفرع)
    ''' </summary>
    Public Function GetActiveDoctors(Optional branchId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT d.DoctorID,")
                sb.AppendLine("       d.FirstName + ' ' + d.LastName AS DoctorName")
                sb.AppendLine("FROM   dbo.Doctors d")
                If branchId > 0 Then
                    sb.AppendLine("JOIN dbo.DoctorBranches db ON db.DoctorID = d.DoctorID AND db.BranchID = @BranchID")
                End If
                sb.AppendLine("WHERE  d.IsActive = 1")
                sb.AppendLine("ORDER  BY d.FirstName, d.LastName")

                Using cmd As New SqlCommand(sb.ToString(), conn)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetActiveDoctors: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' يجلب إجمالي إحصائيات الشهر (مواعيد، مرضى، إيرادات)
    ''' </summary>
    Public Function GetMonthSummary(year As Integer, month As Integer,
                                    Optional branchId As Integer = 0,
                                    Optional doctorId As Integer = 0) As DataRow
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT COUNT(DISTINCT a.AppointmentID)  AS TotalAppointments,")
                sb.AppendLine("       COUNT(DISTINCT a.PatientID)       AS TotalPatients,")
                sb.AppendLine("       ISNULL(SUM(i.NetAmount), 0)     AS TotalRevenue")
                sb.AppendLine("FROM   dbo.Appointments a")
                sb.AppendLine("LEFT JOIN dbo.Invoices  i ON i.AppointmentID = a.AppointmentID")
                sb.AppendLine("                         AND i.Status <> N'Cancelled'")
                sb.AppendLine("WHERE  YEAR(a.AppointmentStart)  = @Year")
                sb.AppendLine("  AND  MONTH(a.AppointmentStart) = @Month")
                sb.AppendLine("  AND  a.Status NOT IN (N'Cancelled', N'NoShow')")
                If branchId > 0 Then sb.AppendLine("  AND  a.BranchID = @BranchID")
                If doctorId > 0 Then sb.AppendLine("  AND  a.DoctorID = @DoctorID")

                Using cmd As New SqlCommand(sb.ToString(), conn)
                    cmd.Parameters.AddWithValue("@Year", year)
                    cmd.Parameters.AddWithValue("@Month", month)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    If doctorId > 0 Then cmd.Parameters.AddWithValue("@DoctorID", doctorId)
                    conn.Open()
                    Dim dt As New DataTable()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                    If dt.Rows.Count > 0 Then Return dt.Rows(0)
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetMonthSummary: {ex.Message}")
            Throw
        End Try
        Return Nothing
    End Function

    ' مطلوبة من AdminDashboardPanel
    Public Function GetTotalPatientsCount(Optional branchId As Integer = 0) As Integer
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sql As String
                If branchId > 0 Then
                    sql = "SELECT COUNT(DISTINCT p.PatientID) FROM dbo.Patients p " &
                          "INNER JOIN dbo.Appointments a ON a.PatientID = p.PatientID " &
                          "WHERE p.IsDeleted = 0 AND a.BranchID = @BranchID"
                Else
                    sql = "SELECT COUNT(*) FROM dbo.Patients WHERE IsDeleted = 0"
                End If
                Using cmd As New SqlCommand(sql, conn)
                    If branchId > 0 Then cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetTotalPatientsCount: {ex.Message}")
            Return 0
        End Try
    End Function

    Public Function GetMonthRevenue(branchId As Integer, year As Integer, month As Integer) As Decimal
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sql = "SELECT ISNULL(SUM(NetAmount),0) FROM dbo.Invoices " &
                          "WHERE Status <> N'Cancelled' AND YEAR(InvoiceDate)=@Year AND MONTH(InvoiceDate)=@Month" &
                          If(branchId > 0, " AND BranchID=@BranchID", "")
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year
                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month
                    If branchId > 0 Then cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                    conn.Open()
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PerformanceDAL.GetMonthRevenue(m): {ex.Message}")
            Return 0D
        End Try
    End Function

End Class
