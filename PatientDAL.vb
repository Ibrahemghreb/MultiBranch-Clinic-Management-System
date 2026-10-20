Imports System.Data
Imports System.Data.SqlClient

Public Class PatientDAL
    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT PatientID, PatientCode, FirstName, LastName, DateOfBirth, Gender, NationalID, PassportNo, Phone, Phone2, Email, Address, City, CountryCode, BloodType, EmergencyContact, EmergencyPhone, Notes FROM Patients WHERE IsDeleted = 0 ORDER BY LastName, FirstName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetDoctors() As List(Of PatientListItem)
        Dim doctors As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT DoctorID, FirstName, LastName FROM Doctors ORDER BY FirstName, LastName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        doctors.Add(New PatientListItem(reader.GetInt32(0), $"{reader.GetInt32(0)} - {reader.GetString(1)} {reader.GetString(2)}"))
                    End While
                End Using
            End Using
        End Using

        Return doctors
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Dim branches As New List(Of PatientListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT BranchID, BranchName FROM Branches ORDER BY BranchName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        branches.Add(New PatientListItem(reader.GetInt32(0), $"{reader.GetInt32(0)} - {reader.GetString(1)}"))
                    End While
                End Using
            End Using
        End Using

        Return branches
    End Function

    Public Function GetById(patientId As Integer) As Patient
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT PatientID, PatientCode, FirstName, LastName, DateOfBirth, Gender, NationalID, PassportNo, Phone, Phone2, Email, Address, City, CountryCode, BloodType, EmergencyContact, EmergencyPhone, Notes, IsDeleted, DeletedAt, DeletedBy, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy FROM Patients WHERE PatientID = @PatientID AND IsDeleted = 0"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientId
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapPatient(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function PatientExists(firstName As String, lastName As String, phone As String, nationalId As String, dateOfBirth As DateTime, Optional excludePatientId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM Patients WHERE IsDeleted = 0 AND ((NationalID IS NOT NULL AND NationalID <> '' AND NationalID = @NationalID) OR (Phone IS NOT NULL AND Phone <> '' AND Phone = @Phone) OR (FirstName = @FirstName AND LastName = @LastName AND DateOfBirth = @DateOfBirth))"
            If excludePatientId > 0 Then
                query &= " AND PatientID <> @PatientID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@NationalID", SqlDbType.NVarChar, 20).Value = If(String.IsNullOrWhiteSpace(nationalId), CType(DBNull.Value, Object), nationalId.Trim())
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = If(String.IsNullOrWhiteSpace(phone), CType(DBNull.Value, Object), phone.Trim())
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(firstName), CType(DBNull.Value, Object), firstName.Trim())
                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = If(String.IsNullOrWhiteSpace(lastName), CType(DBNull.Value, Object), lastName.Trim())
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = dateOfBirth.Date
                If excludePatientId > 0 Then
                    command.Parameters.Add("@PatientID", SqlDbType.Int).Value = excludePatientId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(patient As Patient)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "INSERT INTO Patients (PatientCode, FirstName, LastName, DateOfBirth, Gender, NationalID, PassportNo, Phone, Phone2, Email, Address, City, CountryCode, BloodType, EmergencyContact, EmergencyPhone, Notes, CreatedAt) VALUES (@PatientCode, @FirstName, @LastName, @DateOfBirth, @Gender, @NationalID, @PassportNo, @Phone, @Phone2, @Email, @Address, @City, @CountryCode, @BloodType, @EmergencyContact, @EmergencyPhone, @Notes, SYSUTCDATETIME())"
            Using command As New SqlCommand(query, connection)
                AddPatientParameters(command, patient, includeId:=False)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(patient As Patient)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE Patients SET PatientCode = @PatientCode, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, NationalID = @NationalID, PassportNo = @PassportNo, Phone = @Phone, Phone2 = @Phone2, Email = @Email, Address = @Address, City = @City, CountryCode = @CountryCode, BloodType = @BloodType, EmergencyContact = @EmergencyContact, EmergencyPhone = @EmergencyPhone, Notes = @Notes, UpdatedAt = SYSUTCDATETIME() WHERE PatientID = @PatientID"
            Using command As New SqlCommand(query, connection)
                AddPatientParameters(command, patient, includeId:=True)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(patientId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE Patients SET IsDeleted = 1, DeletedAt = SYSUTCDATETIME() WHERE PatientID = @PatientID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patientId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Sub AddPatientParameters(command As SqlCommand, patient As Patient, includeId As Boolean)
        If includeId Then
            command.Parameters.Add("@PatientID", SqlDbType.Int).Value = patient.PatientID
        End If

        command.Parameters.Add("@PatientCode", SqlDbType.NVarChar, 30).Value = ToDbValue(patient.PatientCode)
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = ToDbValue(patient.FirstName)
        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = ToDbValue(patient.LastName)
        command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = If(patient.DateOfBirth = DateTime.MinValue, CType(DBNull.Value, Object), patient.DateOfBirth.Date)
        command.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = ToDbValue(patient.Gender)
        command.Parameters.Add("@NationalID", SqlDbType.NVarChar, 20).Value = ToDbValue(patient.NationalID)
        command.Parameters.Add("@PassportNo", SqlDbType.NVarChar, 30).Value = ToDbValue(patient.PassportNo)
        command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = ToDbValue(patient.Phone)
        command.Parameters.Add("@Phone2", SqlDbType.NVarChar, 20).Value = ToDbValue(patient.Phone2)
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDbValue(patient.Email)
        command.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = ToDbValue(patient.Address)
        command.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = ToDbValue(patient.City)
        command.Parameters.Add("@CountryCode", SqlDbType.Char, 2).Value = If(String.IsNullOrWhiteSpace(patient.CountryCode), CType(DBNull.Value, Object), patient.CountryCode.Trim().ToUpper())
        command.Parameters.Add("@BloodType", SqlDbType.NVarChar, 5).Value = ToDbValue(patient.BloodType)
        command.Parameters.Add("@EmergencyContact", SqlDbType.NVarChar, 50).Value = ToDbValue(patient.EmergencyContact)
        command.Parameters.Add("@EmergencyPhone", SqlDbType.NVarChar, 20).Value = ToDbValue(patient.EmergencyPhone)
        command.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = ToDbValue(patient.Notes)
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Shared Function MapPatient(reader As SqlDataReader) As Patient
        Return New Patient With {
            .PatientID = CInt(reader("PatientID")),
            .PatientCode = reader("PatientCode").ToString(),
            .FirstName = reader("FirstName").ToString(),
            .LastName = reader("LastName").ToString(),
            .DateOfBirth = If(IsDBNull(reader("DateOfBirth")), DateTime.Now, Convert.ToDateTime(reader("DateOfBirth"))),
            .Gender = reader("Gender").ToString(),
            .NationalID = reader("NationalID").ToString(),
            .PassportNo = reader("PassportNo").ToString(),
            .Phone = reader("Phone").ToString(),
            .Phone2 = reader("Phone2").ToString(),
            .Email = reader("Email").ToString(),
            .Address = reader("Address").ToString(),
            .City = reader("City").ToString(),
            .CountryCode = reader("CountryCode").ToString(),
            .BloodType = reader("BloodType").ToString(),
            .EmergencyContact = reader("EmergencyContact").ToString(),
            .EmergencyPhone = reader("EmergencyPhone").ToString(),
            .Notes = reader("Notes").ToString(),
            .IsDeleted = Not IsDBNull(reader("IsDeleted")) AndAlso CBool(reader("IsDeleted")),
            .DeletedAt = If(IsDBNull(reader("DeletedAt")), CType(Nothing, DateTime?), Convert.ToDateTime(reader("DeletedAt"))),
            .DeletedBy = If(IsDBNull(reader("DeletedBy")), CType(Nothing, Integer?), CInt(reader("DeletedBy"))),
            .CreatedAt = If(IsDBNull(reader("CreatedAt")), DateTime.Now, Convert.ToDateTime(reader("CreatedAt"))),
            .CreatedBy = If(IsDBNull(reader("CreatedBy")), CType(Nothing, Integer?), CInt(reader("CreatedBy"))),
            .UpdatedAt = If(IsDBNull(reader("UpdatedAt")), CType(Nothing, DateTime?), Convert.ToDateTime(reader("UpdatedAt"))),
            .UpdatedBy = If(IsDBNull(reader("UpdatedBy")), CType(Nothing, Integer?), CInt(reader("UpdatedBy")))
        }
    End Function
End Class
