Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports System.Text
Imports System.Management
Imports System.Globalization

' نموذج تفعيل البرنامج
Public Class Activation
    Private trialDays As Integer = 30
    Private regKey As String = "Software\managementclinic"
    Private processorId As String

    ''' <summary>
    ''' Initializes the activation form.
    ''' </summary>
    Private Sub Activation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            processorId = GetProcessorId()
            txtProcessorID.Text = processorId
            CheckActivationStatus()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحميل نموذج التفعيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ تحميل Activation: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Gets the processor ID for activation.
    ''' </summary>
    Private Function GetProcessorId() As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
            For Each obj As ManagementObject In searcher.Get()
                Return obj("ProcessorId")?.ToString()
            Next
            Return "NotFound"
        Catch ex As Exception
            MessageBox.Show($"خطأ الحصول على معرف المعالج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ الحصول على معرف المعالج: {ex.Message}")
            Return "Error: " & ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Checks the activation status and decides whether to show the main form.
    ''' </summary>
    Private Sub CheckActivationStatus()
        Try
            Dim savedHash As String = GetRegistryValue("ActivationHash")
            Dim currentHash As String = GenerateActivationKey(processorId)

            If savedHash = currentHash Then
                lblStatus.Text = "البرنامج مفعل"
                ShowMainForm()
            ElseIf IsTrialActivated() AndAlso Not IsTrialExpired() Then
                lblStatus.Text = "النسخة التجريبية مفعلة حتى " & GetTrialEndDate().ToShortDateString()
                ShowMainForm()
            ElseIf IsTrialExpired() Then
                lblStatus.Text = "انتهت الفترة التجريبية - يرجى الاتصال بالدعم الفني"
            Else
                lblStatus.Text = "البرنامج غير مفعل"
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ التحقق من حالة التفعيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ التحقق من التفعيل: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Generates an activation key based on the processor ID.
    ''' </summary>
    Private Function GenerateActivationKey(processorId As String) As String
        Try
            Using sha As SHA256 = SHA256.Create()
                Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(processorId & "CLINIC_SALT"))
                Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
            End Using
        Catch ex As Exception
            Logger.LogError($"خطأ توليد مفتاح التفعيل: {ex.Message}")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Encrypts a string using SHA256.
    ''' </summary>
    Private Function EncryptString(input As String) As String
        Try
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
            Using sha As SHA256 = SHA256.Create()
                Dim hash As Byte() = sha.ComputeHash(bytes)
                Return BitConverter.ToString(hash).Replace("-", "").ToLower()
            End Using
        Catch ex As Exception
            Logger.LogError($"خطأ تشفير النص: {ex.Message}")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Handles the activation button click to activate trial or license.
    ''' </summary>
    Private Sub btnActivate_Click(sender As Object, e As EventArgs) Handles btnActivate.Click
        Try
            If rdbTrial.Checked Then
                ActivateTrial()
            ElseIf rdbLicense.Checked Then
                ValidateLicense()
            Else
                MessageBox.Show("من فضلك اختر نوع التفعيل أولاً (نسخة تجريبية أو مفتاح تفعيل)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء التفعيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ التفعيل: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Activates the trial version.
    ''' </summary>
    Private Sub ActivateTrial()
        Try
            Dim installDate As String = DateTime.Now.ToString("yyyy-MM-dd")
            SaveToRegistry("InstallDate", installDate)
            SaveToRegistry("InstallHash", EncryptString(installDate))
            SaveToRegistry("TrialActivated", "1")
            lblStatus.Text = "تم تفعيل النسخة التجريبية حتى " & DateTime.Now.AddDays(trialDays).ToShortDateString()
            Logger.LogError("تم تفعيل النسخة التجريبية")
            ShowMainForm()
        Catch ex As Exception
            MessageBox.Show($"خطأ تفعيل النسخة التجريبية: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ تفعيل النسخة التجريبية: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Validates the license key entered by the user.
    ''' </summary>
    Private Sub ValidateLicense()
        Try
            Dim userKey As String = txtActivationKey.Text.Trim().ToLower()
            Dim validKey As String = GenerateActivationKey(processorId)

            If userKey = validKey Then
                SaveToRegistry("ActivationHash", validKey)
                lblStatus.Text = "تم التفعيل بنجاح!"
                Logger.LogError("تم تفعيل البرنامج بمفتاح ترخيص")
                ShowMainForm()
            Else
                MessageBox.Show("مفتاح التفعيل غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Logger.LogError("محاولة تفعيل بمفتاح غير صحيح")
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ التحقق من مفتاح التفعيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ التحقق من مفتاح التفعيل: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Checks if the trial version is activated.
    ''' </summary>
    Private Function IsTrialActivated() As Boolean
        Return GetRegistryValue("TrialActivated") = "1"
    End Function

    ''' <summary>
    ''' Gets the trial end date.
    ''' </summary>
    Private Function GetTrialEndDate() As Date
        Try
            Dim installDateStr As String = GetRegistryValue("InstallDate")
            If Not String.IsNullOrEmpty(installDateStr) Then
                Dim installDt As Date
                If Date.TryParseExact(installDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, installDt) Then
                    Return installDt.AddDays(trialDays)
                End If
            End If
            Return DateTime.MinValue
        Catch ex As Exception
            Logger.LogError($"خطأ الحصول على تاريخ انتهاء التجربة: {ex.Message}")
            Return DateTime.MinValue
        End Try
    End Function

    ''' <summary>
    ''' Checks if the trial period has expired.
    ''' </summary>
    Private Function IsTrialExpired() As Boolean
        Try
            Dim installDate As String = GetRegistryValue("InstallDate")
            Dim installHash As String = GetRegistryValue("InstallHash")

            If String.IsNullOrEmpty(installDate) OrElse String.IsNullOrEmpty(installHash) Then
                Return False
            End If

            If EncryptString(installDate) <> installHash Then
                Logger.LogError("تم اكتشاف تلاعب في بيانات التجربة")
                Return True
            End If

            Dim installDt As DateTime
            If DateTime.TryParseExact(installDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, installDt) Then
                If DateTime.Now < installDt Then
                    Logger.LogError("تم اكتشاف تلاعب في تاريخ النظام")
                    Return True
                End If
                Return (DateTime.Now - installDt).TotalDays > trialDays
            End If
            Return False
        Catch ex As Exception
            Logger.LogError($"خطأ التحقق من انتهاء التجربة: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Saves a value to the registry.
    ''' </summary>
    Private Sub SaveToRegistry(keyName As String, value As String)
        Try
            Using key As RegistryKey = Registry.CurrentUser.CreateSubKey(regKey)
                If key IsNot Nothing Then
                    key.SetValue(keyName, value)
                Else
                    MessageBox.Show("فشل إنشاء مفتاح الريجيستري. تحقق من الأذونات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Logger.LogError("فشل إنشاء مفتاح الريجيستري")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"خطأ حفظ البيانات في الريجيستري: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ حفظ البيانات في الريجيستري: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Retrieves a value from the registry.
    ''' </summary>
    Private Function GetRegistryValue(keyName As String) As String
        Try
            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(regKey)
                If key IsNot Nothing Then
                    Return key.GetValue(keyName, "").ToString()
                End If
                Return ""
            End Using
        Catch ex As Exception
            Logger.LogError($"خطأ قراءة البيانات من الريجيستري: {ex.Message}")
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Copies the processor ID to the clipboard.
    ''' </summary>
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btncopy.Click
        Try
            Clipboard.SetText(txtProcessorID.Text)
            MessageBox.Show("تم نسخ معرف المعالج إلى الحافظة.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"خطأ نسخ معرف المعالج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ نسخ معرف المعالج: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Shows the login form and closes the activation form.
    ''' </summary>
    Private Sub ShowMainForm()
        Try
            ' إغلاق أي نسخ مفتوحة من frmLogin
            For Each form As Form In Application.OpenForms
                If TypeOf form Is frmLogin Then
                    form.Close()
                End If
            Next

            Dim loginForm As New frmLogin()
            Me.Hide()
            loginForm.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"خطأ فتح نموذج تسجيل الدخول: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ فتح نموذج تسجيل الدخول: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Exits the application.
    ''' </summary>
    Private Sub btnend_Click(sender As Object, e As EventArgs) Handles btnend.Click
        Application.Exit()
    End Sub
End Class