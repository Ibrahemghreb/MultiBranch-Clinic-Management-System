Imports System.Data
Imports System.Data.SqlClient

Public Class DiagnosisDAL
    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT DiagnosisID, AppointmentID, DoctorID, DiagnosisDate, DiagnosisText, Notes FROM Diagnoses"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
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

    Public Function GetAppointmentsByPatient(patientId As Integer) As List(Of PatientListItem)
        Dim items As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT a.AppointmentID, p.FirstName, p.LastName " &
                                  "FROM Appointments a " &
                                  "JOIN Patients p ON a.PatientID = p.PatientID " &
                                  "WHERE a.PatientID = @PatientID " &
                                  "ORDER BY a.AppointmentStart DESC"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientId
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

    Public Function GetById(diagnosisId As Integer) As Diagnosis
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT d.*, a.PatientID " &
                                  "FROM Diagnoses d " &
                                  "JOIN Appointments a ON d.AppointmentID = a.AppointmentID " &
                                  "WHERE d.DiagnosisID = @DiagnosisID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = diagnosisId
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapDiagnosis(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function DiagnosisExistsForAppointment(appointmentId As Integer, Optional excludeDiagnosisId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM Diagnoses WHERE AppointmentID = @AppointmentID"
            If excludeDiagnosisId > 0 Then
                query &= " AND DiagnosisID <> @DiagnosisID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointmentId
                If excludeDiagnosisId > 0 Then
                    command.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = excludeDiagnosisId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Function Insert(diagnosis As Diagnosis) As Integer
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "INSERT INTO Diagnoses (AppointmentID, DoctorID, DiagnosisDate, DiagnosisText, Notes) " &
                                  "VALUES (@AppointmentID, @DoctorID, @DiagnosisDate, @DiagnosisText, @Notes); SELECT CAST(SCOPE_IDENTITY() AS INT)"
            Using command As New SqlCommand(query, connection)
                AddDiagnosisParameters(command, diagnosis, includeId:=False)
                connection.Open()
                Return CInt(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Update(diagnosis As Diagnosis)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "UPDATE Diagnoses SET AppointmentID = @AppointmentID, DoctorID = @DoctorID, DiagnosisDate = @DiagnosisDate, DiagnosisText = @DiagnosisText, Notes = @Notes WHERE DiagnosisID = @DiagnosisID"
            Using command As New SqlCommand(query, connection)
                AddDiagnosisParameters(command, diagnosis, includeId:=True)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(diagnosisId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM Diagnoses WHERE DiagnosisID = @DiagnosisID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = diagnosisId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Sub AddDiagnosisParameters(command As SqlCommand, diagnosis As Diagnosis, includeId As Boolean)
        If includeId Then
            command.Parameters.Add("@DiagnosisID", SqlDbType.Int).Value = diagnosis.DiagnosisID
        End If

        command.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = diagnosis.AppointmentID
        command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = diagnosis.DoctorID
        command.Parameters.Add("@DiagnosisDate", SqlDbType.DateTime).Value = diagnosis.DiagnosisDate
        command.Parameters.Add("@DiagnosisText", SqlDbType.NVarChar).Value = ToDbValue(diagnosis.DiagnosisText)
        command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = ToDbValue(diagnosis.Notes)
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Shared Function MapDiagnosis(reader As SqlDataReader) As Diagnosis
        Return New Diagnosis With {
            .DiagnosisID = CInt(reader("DiagnosisID")),
            .AppointmentID = CInt(reader("AppointmentID")),
            .DoctorID = CInt(reader("DoctorID")),
            .PatientID = CInt(reader("PatientID")),
            .DiagnosisDate = If(IsDBNull(reader("DiagnosisDate")), DateTime.Now, Convert.ToDateTime(reader("DiagnosisDate"))),
            .DiagnosisText = reader("DiagnosisText").ToString(),
            .Report = String.Empty,
            .XRayImage = Nothing,
            .Notes = If(IsDBNull(reader("Notes")), String.Empty, reader("Notes").ToString())
        }
    End Function
End Class
