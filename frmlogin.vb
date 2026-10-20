Imports ReaLTaiizor

Public Class frmLogin
    Public Property IsInitialLogin As Boolean = False

    Private ReadOnly userService As New UserBAL()

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UiTheme.ApplyMaterialForm(Me, "System Clinic - Login")
        txtusername.Text = ""
        txtPassword.Text = ""
        txtusername.Focus()
        SessionManager.ClearSession()

        ' ======================================================
        ' أول تشغيل: لو hash المدير placeholder → افتح فورم
        ' الإعداد الأولي لتعيين كلمة مرور قوية.
        ' لا يُعرض اسم مستخدم أو كلمة مرور في أي رسالة.
        ' ======================================================
        Try
            If userService.IsAdminPasswordPlaceholder() Then
                Dim setupForm As New frmFirstRunSetup()
                Dim result As DialogResult = setupForm.ShowDialog(Me)

                If result <> DialogResult.OK Then
                    ' المستخدم أغلق نافذة الإعداد بدون تعيين كلمة مرور → إغلاق التطبيق
                    MessageBox.Show(
                        "لم يتم إعداد كلمة مرور المدير. سيتم إغلاق البرنامج.",
                        "إعداد مطلوب",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                    Application.Exit()
                    Return
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(
                $"تعذّر التحقق من إعداد المدير: {ex.Message}",
                "تحذير",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Try
            Dim loginResult = userService.Authenticate(txtusername.Text, txtPassword.Text)

            If Not loginResult.IsSuccess Then
                MessageBox.Show(loginResult.Message, "تسجيل الدخول", MessageBoxButtons.OK, GetMessageIcon(loginResult.Status))
                SessionManager.ClearSession()
                Return
            End If

            ApplySession(loginResult)

            ' إذا كانت كلمة المرور تستوجب التغيير → افتح فورم التغيير إلزامياً
            If loginResult.Status = LoginStatus.MustChangePassword Then
                Dim changeForm As New frmChangePassword(loginResult.UserID, forceChange:=True)
                Dim changeResult As DialogResult = changeForm.ShowDialog(Me)

                If changeResult <> DialogResult.OK Then
                    ' المستخدم أغلق النافذة بدون تغيير كلمة المرور → سجّل خروج
                    SessionManager.ClearSession()
                    Return
                End If
            End If

            ' إغلاق أي فورم رئيسية مفتوحة قبل فتح الجديدة
            CloseOpenMainForms()

            If IsDoctorRole(loginResult) Then
                If loginResult.DoctorID = 0 OrElse loginResult.BranchID = 0 Then
                    MessageBox.Show(
                        "بيانات حساب الطبيب غير مكتملة (الطبيب أو الفرع). يرجى ربط الطبيب بفرع في قاعدة البيانات.",
                        "خطأ في الحساب",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
                    SessionManager.ClearSession()
                    Return
                End If

                OpenFormAndCloseLogin(New frmDoctorDashboard())
            Else
                OpenFormAndCloseLogin(New FrmMain())
            End If
        Catch ex As Exception
            MessageBox.Show($"حدث خطأ غير متوقع: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SessionManager.ClearSession()
        End Try
    End Sub

    Private Shared Sub ApplySession(loginResult As LoginResult)
        SessionManager.UserID = loginResult.UserID
        SessionManager.UserName = loginResult.Username
        SessionManager.FullName = loginResult.FullName
        SessionManager.Role = loginResult.Role
        SessionManager.RoleCode = loginResult.RoleCode
        SessionManager.RoleID = loginResult.RoleID
        SessionManager.DoctorID = loginResult.DoctorID
        SessionManager.BranchID = loginResult.BranchID
        SessionManager.SaveToSettings()
        PermissionService.ClearCache()
        AuditLogDAL.WriteLog("Login", "Users", loginResult.UserID, loginResult.Username)
    End Sub

    Private Shared Function IsDoctorRole(loginResult As LoginResult) As Boolean
        Return String.Equals(loginResult.RoleCode, "DOCTOR", StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(loginResult.Role, "Doctor", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Sub CloseOpenMainForms()
        Dim openForms = Application.OpenForms.Cast(Of Form)().ToList()
        Dim hasMainForm As Boolean = False
        For Each form In openForms
            If form IsNot Nothing AndAlso (TypeOf form Is FrmMain OrElse TypeOf form Is frmDoctorDashboard) Then
                hasMainForm = True
                Exit For
            End If
        Next
        If Not hasMainForm Then Return

        FrmMain.SuppressCloseConfirmation = True
        For Each form In openForms
            If form IsNot Nothing AndAlso (TypeOf form Is FrmMain OrElse TypeOf form Is frmDoctorDashboard) Then
                form.Close()
            End If
        Next
        FrmMain.SuppressCloseConfirmation = False
    End Sub

    Private Sub OpenFormAndCloseLogin(target As Form)
        Me.Hide()
        target.ShowDialog()
        Me.Close()
    End Sub

    Private Shared Function GetMessageIcon(status As LoginStatus) As MessageBoxIcon
        Select Case status
            Case LoginStatus.AccountLocked, LoginStatus.AccountInactive, LoginStatus.InvalidStoredPassword
                Return MessageBoxIcon.Warning
            Case LoginStatus.UserNotFound, LoginStatus.InvalidCredentials
                Return MessageBoxIcon.Error
            Case Else
                Return MessageBoxIcon.Information
        End Select
    End Function

    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Application.Exit()
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            btnlogin.PerformClick()
        End If
    End Sub
End Class
