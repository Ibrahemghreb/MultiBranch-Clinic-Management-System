Public Class NotificationItem
    Public Property NotificationID As Integer
    Public Property UserID As Integer
    Public Property Title As String
    Public Property Message As String
    Public Property Severity As String
    Public Property IsRead As Boolean
    Public Property ExpiresAt As DateTime?
    Public Property CreatedAt As DateTime
End Class
