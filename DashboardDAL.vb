Imports System.Data
Imports System.Data.SqlClient

Public Class DashboardDAL

    Public Function GetTodayAppointmentsCount(Optional branchId As Integer = 0) As Integer
        Return GetScalarCount(
            "SELECT COUNT(*) FROM dbo.Appointments WHERE CAST(AppointmentStart AS DATE) = CAST(SYSUTCDATETIME() AS DATE) AND Status NOT IN (N'Cancelled', N'NoShow')" &
            If(branchId > 0, " AND BranchID = @BranchID", ""),
            branchId)
    End Function

    Public Function GetWeekAppointmentsCount(Optional branchId As Integer = 0) As Integer
        Return GetScalarCount(
            "SELECT COUNT(*) FROM dbo.Appointments WHERE AppointmentStart >= DATEADD(DAY, -6, CAST(SYSUTCDATETIME() AS DATE)) " &
            "AND Status NOT IN (N'Cancelled', N'NoShow')" &
            If(branchId > 0, " AND BranchID = @BranchID", ""),
            branchId)
    End Function

    Public Function GetNewPatientsThisMonth(Optional branchId As Integer = 0) As Integer
        If branchId > 0 Then
            Return GetScalarCount(
                "SELECT COUNT(DISTINCT p.PatientID) FROM dbo.Patients p " &
                "INNER JOIN dbo.Appointments a ON a.PatientID = p.PatientID " &
                "WHERE p.IsDeleted = 0 AND YEAR(p.CreatedAt) = YEAR(SYSUTCDATETIME()) AND MONTH(p.CreatedAt) = MONTH(SYSUTCDATETIME()) " &
                "AND a.BranchID = @BranchID",
                branchId)
        End If
        Return GetScalarCount(
            "SELECT COUNT(*) FROM dbo.Patients WHERE IsDeleted = 0 AND YEAR(CreatedAt) = YEAR(SYSUTCDATETIME()) AND MONTH(CreatedAt) = MONTH(SYSUTCDATETIME())",
            0)
    End Function

    Public Function GetMonthRevenue(Optional branchId As Integer = 0) As Decimal
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT ISNULL(SUM(i.NetAmount), 0) FROM dbo.Invoices i WHERE i.Status <> N'Cancelled'")
                sb.AppendLine("AND YEAR(i.InvoiceDate) = YEAR(SYSUTCDATETIME()) AND MONTH(i.InvoiceDate) = MONTH(SYSUTCDATETIME())")
                If branchId > 0 Then sb.AppendLine("AND i.BranchID = @BranchID")
                Using cmd As New SqlCommand(sb.ToString(), conn)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    conn.Open()
                    Return Convert.ToDecimal(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"DashboardDAL.GetMonthRevenue: {ex.Message}")
            Return 0D
        End Try
    End Function

    Public Function GetCancelledAppointmentsThisMonth(Optional branchId As Integer = 0) As Integer
        Return GetScalarCount(
            "SELECT COUNT(*) FROM dbo.Appointments WHERE Status IN (N'Cancelled', N'NoShow') " &
            "AND YEAR(AppointmentStart) = YEAR(SYSUTCDATETIME()) AND MONTH(AppointmentStart) = MONTH(SYSUTCDATETIME())" &
            If(branchId > 0, " AND BranchID = @BranchID", ""),
            branchId)
    End Function

    Public Function GetTodayAppointments(Optional branchId As Integer = 0, Optional doctorId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT FORMAT(a.AppointmentStart, 'HH:mm') AS AppTime,")
                sb.AppendLine("       p.FirstName + ' ' + p.LastName AS PatientName,")
                sb.AppendLine("       d.FirstName + ' ' + d.LastName AS DoctorName,")
                sb.AppendLine("       b.BranchName, a.Status")
                sb.AppendLine("FROM dbo.Appointments a")
                sb.AppendLine("JOIN dbo.Patients p ON p.PatientID = a.PatientID")
                sb.AppendLine("JOIN dbo.Doctors d ON d.DoctorID = a.DoctorID")
                sb.AppendLine("JOIN dbo.Branches b ON b.BranchID = a.BranchID")
                sb.AppendLine("WHERE CAST(a.AppointmentStart AS DATE) = CAST(SYSUTCDATETIME() AS DATE)")
                sb.AppendLine("AND a.Status NOT IN (N'Cancelled', N'NoShow')")
                If branchId > 0 Then sb.AppendLine("AND a.BranchID = @BranchID")
                If doctorId > 0 Then sb.AppendLine("AND a.DoctorID = @DoctorID")
                sb.AppendLine("ORDER BY a.AppointmentStart")
                Using cmd As New SqlCommand(sb.ToString(), conn)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    If doctorId > 0 Then cmd.Parameters.AddWithValue("@DoctorID", doctorId)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"DashboardDAL.GetTodayAppointments: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    Public Function GetLast7DaysAppointmentCounts(Optional branchId As Integer = 0) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim sb As New System.Text.StringBuilder()
                sb.AppendLine("SELECT CAST(AppointmentStart AS DATE) AS AppDay, COUNT(*) AS AppCount")
                sb.AppendLine("FROM dbo.Appointments")
                sb.AppendLine("WHERE AppointmentStart >= DATEADD(DAY, -6, CAST(SYSUTCDATETIME() AS DATE))")
                sb.AppendLine("AND Status NOT IN (N'Cancelled', N'NoShow')")
                If branchId > 0 Then sb.AppendLine("AND BranchID = @BranchID")
                sb.AppendLine("GROUP BY CAST(AppointmentStart AS DATE)")
                sb.AppendLine("ORDER BY AppDay")
                Using cmd As New SqlCommand(sb.ToString(), conn)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    conn.Open()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"DashboardDAL.GetLast7DaysAppointmentCounts: {ex.Message}")
            Throw
        End Try
        Return dt
    End Function

    Private Function GetScalarCount(query As String, branchId As Integer) As Integer
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Using cmd As New SqlCommand(query, conn)
                    If branchId > 0 Then cmd.Parameters.AddWithValue("@BranchID", branchId)
                    conn.Open()
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"DashboardDAL.GetScalarCount: {ex.Message}")
            Return 0
        End Try
    End Function
End Class
