Imports System.Data

Public Class RolesBAL
    Private ReadOnly rolesDal As RolesDAL

    Public Sub New()
        Me.New(New RolesDAL())
    End Sub

    Public Sub New(repository As RolesDAL)
        rolesDal = repository
    End Sub

    Public Function GetAllRoles() As DataTable
        Return rolesDal.GetAll()
    End Function

    Public Function GetRoleById(roleId As Integer) As Role
        If roleId <= 0 Then
            Throw New ArgumentException("Invalid role ID.")
        End If

        Return rolesDal.GetById(roleId)
    End Function

    Public Sub AddRole(role As Role)
        ValidateRole(role, requireRoleId:=False)

        If rolesDal.RoleExists(role.RoleCode, role.RoleName) Then
            Throw New InvalidOperationException("Role code or name already exists.")
        End If

        rolesDal.Insert(role)
    End Sub

    Public Sub UpdateRole(role As Role)
        ValidateRole(role, requireRoleId:=True)

        If rolesDal.RoleExists(role.RoleCode, role.RoleName, role.RoleID) Then
            Throw New InvalidOperationException("Another role with the same code or name already exists.")
        End If

        rolesDal.Update(role)
    End Sub

    Public Sub DeleteRole(roleId As Integer)
        If roleId <= 0 Then
            Throw New ArgumentException("Please select a valid role to delete.")
        End If

        rolesDal.Delete(roleId)
    End Sub

    Private Shared Sub ValidateRole(role As Role, requireRoleId As Boolean)
        If role Is Nothing Then
            Throw New ArgumentNullException(NameOf(role))
        End If

        If requireRoleId AndAlso role.RoleID <= 0 Then
            Throw New ArgumentException("Invalid role ID.")
        End If

        If String.IsNullOrWhiteSpace(role.RoleCode) Then
            Throw New ArgumentException("Please enter role code.")
        End If

        If String.IsNullOrWhiteSpace(role.RoleName) Then
            Throw New ArgumentException("Please enter role name.")
        End If

        role.RoleCode = role.RoleCode.Trim()
        role.RoleName = role.RoleName.Trim()
        role.Description = If(role.Description, String.Empty).Trim()
    End Sub
End Class
