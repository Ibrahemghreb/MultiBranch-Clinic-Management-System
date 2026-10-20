Imports System.Data
Imports System.Data.SqlClient

Public Class PaymentDAL
    Public Function GetAll() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT p.PaymentID, p.InvoiceID, i.InvoiceNo, " &
                "LTRIM(RTRIM(ISNULL(pt.FirstName, '') + ' ' + ISNULL(pt.LastName, ''))) AS PatientName, " &
                "p.Amount, p.PaymentDate, p.PaymentMethod, p.ReferenceNo " &
                "FROM dbo.Payments p " &
                "INNER JOIN dbo.Invoices i ON i.InvoiceID = p.InvoiceID " &
                "LEFT JOIN dbo.Patients pt ON pt.PatientID = i.PatientID " &
                "ORDER BY p.PaymentDate DESC"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetPayableInvoices() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT i.InvoiceID, i.InvoiceNo, " &
                "LTRIM(RTRIM(ISNULL(p.FirstName, '') + ' ' + ISNULL(p.LastName, ''))) AS PatientName, " &
                "b.BranchName, i.NetAmount, ISNULL(SUM(pay.Amount), 0) AS PaidAmount, " &
                "i.NetAmount - ISNULL(SUM(pay.Amount), 0) AS Balance " &
                "FROM dbo.Invoices i " &
                "INNER JOIN dbo.Patients p ON p.PatientID = i.PatientID " &
                "INNER JOIN dbo.Branches b ON b.BranchID = i.BranchID " &
                "LEFT JOIN dbo.Payments pay ON pay.InvoiceID = i.InvoiceID " &
                "WHERE i.Status <> N'Cancelled' " &
                "GROUP BY i.InvoiceID, i.InvoiceNo, p.FirstName, p.LastName, b.BranchName, i.NetAmount " &
                "HAVING i.NetAmount - ISNULL(SUM(pay.Amount), 0) > 0 " &
                "ORDER BY i.InvoiceDate DESC"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Sub Insert(invoiceId As Integer, amount As Decimal, paymentMethod As String, referenceNo As String)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            conn.Open()
            Using tx As SqlTransaction = conn.BeginTransaction()
                Try
                    Const insertQuery As String =
                        "INSERT INTO dbo.Payments (InvoiceID, Amount, PaymentMethod, ReferenceNo, CreatedBy) " &
                        "VALUES (@InvoiceID, @Amount, @PaymentMethod, @ReferenceNo, @CreatedBy)"
                    Using cmd As New SqlCommand(insertQuery, conn, tx)
                        cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId
                        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount
                        cmd.Parameters("@Amount").Precision = 18
                        cmd.Parameters("@Amount").Scale = 2
                        cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 30).Value = paymentMethod
                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar, 100).Value = If(String.IsNullOrWhiteSpace(referenceNo), CObj(DBNull.Value), referenceNo.Trim())
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = If(SessionManager.UserID > 0, CObj(SessionManager.UserID), DBNull.Value)
                        cmd.ExecuteNonQuery()
                    End Using

                    Const updateStatusQuery As String =
                        "UPDATE i SET Status = CASE WHEN paid.TotalPaid >= i.NetAmount THEN N'Paid' ELSE N'PartiallyPaid' END " &
                        "FROM dbo.Invoices i " &
                        "CROSS APPLY (SELECT ISNULL(SUM(Amount), 0) AS TotalPaid FROM dbo.Payments WHERE InvoiceID = i.InvoiceID) paid " &
                        "WHERE i.InvoiceID = @InvoiceID AND i.Status <> N'Cancelled'"
                    Using cmd As New SqlCommand(updateStatusQuery, conn, tx)
                        cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId
                        cmd.ExecuteNonQuery()
                    End Using

                    tx.Commit()
                Catch
                    tx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub
End Class
