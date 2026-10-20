Public Class NotificationBAL
    Private ReadOnly _dal As New NotificationDAL()

    Public Function GetNotifications(userId As Integer, unreadOnly As Boolean, severity As String) As DataTable
        Return _dal.GetByUser(userId, unreadOnly, severity)
    End Function

    Public Function GetUnreadCount(userId As Integer) As Integer
        Return _dal.GetUnreadCount(userId)
    End Function

    Public Sub NotifyUser(userId As Integer, title As String, message As String, Optional severity As String = "Info")
        If userId <= 0 Then Return
        _dal.Create(userId, title, message, severity)
    End Sub

    Public Sub NotifyAdmins(title As String, message As String, Optional severity As String = "Warning")
        For Each adminId In _dal.GetAdminUserIds()
            _dal.Create(adminId, title, message, severity)
        Next
    End Sub

    Public Sub MarkRead(notificationId As Integer)
        _dal.MarkRead(notificationId)
    End Sub

    Public Sub MarkAllRead(userId As Integer)
        _dal.MarkAllRead(userId)
    End Sub

    Public Sub Delete(notificationId As Integer)
        _dal.Delete(notificationId)
    End Sub

    Public Sub NotifyAppointmentCreated(patientName As String, appointmentStart As DateTime, doctorUserId As Integer)
        NotifyUser(doctorUserId, "موعد جديد", $"موعد جديد للمريض {patientName} في {appointmentStart:yyyy-MM-dd HH:mm}", "Info")
    End Sub

    Public Sub NotifyAppointmentCancelled(patientName As String, appointmentStart As DateTime)
        NotifyAdmins("إلغاء موعد", $"تم إلغاء موعد {patientName} بتاريخ {appointmentStart:yyyy-MM-dd HH:mm}", "Warning")
    End Sub

    Public Sub NotifyFailedLogin(username As String)
        NotifyAdmins("محاولة دخول فاشلة", $"فشل تسجيل الدخول للمستخدم: {username}", "Error")
    End Sub
End Class
