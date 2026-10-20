Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports System.Management

Namespace My
    Module Program
        Private Function IsActivated() As Boolean
            Dim processorId As String = GetProcessorId()
            Dim savedHash As String = GetRegistryValue("ActivationHash")
            Dim currentHash As String = GenerateActivationKey(processorId)

            If savedHash = currentHash Then
                Return True
            End If

            Dim installDateStr As String = GetRegistryValue("InstallDate")
            Dim trialActivated As String = GetRegistryValue("TrialActivated")

            If Not String.IsNullOrEmpty(installDateStr) AndAlso trialActivated = "1" Then
                Dim installDate As DateTime
                If DateTime.TryParse(installDateStr, installDate) Then
                    If (DateTime.Now - installDate).TotalDays <= 30 Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Function

        Private Function GetProcessorId() As String
            Try
                Dim searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
                For Each obj As ManagementObject In searcher.Get()
                    Return obj("ProcessorId")?.ToString()
                Next
                Return "NotFound"
            Catch ex As Exception
                Return "Error"
            End Try
        End Function

        Private Function GenerateActivationKey(processorId As String) As String
            Using sha As SHA256 = SHA256.Create()
                Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(processorId & "CLINIC_SALT"))
                Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
            End Using
        End Function

        Private Function GetRegistryValue(keyName As String) As String
            ' تصحيح المسار
            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\managementclinic")
                If key IsNot Nothing Then
                    Return key.GetValue(keyName, "").ToString()
                End If
                Return ""
            End Using
        End Function
    End Module

    Partial Friend Class MyApplication
        ' يمكن إضافة أحداث إضافية هنا
    End Class
End Namespace