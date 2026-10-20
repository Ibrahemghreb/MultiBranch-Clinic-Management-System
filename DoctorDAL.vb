Imports System.Data
Imports System.Data.SqlClient

Public Class DoctorDAL
    Private Const DoctorSelectColumns As String =
        "d.DoctorID, d.UserID, d.FirstName, d.LastName, d.Phone, d.Email, d.LicenseNo, d.IsActive, " &
        "db.BranchID, ds.SpecialtyID"

    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String =
                "SELECT d.DoctorID, d.UserID, d.FirstName, d.LastName, d.Phone, d.Email, d.IsActive, " &
                "       b.BranchName, s.SpecialtyName " &
                "FROM dbo.Doctors d " &
                "LEFT JOIN dbo.DoctorBranches db ON db.DoctorID = d.DoctorID AND db.IsPrimary = 1 " &
                "LEFT JOIN dbo.Branches b ON b.BranchID = db.BranchID " &
                "LEFT JOIN dbo.DoctorSpecialties ds ON ds.DoctorID = d.DoctorID " &
                "LEFT JOIN dbo.Specialties s ON s.SpecialtyID = ds.SpecialtyID " &
                "ORDER BY d.FirstName, d.LastName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetBranches() As List(Of BranchListItem)
        Dim branches As New List(Of BranchListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT BranchID, BranchName FROM dbo.Branches WHERE IsActive = 1 ORDER BY BranchName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        branches.Add(New BranchListItem With {
                            .BranchID = reader.GetInt32(0),
                            .BranchName = reader.GetString(1)
                        })
                    End While
                End Using
            End Using
        End Using

        Return branches
    End Function

    Public Function GetSpecialties() As List(Of SpecialtyListItem)
        Dim specialties As New List(Of SpecialtyListItem)()

        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT SpecialtyID, SpecialtyName FROM dbo.Specialties WHERE IsActive = 1 ORDER BY SpecialtyName"
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        specialties.Add(New SpecialtyListItem With {
                            .SpecialtyID = reader.GetInt32(0),
                            .SpecialtyName = reader.GetString(1)
                        })
                    End While
                End Using
            End Using
        End Using

        Return specialties
    End Function

    Public Function GetById(doctorId As Integer) As Doctor
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {DoctorSelectColumns} FROM dbo.Doctors d " &
                                  "LEFT JOIN dbo.DoctorBranches db ON db.DoctorID = d.DoctorID AND db.IsPrimary = 1 " &
                                  "LEFT JOIN dbo.DoctorSpecialties ds ON ds.DoctorID = d.DoctorID " &
                                  "WHERE d.DoctorID = @DoctorID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapDoctor(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function UserExists(userId As Integer) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT COUNT(*) FROM dbo.Users WHERE UserID = @UserID AND IsActive = 1"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Function UserAlreadyLinked(userId As Integer, Optional excludeDoctorId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Doctors WHERE UserID = @UserID"
            If excludeDoctorId > 0 Then
                query &= " AND DoctorID <> @DoctorID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                If excludeDoctorId > 0 Then
                    command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = excludeDoctorId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Function DoctorExists(firstName As String, lastName As String, phone As String, Optional excludeDoctorId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Doctors WHERE FirstName = @FirstName AND LastName = @LastName AND Phone = @Phone"
            If excludeDoctorId > 0 Then
                query &= " AND DoctorID <> @DoctorID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = firstName
                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = lastName
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = If(String.IsNullOrWhiteSpace(phone), CType(DBNull.Value, Object), phone)
                If excludeDoctorId > 0 Then
                    command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = excludeDoctorId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(doctor As Doctor)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Const insertDoctor As String =
                        "INSERT INTO dbo.Doctors (UserID, FirstName, LastName, Phone, Email, LicenseNo, IsActive) " &
                        "OUTPUT INSERTED.DoctorID VALUES (@UserID, @FirstName, @LastName, @Phone, @Email, @LicenseNo, @IsActive)"

                    Dim doctorId As Integer
                    Using command As New SqlCommand(insertDoctor, connection, transaction)
                        AddDoctorCoreParameters(command, doctor)
                        doctorId = CInt(command.ExecuteScalar())
                    End Using

                    SaveBranchLink(connection, transaction, doctorId, doctor.BranchID)
                    SaveSpecialtyLink(connection, transaction, doctorId, doctor.SpecialtyID)
                    transaction.Commit()
                Catch
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Public Sub Update(doctor As Doctor)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            connection.Open()
            Using transaction = connection.BeginTransaction()
                Try
                    Const updateDoctor As String =
                        "UPDATE dbo.Doctors SET UserID = @UserID, FirstName = @FirstName, LastName = @LastName, " &
                        "Phone = @Phone, Email = @Email, LicenseNo = @LicenseNo, IsActive = @IsActive WHERE DoctorID = @DoctorID"

                    Using command As New SqlCommand(updateDoctor, connection, transaction)
                        command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctor.DoctorID
                        AddDoctorCoreParameters(command, doctor)
                        command.ExecuteNonQuery()
                    End Using

                    Using command As New SqlCommand("DELETE FROM dbo.DoctorBranches WHERE DoctorID = @DoctorID", connection, transaction)
                        command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctor.DoctorID
                        command.ExecuteNonQuery()
                    End Using

                    Using command As New SqlCommand("DELETE FROM dbo.DoctorSpecialties WHERE DoctorID = @DoctorID", connection, transaction)
                        command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctor.DoctorID
                        command.ExecuteNonQuery()
                    End Using

                    SaveBranchLink(connection, transaction, doctor.DoctorID, doctor.BranchID)
                    SaveSpecialtyLink(connection, transaction, doctor.DoctorID, doctor.SpecialtyID)
                    transaction.Commit()
                Catch
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Public Sub Delete(doctorId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Doctors WHERE DoctorID = @DoctorID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Sub SaveBranchLink(connection As SqlConnection, transaction As SqlTransaction, doctorId As Integer, branchId As Integer)
        Const query As String = "INSERT INTO dbo.DoctorBranches (DoctorID, BranchID, IsPrimary) VALUES (@DoctorID, @BranchID, 1)"
        Using command As New SqlCommand(query, connection, transaction)
            command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
            command.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Sub SaveSpecialtyLink(connection As SqlConnection, transaction As SqlTransaction, doctorId As Integer, specialtyId As Integer)
        If specialtyId <= 0 Then
            Return
        End If

        Const query As String = "INSERT INTO dbo.DoctorSpecialties (DoctorID, SpecialtyID) VALUES (@DoctorID, @SpecialtyID)"
        Using command As New SqlCommand(query, connection, transaction)
            command.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorId
            command.Parameters.Add("@SpecialtyID", SqlDbType.Int).Value = specialtyId
            command.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Sub AddDoctorCoreParameters(command As SqlCommand, doctor As Doctor)
        command.Parameters.Add("@UserID", SqlDbType.Int).Value = doctor.UserID
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = doctor.FirstName
        command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = doctor.LastName
        command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = ToDbValue(doctor.Phone)
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDbValue(doctor.Email)
        command.Parameters.Add("@LicenseNo", SqlDbType.NVarChar, 50).Value = ToDbValue(doctor.LicenseNo)
        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = doctor.IsActive
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Shared Function MapDoctor(reader As SqlDataReader) As Doctor
        Dim doctor As New Doctor With {
            .DoctorID = CInt(reader("DoctorID")),
            .UserID = CInt(reader("UserID")),
            .FirstName = reader("FirstName").ToString(),
            .LastName = reader("LastName").ToString(),
            .Phone = If(IsDBNull(reader("Phone")), "", reader("Phone").ToString()),
            .Email = If(IsDBNull(reader("Email")), "", reader("Email").ToString()),
            .LicenseNo = If(IsDBNull(reader("LicenseNo")), "", reader("LicenseNo").ToString()),
            .IsActive = CBool(reader("IsActive"))
        }

        If Not IsDBNull(reader("BranchID")) Then
            doctor.BranchID = CInt(reader("BranchID"))
        End If

        If Not IsDBNull(reader("SpecialtyID")) Then
            doctor.SpecialtyID = CInt(reader("SpecialtyID"))
        End If

        Return doctor
    End Function
End Class
