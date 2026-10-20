Imports System.Data
Imports System.Data.SqlClient

Public Class InvoiceDAL

    Public Function GetPatients() As List(Of PatientListItem)
        Dim list As New List(Of PatientListItem)()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT PatientID, FirstName + ' ' + LastName AS FullName FROM Patients WHERE IsDeleted = 0 ORDER BY FirstName, LastName"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New PatientListItem(reader.GetInt32(0), reader.GetString(1)))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Dim list As New List(Of PatientListItem)()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT BranchID, BranchName FROM dbo.Branches WHERE IsActive = 1 ORDER BY BranchName"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New PatientListItem(reader.GetInt32(0), reader.GetString(1)))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetAppointmentsByPatient(patientId As Integer) As List(Of PatientListItem)
        Dim list As New List(Of PatientListItem)()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT AppointmentID, AppointmentStart FROM dbo.Appointments " &
                "WHERE PatientID = @PatientID AND Status = N'Scheduled' " &
                "ORDER BY AppointmentStart DESC"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@PatientID", patientId)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim display = "Appt #" & reader.GetInt32(0).ToString() & " - " & reader.GetDateTime(1).ToString("yyyy-MM-dd HH:mm")
                        list.Add(New PatientListItem(reader.GetInt32(0), display))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetAll() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT i.InvoiceID, i.InvoiceNo, i.PatientID, p.FirstName + ' ' + p.LastName AS PatientName, " &
                "i.BranchID, b.BranchName, i.InvoiceDate, i.SubTotal, i.DiscountAmount, i.TaxRate, i.TaxAmount, i.NetAmount, i.Status " &
                "FROM dbo.Invoices i " &
                "INNER JOIN dbo.Patients p ON p.PatientID = i.PatientID " &
                "INNER JOIN dbo.Branches b ON b.BranchID = i.BranchID " &
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

    Public Sub Insert(invoice As Invoice)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Invoices (InvoiceNo, PatientID, AppointmentID, BranchID, InvoiceDate, SubTotal, DiscountAmount, TaxRate, TaxAmount, NetAmount, Status, Notes, CreatedBy) " &
                "VALUES (@InvoiceNo, @PatientID, @AppointmentID, @BranchID, @InvoiceDate, @SubTotal, @DiscountAmount, @TaxRate, @TaxAmount, @NetAmount, @Status, @Notes, @CreatedBy)"
            Using cmd As New SqlCommand(query, conn)
                AddInvoiceParams(cmd, invoice)
                cmd.Parameters.AddWithValue("@CreatedBy", If(SessionManager.UserID > 0, CObj(SessionManager.UserID), DBNull.Value))
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(invoice As Invoice)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Invoices SET InvoiceNo=@InvoiceNo, PatientID=@PatientID, AppointmentID=@AppointmentID, BranchID=@BranchID, " &
                "InvoiceDate=@InvoiceDate, SubTotal=@SubTotal, DiscountAmount=@DiscountAmount, TaxRate=@TaxRate, TaxAmount=@TaxAmount, NetAmount=@NetAmount, Status=@Status, Notes=@Notes " &
                "WHERE InvoiceID=@InvoiceID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID)
                AddInvoiceParams(cmd, invoice)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(invoiceId As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE dbo.Invoices SET Status = N'Cancelled' WHERE InvoiceID = @InvoiceID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceId)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Sub AddInvoiceParams(cmd As SqlCommand, invoice As Invoice)
        cmd.Parameters.AddWithValue("@InvoiceNo", invoice.InvoiceNo)
        cmd.Parameters.AddWithValue("@PatientID", invoice.PatientID)
        cmd.Parameters.AddWithValue("@AppointmentID", If(invoice.AppointmentID.HasValue, CObj(invoice.AppointmentID.Value), DBNull.Value))
        cmd.Parameters.AddWithValue("@BranchID", invoice.BranchID)
        cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate)
        cmd.Parameters.AddWithValue("@SubTotal", invoice.SubTotal)
        cmd.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount)
        cmd.Parameters.AddWithValue("@TaxRate", invoice.TaxRate)
        cmd.Parameters.AddWithValue("@TaxAmount", invoice.TaxAmount)
        cmd.Parameters.AddWithValue("@NetAmount", invoice.NetAmount)
        cmd.Parameters.AddWithValue("@Status", invoice.Status)
        cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrWhiteSpace(invoice.Notes), DBNull.Value, invoice.Notes))
    End Sub
End Class
