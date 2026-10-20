Imports System.Data.SqlClient

Public Class PermissionService
    Private Shared _cachedPermissions As HashSet(Of String)
    Private Shared _cachedRoleId As Integer = -1

    Public Shared Sub ClearCache()
        _cachedPermissions = Nothing
        _cachedRoleId = -1
    End Sub

    Public Shared Function HasPermission(permissionCode As String) As Boolean
        If SessionManager.UserID <= 0 Then Return False
        If String.Equals(SessionManager.RoleCode, "ADMIN", StringComparison.OrdinalIgnoreCase) Then Return True

        EnsureCache()
        Return _cachedPermissions IsNot Nothing AndAlso _cachedPermissions.Contains(permissionCode)
    End Function

    Public Shared Function HasAnyPermission(ParamArray codes As String()) As Boolean
        For Each code In codes
            If HasPermission(code) Then Return True
        Next
        Return False
    End Function

    Private Shared Sub EnsureCache()
        Dim roleId = SessionManager.RoleID
        If roleId <= 0 Then
            _cachedPermissions = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Return
        End If

        If _cachedPermissions IsNot Nothing AndAlso _cachedRoleId = roleId Then Return

        _cachedRoleId = roleId
        _cachedPermissions = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Const query As String =
                    "SELECT p.PermissionCode FROM dbo.RolePermissions rp " &
                    "INNER JOIN dbo.Permissions p ON p.PermissionID = rp.PermissionID " &
                    "WHERE rp.RoleID = @RoleID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@RoleID", roleId)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            _cachedPermissions.Add(reader.GetString(0))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"PermissionService.EnsureCache: {ex.Message}")
        End Try
    End Sub
End Class
