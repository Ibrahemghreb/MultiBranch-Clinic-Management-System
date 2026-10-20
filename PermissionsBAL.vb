Imports System.Data

Public Class PermissionsBAL
    Private ReadOnly dal As PermissionsDAL

    Public Sub New()
        Me.New(New PermissionsDAL())
    End Sub

    Public Sub New(repository As PermissionsDAL)
        dal = repository
    End Sub

    Public Function GetAllPermissions() As DataTable
        Return dal.GetAll()
    End Function

    Public Function GetPermissionById(permissionId As Integer) As Permission
        If permissionId <= 0 Then
            Throw New ArgumentException("Invalid permission ID.")
        End If

        Return dal.GetById(permissionId)
    End Function

    Public Sub AddPermission(permission As Permission)
        ValidatePermission(permission, requireId:=False)

        If dal.PermissionExists(permission.PermissionCode, permission.PermissionName) Then
            Throw New InvalidOperationException("Permission code or name already exists.")
        End If

        dal.Insert(permission)
    End Sub

    Public Sub UpdatePermission(permission As Permission)
        ValidatePermission(permission, requireId:=True)

        If dal.PermissionExists(permission.PermissionCode, permission.PermissionName, permission.PermissionID) Then
            Throw New InvalidOperationException("Another permission with the same code or name already exists.")
        End If

        dal.Update(permission)
    End Sub

    Public Sub DeletePermission(permissionId As Integer)
        If permissionId <= 0 Then
            Throw New ArgumentException("Please select a valid permission to delete.")
        End If

        dal.Delete(permissionId)
    End Sub

    Private Shared Sub ValidatePermission(permission As Permission, requireId As Boolean)
        If permission Is Nothing Then
            Throw New ArgumentNullException(NameOf(permission))
        End If

        If requireId AndAlso permission.PermissionID <= 0 Then
            Throw New ArgumentException("Invalid permission ID.")
        End If

        If String.IsNullOrWhiteSpace(permission.PermissionCode) Then
            Throw New ArgumentException("Please enter permission code.")
        End If

        If String.IsNullOrWhiteSpace(permission.PermissionName) Then
            Throw New ArgumentException("Please enter permission name.")
        End If

        permission.PermissionCode = permission.PermissionCode.Trim()
        permission.PermissionName = permission.PermissionName.Trim()
        permission.ModuleName = If(permission.ModuleName, String.Empty).Trim()
        permission.Description = If(permission.Description, String.Empty).Trim()
    End Sub
End Class
