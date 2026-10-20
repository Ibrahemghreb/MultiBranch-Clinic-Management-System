Public Class ClinicSettingsBAL
    Private ReadOnly _dal As New ClinicSettingsDAL()

    Public Const KeyClinicName As String = "ClinicName"
    Public Const KeyDefaultCurrency As String = "DefaultCurrency"
    Public Const KeyDefaultVatRate As String = "DefaultVatRate"
    Public Const KeyDefaultCountryCode As String = "DefaultCountryCode"
    Public Const KeyMaxFailedLoginAttempts As String = "MaxFailedLoginAttempts"
    Public Const KeyLockoutMinutes As String = "LockoutMinutes"
    Public Const KeyPatientFilesRoot As String = "PatientFilesRoot"

    Public Function GetAllSettings() As Dictionary(Of String, String)
        Return _dal.GetAllSettings()
    End Function

    Public Function GetSetting(key As String, Optional defaultValue As String = "") As String
        Dim value = _dal.GetSetting(key)
        Return If(String.IsNullOrWhiteSpace(value), defaultValue, value)
    End Function

    Public Sub SaveSettings(settings As Dictionary(Of String, String))
        ValidateSettings(settings)
        Dim updatedBy As Integer = SessionManager.UserID
        _dal.SaveSettings(settings, updatedBy)
    End Sub

    Public Sub ValidateSettings(settings As Dictionary(Of String, String))
        If settings.ContainsKey(KeyDefaultVatRate) Then
            Dim vat As Decimal
            If Not Decimal.TryParse(settings(KeyDefaultVatRate), vat) OrElse vat < 0 OrElse vat > 100 Then
                Throw New ArgumentException("نسبة ضريبة القيمة المضافة يجب أن تكون بين 0 و 100.")
            End If
        End If

        If settings.ContainsKey(KeyMaxFailedLoginAttempts) Then
            Dim attempts As Integer
            If Not Integer.TryParse(settings(KeyMaxFailedLoginAttempts), attempts) OrElse attempts < 1 OrElse attempts > 20 Then
                Throw New ArgumentException("عدد محاولات تسجيل الدخول الفاشلة يجب أن يكون بين 1 و 20.")
            End If
        End If

        If settings.ContainsKey(KeyLockoutMinutes) Then
            Dim minutes As Integer
            If Not Integer.TryParse(settings(KeyLockoutMinutes), minutes) OrElse minutes < 1 OrElse minutes > 1440 Then
                Throw New ArgumentException("مدة القفل يجب أن تكون بين 1 و 1440 دقيقة.")
            End If
        End If

        If settings.ContainsKey(KeyPatientFilesRoot) AndAlso Not String.IsNullOrWhiteSpace(settings(KeyPatientFilesRoot)) Then
            Dim path = settings(KeyPatientFilesRoot).Trim()
            If path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0 Then
                Throw New ArgumentException("مسار ملفات المرضى غير صالح.")
            End If
        End If
    End Sub
End Class
