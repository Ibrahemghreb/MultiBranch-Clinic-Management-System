Imports System.Data
Imports System.Data.SqlClient

Public Class PermissionsDAL
    Private Const SelectColumns As String = "PermissionID, PermissionCode, PermissionName, ModuleName, Description"

    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Permissions ORDER BY PermissionName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetById(permissionId As Integer) As Permission
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Permissions WHERE PermissionID = @PermissionID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionId
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapPermission(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function PermissionExists(permissionCode As String, permissionName As String, Optional excludePermissionId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Permissions WHERE (PermissionCode = @PermissionCode OR PermissionName = @PermissionName)"
            If excludePermissionId > 0 Then
                query &= " AND PermissionID <> @PermissionID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PermissionCode", SqlDbType.NVarChar, 50).Value = permissionCode
                command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permissionName
                If excludePermissionId > 0 Then
                    command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = excludePermissionId
                End If
                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(permission As Permission)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "INSERT INTO dbo.Permissions (PermissionCode, PermissionName, ModuleName, Description) OUTPUT INSERTED.PermissionID VALUES (@PermissionCode, @PermissionName, @ModuleName, @Description)"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PermissionCode", SqlDbType.NVarChar, 50).Value = permission.PermissionCode
                command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permission.PermissionName
                command.Parameters.Add("@ModuleName", SqlDbType.NVarChar, 50).Value = ToDbValue(permission.ModuleName)
                command.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ToDbValue(permission.Description)
                connection.Open()
                permission.PermissionID = CInt(command.ExecuteScalar())
            End Using
        End Using
    End Sub

    Public Sub Update(permission As Permission)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE dbo.Permissions SET PermissionCode = @PermissionCode, PermissionName = @PermissionName, ModuleName = @ModuleName, Description = @Description WHERE PermissionID = @PermissionID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permission.PermissionID
                command.Parameters.Add("@PermissionCode", SqlDbType.NVarChar, 50).Value = permission.PermissionCode
                command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permission.PermissionName
                command.Parameters.Add("@ModuleName", SqlDbType.NVarChar, 50).Value = ToDbValue(permission.ModuleName)
                command.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ToDbValue(permission.Description)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(permissionId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Permissions WHERE PermissionID = @PermissionID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionId
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

    Private Shared Function MapPermission(reader As SqlDataReader) As Permission
        Return New Permission With {
            .PermissionID = CInt(reader("PermissionID")),
            .PermissionCode = reader("PermissionCode").ToString(),
            .PermissionName = reader("PermissionName").ToString(),
            .ModuleName = If(IsDBNull(reader("ModuleName")), "", reader("ModuleName").ToString()),
            .Description = If(IsDBNull(reader("Description")), "", reader("Description").ToString())
        }
    End Function
End Class
