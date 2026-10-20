Imports System.Data
Imports System.Data.SqlClient

Public Class ClinicSettingsDAL

    Public Function GetAllSettings() As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Try
            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                Const query As String = "SELECT SettingKey, SettingValue FROM dbo.ClinicSettings"
                Using cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result(reader.GetString(0)) = reader.GetString(1)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"ClinicSettingsDAL.GetAllSettings: {ex.Message}")
            Throw
        End Try
        Return result
    End Function

    Public Function GetSetting(settingKey As String) As String
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT SettingValue FROM dbo.ClinicSettings WHERE SettingKey = @SettingKey"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@SettingKey", SqlDbType.NVarChar, 100).Value = settingKey
                conn.Open()
                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return Nothing
                Return value.ToString()
            End Using
        End Using
    End Function

    Public Sub UpsertSetting(settingKey As String, settingValue As String, Optional updatedBy As Integer = 0)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "IF EXISTS (SELECT 1 FROM dbo.ClinicSettings WHERE SettingKey = @SettingKey) " &
                "UPDATE dbo.ClinicSettings SET SettingValue = @SettingValue, UpdatedAt = SYSUTCDATETIME(), UpdatedBy = @UpdatedBy WHERE SettingKey = @SettingKey " &
                "ELSE INSERT INTO dbo.ClinicSettings (SettingKey, SettingValue, UpdatedBy) VALUES (@SettingKey, @SettingValue, @UpdatedBy)"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@SettingKey", SqlDbType.NVarChar, 100).Value = settingKey
                cmd.Parameters.Add("@SettingValue", SqlDbType.NVarChar, 500).Value = settingValue
                If updatedBy > 0 Then
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = updatedBy
                Else
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = DBNull.Value
                End If
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub SaveSettings(settings As Dictionary(Of String, String), Optional updatedBy As Integer = 0)
        For Each kvp In settings
            UpsertSetting(kvp.Key, kvp.Value, updatedBy)
        Next
    End Sub
End Class
