Imports System.Data
Imports System.Data.SqlClient

Public Class NotificationDAL

    Public Function GetByUser(userId As Integer, Optional unreadOnly As Boolean = False, Optional severity As String = "") As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim sb As New System.Text.StringBuilder()
            sb.AppendLine("SELECT NotificationID, UserID, Title, Message, Severity, IsRead, ExpiresAt, CreatedAt")
            sb.AppendLine("FROM dbo.Notifications WHERE UserID = @UserID")
            If unreadOnly Then sb.AppendLine("AND IsRead = 0")
            If Not String.IsNullOrWhiteSpace(severity) Then sb.AppendLine("AND Severity = @Severity")
            sb.AppendLine("AND (ExpiresAt IS NULL OR ExpiresAt > SYSUTCDATETIME())")
            sb.AppendLine("ORDER BY CreatedAt DESC")
            Using cmd As New SqlCommand(sb.ToString(), conn)
                cmd.Parameters.AddWithValue("@UserID", userId)
                If Not String.IsNullOrWhiteSpace(severity) Then cmd.Parameters.AddWithValue("@Severity", severity)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetUnreadCount(userId As Integer) As Integer
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT COUNT(*) FROM dbo.Notifications WHERE UserID = @UserID AND IsRead = 0 " &
                "AND (ExpiresAt IS NULL OR ExpiresAt > SYSUTCDATETIME())"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserID", userId)
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub Create(userId As Integer, title As String, message As String, Optional severity As String = "Info", Optional expiresAt As DateTime? = Nothing)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Notifications (UserID, Title, Message, Severity, ExpiresAt) " &
                "VALUES (@UserID, @Title, @Message, @Severity, @ExpiresAt)"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserID", userId)
                cmd.Parameters.AddWithValue("@Title", title)
                cmd.Parameters.AddWithValue("@Message", message)
                cmd.Parameters.AddWithValue("@Severity", severity)
                If expiresAt.HasValue Then
                    cmd.Parameters.AddWithValue("@ExpiresAt", expiresAt.Value)
                Else
                    cmd.Parameters.AddWithValue("@ExpiresAt", DBNull.Value)
                End If
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub MarkRead(notificationId As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE dbo.Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@NotificationID", notificationId)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub MarkAllRead(userId As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "UPDATE dbo.Notifications SET IsRead = 1 WHERE UserID = @UserID AND IsRead = 0"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserID", userId)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(notificationId As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Notifications WHERE NotificationID = @NotificationID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@NotificationID", notificationId)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DeleteExpired()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Notifications WHERE ExpiresAt IS NOT NULL AND ExpiresAt <= SYSUTCDATETIME()"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function GetAdminUserIds() As List(Of Integer)
        Dim ids As New List(Of Integer)()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT u.UserID FROM dbo.Users u INNER JOIN dbo.Roles r ON r.RoleID = u.RoleID " &
                "WHERE r.RoleCode = N'ADMIN' AND u.IsActive = 1"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        ids.Add(reader.GetInt32(0))
                    End While
                End Using
            End Using
        End Using
        Return ids
    End Function
End Class
