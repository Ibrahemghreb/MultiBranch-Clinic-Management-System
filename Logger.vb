Imports System.IO

Public Class Logger
    Private Shared ReadOnly LogFilePath As String = Path.Combine(Application.StartupPath, "Logs", "ClinicApp.log")

    Public Shared Sub LogError(message As String)
        Try
            Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath))
            Using writer As New StreamWriter(LogFilePath, True)
                writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}")
            End Using
        Catch
            ' تجاهل أخطاء التسجيل
        End Try
    End Sub
End Class