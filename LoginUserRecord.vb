Public Class LoginUserRecord
    Public Property UserID As Integer
    Public Property Username As String
    Public Property PasswordHash As String
    Public Property RoleID As Integer
    Public Property RoleCode As String
    Public Property RoleName As String
    Public Property FullName As String
    Public Property IsActive As Boolean
    Public Property IsLocked As Boolean
    Public Property FailedLoginAttempts As Integer
    Public Property LockoutUntil As DateTime?
    Public Property MustChangePassword As Boolean
    Public Property DoctorID As Integer?
    Public Property BranchID As Integer?
End Class
