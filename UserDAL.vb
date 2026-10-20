Imports System.Data
Imports System.Data.SqlClient

Public Class UserDAL

    Private Const LoginSelectSql As String =
        "SELECT u.UserID, u.Username, u.PasswordHash, u.RoleID, u.FullName, " &
        "       u.IsActive, u.IsLocked, u.FailedLoginAttempts, u.LockoutUntil, u.MustChangePassword, " &
        "       r.RoleCode, r.RoleName, " &
        "       d.DoctorID, " &
        "       COALESCE(" &
        "           (SELECT TOP 1 db.BranchID FROM dbo.DoctorBranches db " &
        "            WHERE db.DoctorID = d.DoctorID ORDER BY db.IsPrimary DESC, db.BranchID), " &
        "           (SELECT TOP 1 ub.BranchID FROM dbo.UserBranches ub " &
        "            WHERE ub.UserID = u.UserID ORDER BY ub.IsDefault DESC, ub.BranchID)" &
        "       ) AS BranchID " &
        "FROM dbo.Users u " &
        "INNER JOIN dbo.Roles r ON r.RoleID = u.RoleID " &
        "LEFT JOIN dbo.Doctors d ON d.UserID = u.UserID AND d.IsActive = 1 " &
        "WHERE u.Username = @Username"

    Public Function GetLoginUser(username As String) As LoginUserRecord
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Using command As New SqlCommand(LoginSelectSql, connection)
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapLoginUser(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function GetClinicSetting(settingKey As String) As String
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT SettingValue FROM dbo.ClinicSettings WHERE SettingKey = @SettingKey"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@SettingKey", SqlDbType.NVarChar, 100).Value = settingKey
                connection.Open()
                Dim value = command.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then
                    Return Nothing
                End If
                Return value.ToString()
            End Using
        End Using
    End Function

    Public Sub RecordSuccessfulLogin(userId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Users " &
                "SET FailedLoginAttempts = 0, IsLocked = 0, LockoutUntil = NULL, LastLoginAt = SYSUTCDATETIME() " &
                "WHERE UserID = @UserID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub RecordFailedLogin(userId As Integer, maxAttempts As Integer, lockoutMinutes As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Users " &
                "SET FailedLoginAttempts = FailedLoginAttempts + 1, " &
                "    LockoutUntil = CASE WHEN FailedLoginAttempts + 1 >= @MaxAttempts " &
                "        THEN DATEADD(MINUTE, @LockoutMinutes, SYSUTCDATETIME()) ELSE LockoutUntil END, " &
                "    IsLocked = CASE WHEN FailedLoginAttempts + 1 >= @MaxAttempts THEN 1 ELSE IsLocked END " &
                "WHERE UserID = @UserID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                command.Parameters.Add("@MaxAttempts", SqlDbType.Int).Value = maxAttempts
                command.Parameters.Add("@LockoutMinutes", SqlDbType.Int).Value = lockoutMinutes
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT u.UserID, u.Username, r.RoleName AS Role, u.FullName, u.IsActive " &
                "FROM dbo.Users u " &
                "INNER JOIN dbo.Roles r ON r.RoleID = u.RoleID " &
                "ORDER BY u.Username"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetById(userId As Integer) As ClinicUser
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT u.UserID, u.Username, u.RoleID, r.RoleCode, r.RoleName, u.FullName, u.IsActive " &
                "FROM dbo.Users u " &
                "INNER JOIN dbo.Roles r ON r.RoleID = u.RoleID " &
                "WHERE u.UserID = @UserID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return New ClinicUser With {
                        .UserID = CInt(reader("UserID")),
                        .Username = reader("Username").ToString(),
                        .RoleID = CInt(reader("RoleID")),
                        .RoleCode = reader("RoleCode").ToString(),
                        .Role = reader("RoleName").ToString(),
                        .FullName = If(IsDBNull(reader("FullName")), "", reader("FullName").ToString()),
                        .IsActive = CBool(reader("IsActive"))
                    }
                End Using
            End Using
        End Using
    End Function

    Public Function GetRoleIdByCodeOrName(role As String) As Integer?
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT TOP 1 RoleID FROM dbo.Roles " &
                "WHERE RoleCode = @Role OR RoleName = @Role"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = role.Trim()
                connection.Open()
                Dim value = command.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then
                    Return Nothing
                End If
                Return CInt(value)
            End Using
        End Using
    End Function

    Public Function GetActiveRoles() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT RoleID, RoleName FROM dbo.Roles WHERE IsActive = 1 ORDER BY RoleName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function UsernameExists(username As String, Optional excludeUserId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Users WHERE Username = @Username"
            If excludeUserId > 0 Then
                query &= " AND UserID <> @UserID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username
                If excludeUserId > 0 Then
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = excludeUserId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(user As ClinicUser, passwordHash As String)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Users (Username, PasswordHash, RoleID, FullName, IsActive, MustChangePassword) " &
                "VALUES (@Username, @PasswordHash, @RoleID, @FullName, @IsActive, 1)"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username
                command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = passwordHash
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = user.RoleID
                command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value =
                    If(String.IsNullOrWhiteSpace(user.FullName), CType(DBNull.Value, Object), user.FullName)
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(user As ClinicUser, passwordHash As String)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String =
                "UPDATE dbo.Users SET Username = @Username, RoleID = @RoleID, FullName = @FullName, IsActive = @IsActive"
            If Not String.IsNullOrWhiteSpace(passwordHash) Then
                query &= ", PasswordHash = @PasswordHash, MustChangePassword = 0, PasswordChangedAt = SYSUTCDATETIME()"
            End If
            query &= " WHERE UserID = @UserID"

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = user.RoleID
                command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value =
                    If(String.IsNullOrWhiteSpace(user.FullName), CType(DBNull.Value, Object), user.FullName)
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive
                If Not String.IsNullOrWhiteSpace(passwordHash) Then
                    command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = passwordHash
                End If

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(userId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Users WHERE UserID = @UserID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub SetPasswordHash(userId As Integer, passwordHash As String, mustChangePassword As Boolean)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Users " &
                "SET PasswordHash = @PasswordHash, MustChangePassword = @MustChangePassword, " &
                "    PasswordChangedAt = CASE WHEN @MustChangePassword = 0 THEN SYSUTCDATETIME() ELSE PasswordChangedAt END " &
                "WHERE UserID = @UserID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = passwordHash
                command.Parameters.Add("@MustChangePassword", SqlDbType.Bit).Value = mustChangePassword
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function MapLoginUser(reader As SqlDataReader) As LoginUserRecord
        Dim record As New LoginUserRecord With {
            .UserID = CInt(reader("UserID")),
            .Username = reader("Username").ToString(),
            .PasswordHash = reader("PasswordHash").ToString(),
            .RoleID = CInt(reader("RoleID")),
            .RoleCode = reader("RoleCode").ToString(),
            .RoleName = reader("RoleName").ToString(),
            .FullName = If(IsDBNull(reader("FullName")), "", reader("FullName").ToString()),
            .IsActive = CBool(reader("IsActive")),
            .IsLocked = CBool(reader("IsLocked")),
            .FailedLoginAttempts = CInt(reader("FailedLoginAttempts")),
            .MustChangePassword = CBool(reader("MustChangePassword"))
        }

        If Not IsDBNull(reader("LockoutUntil")) Then
            record.LockoutUntil = CDate(reader("LockoutUntil"))
        End If

        If Not IsDBNull(reader("DoctorID")) Then
            record.DoctorID = CInt(reader("DoctorID"))
        End If

        If Not IsDBNull(reader("BranchID")) Then
            record.BranchID = CInt(reader("BranchID"))
        End If

        Return record
    End Function
    ' ================================================================
    ' تُستخدم فقط من UserBAL.VerifyCurrentPassword
    ' ترجع PasswordHash فقط بدون أي بيانات أخرى
    ' ================================================================
    Public Function GetPasswordHashById(userId As Integer) As String
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT PasswordHash FROM dbo.Users WHERE UserID = @UserID AND IsActive = 1"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId
                connection.Open()
                Dim result = command.ExecuteScalar()
                If result Is Nothing OrElse result Is DBNull.Value Then
                    Return String.Empty
                End If
                Return result.ToString()
            End Using
        End Using
    End Function

End Class
