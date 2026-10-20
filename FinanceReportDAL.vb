Imports System.Data
Imports System.Data.SqlClient

Public Class FinanceReportDAL
    Public Function GetBranchProfit(startDate As Date, endDate As Date) As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "WITH invoiceTotals AS (" &
                " SELECT BranchID, SUM(NetAmount) AS TotalInvoices FROM dbo.Invoices " &
                " WHERE InvoiceDate >= @StartDate AND InvoiceDate < DATEADD(day, 1, @EndDate) AND Status <> N'Cancelled' GROUP BY BranchID)," &
                " paymentTotals AS (" &
                " SELECT i.BranchID, SUM(p.Amount) AS TotalPaid FROM dbo.Payments p INNER JOIN dbo.Invoices i ON i.InvoiceID = p.InvoiceID " &
                " WHERE p.PaymentDate >= @StartDate AND p.PaymentDate < DATEADD(day, 1, @EndDate) GROUP BY i.BranchID)," &
                " expenseTotals AS (" &
                " SELECT BranchID, SUM(Amount) AS TotalExpenses FROM dbo.Expenses " &
                " WHERE ExpenseDate >= @StartDate AND ExpenseDate < DATEADD(day, 1, @EndDate) GROUP BY BranchID) " &
                "SELECT b.BranchID, b.BranchName, ISNULL(i.TotalInvoices, 0) AS TotalInvoices, ISNULL(p.TotalPaid, 0) AS TotalPaid, " &
                "ISNULL(i.TotalInvoices, 0) - ISNULL(p.TotalPaid, 0) AS Remaining, ISNULL(e.TotalExpenses, 0) AS TotalExpenses, " &
                "ISNULL(p.TotalPaid, 0) - ISNULL(e.TotalExpenses, 0) AS NetProfit " &
                "FROM dbo.Branches b " &
                "LEFT JOIN invoiceTotals i ON i.BranchID = b.BranchID " &
                "LEFT JOIN paymentTotals p ON p.BranchID = b.BranchID " &
                "LEFT JOIN expenseTotals e ON e.BranchID = b.BranchID " &
                "ORDER BY b.BranchName"
            Using cmd As New SqlCommand(query, conn)
                AddDateParams(cmd, startDate, endDate)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetDoctorPerformance(startDate As Date, endDate As Date) As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT d.DoctorID, d.FirstName + ' ' + d.LastName AS DoctorName, COUNT(DISTINCT a.AppointmentID) AS AppointmentCount, " &
                "SUM(CASE WHEN a.Status = N'Completed' THEN 1 ELSE 0 END) AS CompletedCount, " &
                "ISNULL(SUM(i.NetAmount), 0) AS TotalInvoices, ISNULL(SUM(p.Amount), 0) AS TotalPaid " &
                "FROM dbo.Doctors d " &
                "LEFT JOIN dbo.Appointments a ON a.DoctorID = d.DoctorID AND a.AppointmentStart >= @StartDate AND a.AppointmentStart < DATEADD(day, 1, @EndDate) " &
                "LEFT JOIN dbo.Invoices i ON i.AppointmentID = a.AppointmentID AND i.Status <> N'Cancelled' " &
                "LEFT JOIN dbo.Payments p ON p.InvoiceID = i.InvoiceID " &
                "GROUP BY d.DoctorID, d.FirstName, d.LastName " &
                "ORDER BY d.FirstName, d.LastName"
            Using cmd As New SqlCommand(query, conn)
                AddDateParams(cmd, startDate, endDate)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Private Shared Sub AddDateParams(cmd As SqlCommand, startDate As Date, endDate As Date)
        cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date
        cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date
    End Sub
End Class
