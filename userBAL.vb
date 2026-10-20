Imports System.Data

Public Class UserBAL
    Private ReadOnly userDal As UserDAL

    Private Shared ReadOnly AllowedRoleCodes As HashSet(Of String) = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        "ADMIN",
        "DOCTOR",
        "RECEPTION",
        "ACCOUNTANT",
        "BRANCHMGR"
    }

    Public Sub New()
        Me.New(New UserDAL())
    End Sub

    Public Sub New(repository As UserDAL)
        userDal = repository
    End Sub

    Public Shared Function GetAllowedRoles() As List(Of String)
        Return New List(Of String) From {
            "Admin",
            "Doctor",
            "Reception",
            "Accountant",
            "BranchManager"
        }
    End Function

    ' ================================================================
    ' يُستدعى من frmLogin لمعرفة هل hash المدير لا يزال placeholder.
    ' لا يُعيد ولا يُظهر أي كلمة مرور.
    ' ================================================================
    Public Function IsAdminPasswordPlaceholder() As Boolean
        Dim admin = userDal.GetLoginUser("admin")
        If admin Is Nothing Then Return False
        Return Not IsValidBcryptHash(admin.PasswordHash)
    End Function

    ' ================================================================
    ' يُستدعى فقط من frmFirstRunSetup بعد أن يُدخل المدير كلمة مروره
    ' الجديدة بنفسه — لا توجد كلمة مرور افتراضية في أي مكان بالكود.
    ' ================================================================
    Public Sub SetAdminPasswordOnFirstRun(newPassword As String)
        If String.IsNullOrWhiteSpace(newPassword) Then
            Throw New ArgumentException("كلمة المرور لا يمكن أن تكون فارغة.")
        End If

        If newPassword.Length < 8 Then
            Throw New ArgumentException("كلمة المرور يجب أن تكون 8 أحرف على الأقل.")
        End If

        Dim admin = userDal.GetLoginUser("admin")
        If admin Is Nothing Then
            Throw New InvalidOperationException("حساب المدير غير موجود في قاعدة البيانات.")
        End If

        ' mustChangePassword = False لأن المدير هو من أدخل الكلمة الآن
        SetUserPassword(admin.UserID, newPassword, mustChangePassword:=False)
    End Sub

    Public Function Authenticate(username As String, password As String) As LoginResult
        Dim result As New LoginResult()

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            result.Status = LoginStatus.InvalidCredentials
            result.Message = "يرجى إدخال اسم المستخدم وكلمة المرور."
            Return result
        End If

        Dim user = userDal.GetLoginUser(username.Trim())
        If user Is Nothing Then
            result.Status = LoginStatus.UserNotFound
            result.Message = "اسم المستخدم غير موجود."
            Return result
        End If

        If Not user.IsActive Then
            result.Status = LoginStatus.AccountInactive
            result.Message = "هذا الحساب غير مفعّل. يرجى مراجعة الإدارة."
            Return result
        End If

        If IsAccountLocked(user) Then
            result.Status = LoginStatus.AccountLocked
            result.Message = BuildLockoutMessage(user)
            Return result
        End If

        If Not IsValidBcryptHash(user.PasswordHash) Then
            result.Status = LoginStatus.InvalidStoredPassword
            result.Message = "كلمة مرور الحساب غير مهيأة. يرجى مراجعة مدير النظام."
            Return result
        End If

        If Not BCrypt.Net.BCrypt.Verify(password.Trim(), user.PasswordHash) Then
            Dim maxAttempts = GetMaxFailedLoginAttempts()
            Dim lockoutMinutes = GetLockoutMinutes()
            userDal.RecordFailedLogin(user.UserID, maxAttempts, lockoutMinutes)
            Try
                Dim notificationBal As New NotificationBAL()
                notificationBal.NotifyFailedLogin(username.Trim())
            Catch
            End Try

            result.Status = LoginStatus.InvalidCredentials
            result.Message = "كلمة المرور غير صحيحة."
            Return result
        End If

        userDal.RecordSuccessfulLogin(user.UserID)

        result.Status = If(user.MustChangePassword, LoginStatus.MustChangePassword, LoginStatus.Success)
        result.UserID = user.UserID
        result.Username = user.Username
        result.FullName = user.FullName
        result.Role = user.RoleName
        result.RoleCode = user.RoleCode
        result.RoleID = user.RoleID
        result.DoctorID = If(user.DoctorID.HasValue, user.DoctorID.Value, 0)
        result.BranchID = If(user.BranchID.HasValue, user.BranchID.Value, 0)
        result.MustChangePassword = user.MustChangePassword

        If result.Status = LoginStatus.MustChangePassword Then
            result.Message = "تم تسجيل الدخول. يجب تغيير كلمة المرور الآن قبل المتابعة."
        Else
            result.Message = "تم تسجيل الدخول بنجاح."
        End If

        Return result
    End Function

    Public Function GetAllUsers() As DataTable
        Return userDal.GetAll()
    End Function

    Public Function GetActiveRoles() As DataTable
        Return userDal.GetActiveRoles()
    End Function

    Public Function GetUserById(userId As Integer) As ClinicUser
        If userId <= 0 Then
            Throw New ArgumentException("رقم المستخدم غير صحيح.")
        End If

        Return userDal.GetById(userId)
    End Function

    Public Sub AddUser(user As ClinicUser)
        ValidateUser(user, requireUserId:=False, requirePassword:=True)

        If userDal.UsernameExists(user.Username) Then
            Throw New InvalidOperationException("اسم المستخدم موجود بالفعل.")
        End If

        ResolveRoleId(user)
        userDal.Insert(user, BCrypt.Net.BCrypt.HashPassword(user.Password))
    End Sub

    Public Sub UpdateUser(user As ClinicUser)
        ValidateUser(user, requireUserId:=True, requirePassword:=False)

        If userDal.UsernameExists(user.Username, user.UserID) Then
            Throw New InvalidOperationException("اسم المستخدم موجود بالفعل لمستخدم آخر.")
        End If

        ResolveRoleId(user)

        Dim passwordHash As String = Nothing
        If Not String.IsNullOrWhiteSpace(user.Password) Then
            passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password.Trim())
        End If

        userDal.Update(user, passwordHash)
    End Sub

    Public Sub DeleteUser(userId As Integer)
        If userId <= 0 Then
            Throw New ArgumentException("الرجاء تحديد مستخدم صالح للحذف.")
        End If

        userDal.Delete(userId)
    End Sub

    Public Sub SetUserPassword(userId As Integer, plainPassword As String, Optional mustChangePassword As Boolean = False)
        If userId <= 0 Then
            Throw New ArgumentException("رقم المستخدم غير صحيح.")
        End If

        If String.IsNullOrWhiteSpace(plainPassword) Then
            Throw New ArgumentException("الرجاء إدخال كلمة المرور.")
        End If

        Dim hash = BCrypt.Net.BCrypt.HashPassword(plainPassword.Trim())
        userDal.SetPasswordHash(userId, hash, mustChangePassword)
    End Sub

    ' ================================================================
    ' الدالة القديمة BootstrapAdminPasswordIfNeeded محذوفة تماماً
    ' لأنها كانت تضع "admin123" كقيمة افتراضية في الكود.
    ' البديل: IsAdminPasswordPlaceholder + SetAdminPasswordOnFirstRun
    ' ================================================================

    Private Shared Function IsAccountLocked(user As LoginUserRecord) As Boolean
        If user.IsLocked Then
            Return True
        End If

        If user.LockoutUntil.HasValue AndAlso user.LockoutUntil.Value > DateTime.Now Then
            Return True
        End If

        Return False
    End Function

    Private Shared Function BuildLockoutMessage(user As LoginUserRecord) As String
        If user.LockoutUntil.HasValue AndAlso user.LockoutUntil.Value > DateTime.Now Then
            Return $"الحساب مقفل مؤقتاً حتى {user.LockoutUntil.Value:yyyy-MM-dd HH:mm}."
        End If

        Return "الحساب مقفل. يرجى مراجعة الإدارة."
    End Function

    Public Shared Function IsValidBcryptHash(passwordHash As String) As Boolean
        Return Not String.IsNullOrWhiteSpace(passwordHash) AndAlso
               passwordHash.StartsWith("$2", StringComparison.Ordinal) AndAlso
               passwordHash.IndexOf("REPLACE_ON_FIRST_DEPLOY", StringComparison.OrdinalIgnoreCase) < 0
    End Function

    Private Function GetMaxFailedLoginAttempts() As Integer
        Dim value = userDal.GetClinicSetting("MaxFailedLoginAttempts")
        Dim attempts As Integer
        If Integer.TryParse(value, attempts) AndAlso attempts > 0 Then
            Return attempts
        End If
        Return 5
    End Function

    Private Function GetLockoutMinutes() As Integer
        Dim value = userDal.GetClinicSetting("LockoutMinutes")
        Dim minutes As Integer
        If Integer.TryParse(value, minutes) AndAlso minutes > 0 Then
            Return minutes
        End If
        Return 15
    End Function

    Private Sub ResolveRoleId(user As ClinicUser)
        If user.RoleID > 0 Then
            Return
        End If

        Dim roleKey = NormalizeRole(user.Role)
        Dim roleId = userDal.GetRoleIdByCodeOrName(roleKey)
        If Not roleId.HasValue Then
            Throw New ArgumentException("الصلاحية المحددة غير موجودة في النظام.")
        End If

        user.RoleID = roleId.Value
    End Sub

    Private Shared Sub ValidateUser(user As ClinicUser, requireUserId As Boolean, requirePassword As Boolean)
        If user Is Nothing Then
            Throw New ArgumentNullException(NameOf(user))
        End If

        If requireUserId AndAlso user.UserID <= 0 Then
            Throw New ArgumentException("رقم المستخدم غير صحيح.")
        End If

        If String.IsNullOrWhiteSpace(user.Username) Then
            Throw New ArgumentException("الرجاء إدخال اسم المستخدم.")
        End If

        If requirePassword AndAlso String.IsNullOrWhiteSpace(user.Password) Then
            Throw New ArgumentException("الرجاء إدخال كلمة المرور.")
        End If

        If String.IsNullOrWhiteSpace(user.Role) AndAlso user.RoleID <= 0 Then
            Throw New ArgumentException("الرجاء تحديد الصلاحية.")
        End If

        user.Username = user.Username.Trim()
        user.Password = If(user.Password, String.Empty).Trim()
        If Not String.IsNullOrWhiteSpace(user.Role) Then
            user.Role = NormalizeRole(user.Role)
        End If
    End Sub

    Private Shared Function NormalizeRole(role As String) As String
        Dim cleanRole = role.Trim()

        Dim mapped = MapRoleAlias(cleanRole)
        If AllowedRoleCodes.Contains(mapped) Then
            Return mapped
        End If

        If AllowedRoleCodes.Contains(cleanRole) Then
            Return cleanRole.ToUpperInvariant()
        End If

        Throw New ArgumentException("الصلاحية غير مدعومة في النظام الحالي.")
    End Function

    Private Shared Function MapRoleAlias(role As String) As String
        Select Case role.ToUpperInvariant()
            Case "ADMIN", "ADMIN "
                Return "ADMIN"
            Case "DOCTOR"
                Return "DOCTOR"
            Case "SECRETARY", "RECEPTION"
                Return "RECEPTION"
            Case "ACCOUNTANT"
                Return "ACCOUNTANT"
            Case "BRANCHMANAGER", "BRANCH MANAGER", "BRANCHMGR"
                Return "BRANCHMGR"
            Case Else
                Return role.ToUpperInvariant()
        End Select
    End Function

    ' ================================================================
    ' التحقق من كلمة المرور الحالية عبر BCrypt مباشرة.
    ' تُستخدم فقط من frmChangePassword — لا تمر بـ login flow.
    ' ================================================================
    Public Function VerifyCurrentPassword(userId As Integer, plainPassword As String) As Boolean
        If userId <= 0 OrElse String.IsNullOrWhiteSpace(plainPassword) Then
            Return False
        End If

        Dim user = userDal.GetPasswordHashById(userId)
        If String.IsNullOrWhiteSpace(user) Then
            Return False
        End If

        If Not IsValidBcryptHash(user) Then
            Return False
        End If

        Return BCrypt.Net.BCrypt.Verify(plainPassword.Trim(), user)
    End Function

End Class
