Imports System.Data
Imports System.Data.SqlClient

Public Class RolesDAL
    Private Const SelectColumns As String = "RoleID, RoleCode, RoleName, Description, IsSystem, IsActive"

    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Roles ORDER BY RoleName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetById(roleId As Integer) As Role
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Roles WHERE RoleID = @RoleID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapRole(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function RoleExists(roleCode As String, roleName As String, Optional excludeRoleId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Roles WHERE (RoleCode = @RoleCode OR RoleName = @RoleName)"
            If excludeRoleId > 0 Then
                query &= " AND RoleID <> @RoleID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@RoleCode", SqlDbType.NVarChar, 30).Value = roleCode
                command.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50).Value = roleName
                If excludeRoleId > 0 Then
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = excludeRoleId
                End If
                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(role As Role)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "INSERT INTO dbo.Roles (RoleCode, RoleName, Description, IsSystem, IsActive) OUTPUT INSERTED.RoleID VALUES (@RoleCode, @RoleName, @Description, @IsSystem, @IsActive)"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@RoleCode", SqlDbType.NVarChar, 30).Value = role.RoleCode
                command.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50).Value = role.RoleName
                command.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ToDbValue(role.Description)
                command.Parameters.Add("@IsSystem", SqlDbType.Bit).Value = role.IsSystem
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = role.IsActive
                connection.Open()
                role.RoleID = CInt(command.ExecuteScalar())
            End Using
        End Using
    End Sub

    Public Sub Update(role As Role)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE dbo.Roles SET RoleCode = @RoleCode, RoleName = @RoleName, Description = @Description, IsSystem = @IsSystem, IsActive = @IsActive WHERE RoleID = @RoleID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = role.RoleID
                command.Parameters.Add("@RoleCode", SqlDbType.NVarChar, 30).Value = role.RoleCode
                command.Parameters.Add("@RoleName", SqlDbType.NVarChar, 50).Value = role.RoleName
                command.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ToDbValue(role.Description)
                command.Parameters.Add("@IsSystem", SqlDbType.Bit).Value = role.IsSystem
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = role.IsActive
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(roleId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Roles WHERE RoleID = @RoleID AND IsSystem = 0"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Shared Function MapRole(reader As SqlDataReader) As Role
        Return New Role With {
            .RoleID = CInt(reader("RoleID")),
            .RoleCode = reader("RoleCode").ToString(),
            .RoleName = reader("RoleName").ToString(),
            .Description = If(IsDBNull(reader("Description")), "", reader("Description").ToString()),
            .IsSystem = CBool(reader("IsSystem")),
            .IsActive = CBool(reader("IsActive"))
        }
    End Function
End Class
