Imports System.Data.SqlClient

Public Class AuditLogDAL

    Public Shared Sub WriteLog(actionType As String, tableName As String, recordId As Integer, details As String)
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Const query As String =
                    "INSERT INTO dbo.AuditLogs (UserID, ActionType, TableName, RecordID, NewValuesJson, BranchID, MachineName) " &
                    "VALUES (@UserID, @ActionType, @TableName, @RecordID, @NewValuesJson, @BranchID, @MachineName)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", If(SessionManager.UserID > 0, CObj(SessionManager.UserID), DBNull.Value))
                    cmd.Parameters.AddWithValue("@ActionType", actionType)
                    cmd.Parameters.AddWithValue("@TableName", tableName)
                    cmd.Parameters.AddWithValue("@RecordID", If(recordId > 0, CObj(recordId), DBNull.Value))
                    cmd.Parameters.AddWithValue("@NewValuesJson", If(String.IsNullOrWhiteSpace(details), DBNull.Value, details))
                    cmd.Parameters.AddWithValue("@BranchID", If(SessionManager.BranchID > 0, CObj(SessionManager.BranchID), DBNull.Value))
                    cmd.Parameters.AddWithValue("@MachineName", Environment.MachineName)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"AuditLogDAL.WriteLog: {ex.Message}")
        End Try
    End Sub
End Class
