Imports System.Data
Imports System.Data.SqlClient

Public Class AppointmentDAL
    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT AppointmentID, BranchID, DoctorID, PatientID, AppointmentStart, AppointmentEnd, Notes, QueueNumber, Status FROM Appointments"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Dim items As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT BranchID, BranchName FROM Branches ORDER BY BranchName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        items.Add(New PatientListItem(reader.GetInt32(0), $"{reader.GetInt32(0)} - {reader.GetString(1)}"))
                    End While
                End Using
            End Using
        End Using

        Return items
    End Function

    Public Function GetDoctors() As List(Of PatientListItem)
        Dim items As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT DoctorID, FirstName, LastName FROM Doctors ORDER BY FirstName, LastName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        items.Add(New PatientListItem(reader.GetInt32(0), $"{reader.GetInt32(0)} - {reader.GetString(1)} {reader.GetString(2)}"))
                    End While
                End Using
            End Using
        End Using

        Return items
    End Function

    Public Function GetPatients() As List(Of PatientListItem)
        Dim items As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT PatientID, FirstName, LastName FROM Patients ORDER BY FirstName, LastName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        items.Add(New PatientListItem(reader.GetInt32(0), $"{reader.GetInt32(0)} - {reader.GetString(1)} {reader.GetString(2)}"))
                    End While
                End Using
            End Using
        End Using

        Return items
    End Function

    Public Function GetAppointmentCountsByDate(periodStart As DateTime, periodEnd As DateTime, Optional branchId As Integer = 0, Optional doctorId As Integer = 0) As Dictionary(Of DateTime, Integer)
        Dim counts As New Dictionary(Of DateTime, Integer)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT CAST(AppointmentStart AS date) AS AppointmentDay, COUNT(*) AS AppointmentCount " &
                                  "FROM Appointments " &
                                  "WHERE AppointmentStart >= @PeriodStart AND AppointmentStart < @PeriodEnd"

            If branchId > 0 Then
                query &= " AND BranchID = @BranchID"
            End If

            If doctorId > 0 Then
                query &= " AND DoctorID = @DoctorID"
            End If

            query &= " GROUP BY CAST(AppointmentStart AS date)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PeriodStart", SqlDbType.DateTime).Value = periodStart.Date
                command.Parameters.Add("@PeriodEnd", SqlDbType.DateTime).Value = periodEnd.Date

                If branchId > 0 Then
                    command.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                End If

                If doctorId > 0 Then
                    command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
                End If

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        counts(Convert.ToDateTime(reader("AppointmentDay")).Date) = CInt(reader("AppointmentCount"))
                    End While
                End Using
            End Using
        End Using

        Return counts
    End Function



    Public Function GetById(appointmentId As Integer) As Appointment
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT AppointmentID, BranchID, DoctorID, PatientID, AppointmentStart, AppointmentEnd, Notes, QueueNumber, Status, CancellationReason FROM Appointments WHERE AppointmentID = @AppointmentID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointmentId
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapAppointment(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function AppointmentExists(patientId As Integer, appointmentStart As DateTime, Optional excludeAppointmentId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM Appointments WHERE PatientID = @PatientID AND AppointmentStart = @AppointmentStart"
            If excludeAppointmentId > 0 Then
                query &= " AND AppointmentID <> @AppointmentID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientId
                command.Parameters.Add("@AppointmentStart", SqlDbType.DateTime).Value = appointmentStart
                If excludeAppointmentId > 0 Then
                    command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = excludeAppointmentId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(appointment As Appointment)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "INSERT INTO Appointments (BranchID, DoctorID, PatientID, AppointmentStart, AppointmentEnd, Notes, QueueNumber, Status, CancellationReason) VALUES (@BranchID, @DoctorID, @PatientID, @AppointmentStart, @AppointmentEnd, @Notes, @QueueNumber, @Status, @CancellationReason)"
            Using command As New SqlCommand(query, connection)
                AddAppointmentParameters(command, appointment, includeId:=False)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(appointment As Appointment)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE Appointments SET BranchID = @BranchID, DoctorID = @DoctorID, PatientID = @PatientID, AppointmentStart = @AppointmentStart, AppointmentEnd = @AppointmentEnd, Notes = @Notes, QueueNumber = @QueueNumber, Status = @Status, CancellationReason = @CancellationReason WHERE AppointmentID = @AppointmentID"
            Using command As New SqlCommand(query, connection)
                AddAppointmentParameters(command, appointment, includeId:=True)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(appointmentId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointmentId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Function GetAppointmentsByDate(
    targetDate As DateTime,
    Optional branchId As Integer = 0,
    Optional doctorId As Integer = 0) As List(Of CalendarAppointmentItem)

        Dim items As New List(Of CalendarAppointmentItem)

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()

            Dim query As String =
            "SELECT A.AppointmentID,
                    A.AppointmentStart,
                    P.FirstName + ' ' + P.LastName AS PatientName,
                    D.FirstName + ' ' + D.LastName AS DoctorName,
                    B.BranchName,
                    A.Status
             FROM Appointments A
             INNER JOIN Patients P ON A.PatientID = P.PatientID
             INNER JOIN Doctors D ON A.DoctorID = D.DoctorID
             INNER JOIN Branches B ON A.BranchID = B.BranchID
             WHERE CAST(A.AppointmentStart AS DATE) = @TargetDate"

            If branchId > 0 Then
                query &= " AND A.BranchID = @BranchID"
            End If

            If doctorId > 0 Then
                query &= " AND A.DoctorID = @DoctorID"
            End If

            query &= " ORDER BY A.AppointmentStart"

            Using cmd As New SqlCommand(query, connection)

                cmd.Parameters.Add("@TargetDate", SqlDbType.Date).Value = targetDate.Date

                If branchId > 0 Then
                    cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                End If

                If doctorId > 0 Then
                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
                End If

                connection.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()

                    While reader.Read()

                        items.Add(New CalendarAppointmentItem With {
                        .AppointmentID = CInt(reader("AppointmentID")),
                        .AppointmentTime = CDate(reader("AppointmentStart")),
                        .PatientName = reader("PatientName").ToString(),
                        .DoctorName = reader("DoctorName").ToString(),
                        .BranchName = reader("BranchName").ToString(),
                        .Status = reader("Status").ToString()
                    })

                    End While

                End Using

            End Using

        End Using

        Return items

    End Function
    Private Shared Sub AddAppointmentParameters(command As SqlCommand, appointment As Appointment, includeId As Boolean)
        If includeId Then
            command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointment.AppointmentID
        End If

        command.Parameters.Add("@BranchID", SqlDbType.Int).Value = appointment.BranchID
        command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = appointment.DoctorID
        command.Parameters.Add("@PatientID", SqlDbType.Int).Value = appointment.PatientID
        command.Parameters.Add("@AppointmentStart", SqlDbType.DateTime).Value = appointment.AppointmentStart
        command.Parameters.Add("@AppointmentEnd", SqlDbType.DateTime).Value = appointment.AppointmentEnd
        command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = If(String.IsNullOrWhiteSpace(appointment.Notes), CType(DBNull.Value, Object), appointment.Notes.Trim())
        command.Parameters.Add("@QueueNumber", SqlDbType.Int).Value = appointment.QueueNumber
        command.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = appointment.Status
        command.Parameters.Add("@CancellationReason", SqlDbType.NVarChar, 200).Value = If(String.IsNullOrWhiteSpace(appointment.CancellationReason), CType(DBNull.Value, Object), appointment.CancellationReason.Trim())
    End Sub

    Private Shared Function MapAppointment(reader As SqlDataReader) As Appointment
        Return New Appointment With {
            .AppointmentID = CInt(reader("AppointmentID")),
            .BranchID = CInt(reader("BranchID")),
            .DoctorID = CInt(reader("DoctorID")),
            .PatientID = CInt(reader("PatientID")),
            .AppointmentStart = Convert.ToDateTime(reader("AppointmentStart")),
            .AppointmentEnd = Convert.ToDateTime(reader("AppointmentEnd")),
            .Notes = reader("Notes").ToString(),
            .CancellationReason = If(IsDBNull(reader("CancellationReason")), String.Empty, reader("CancellationReason").ToString()),
            .QueueNumber = If(IsDBNull(reader("QueueNumber")), 0, CInt(reader("QueueNumber"))),
            .Status = reader("Status").ToString()
        }
    End Function
End Class
