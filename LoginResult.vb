Public Enum LoginStatus
    Success = 0
    InvalidCredentials = 1
    UserNotFound = 2
    AccountInactive = 3
    AccountLocked = 4
    MustChangePassword = 5
    InvalidStoredPassword = 6
End Enum

Public Class LoginResult
    Public Property Status As LoginStatus
    Public Property Message As String = ""
    Public Property UserID As Integer
    Public Property Username As String = ""
    Public Property FullName As String = ""
    Public Property Role As String = ""
    Public Property RoleCode As String = ""
    Public Property RoleID As Integer
    Public Property DoctorID As Integer
    Public Property BranchID As Integer
    Public Property MustChangePassword As Boolean

    Public ReadOnly Property IsSuccess As Boolean
        Get
            Return Status = LoginStatus.Success OrElse Status = LoginStatus.MustChangePassword
        End Get
    End Property
End Class
