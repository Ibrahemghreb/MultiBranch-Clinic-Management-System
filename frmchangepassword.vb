''' <summary>
''' فورم تغيير كلمة المرور.
''' عند forceChange=True: لا يمكن إغلاقه إلا بعد التغيير الناجح.
''' عند forceChange=False: يُفتح طوعياً من إعدادات الحساب.
''' </summary>
Public Class frmChangePassword

    Private ReadOnly _userId As Integer
    Private ReadOnly _forceChange As Boolean
    Private ReadOnly _userService As New UserBAL()

    Public Sub New(userId As Integer, Optional forceChange As Boolean = False)
        InitializeComponent()
        _userId = userId
        _forceChange = forceChange
    End Sub

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = If(_forceChange, "تغيير كلمة المرور (إلزامي)", "تغيير كلمة المرور")
        Me.StartPosition = FormStartPosition.CenterParent
        Me.RightToLeft = RightToLeft.Yes

        txtOldPassword.PasswordChar = "*"c
        txtNewPassword.PasswordChar = "*"c
        txtConfirmPassword.PasswordChar = "*"c

        If _forceChange Then
            Me.ControlBox = False
            lblNote.Text =
                "يجب تغيير كلمة المرور قبل استخدام النظام." & Environment.NewLine &
                "الحد الأدنى: 8 أحرف."
            lblNote.ForeColor = Color.DarkRed
        Else
            lblNote.Text = "الحد الأدنى لكلمة المرور: 8 أحرف."
            lblNote.ForeColor = Color.Gray
        End If

        txtOldPassword.Focus()
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Dim oldPass As String = txtOldPassword.Text.Trim()
        Dim newPass As String = txtNewPassword.Text.Trim()
        Dim confirmPass As String = txtConfirmPassword.Text.Trim()

        ' --- التحقق من المدخلات ---
        If String.IsNullOrWhiteSpace(oldPass) Then
            ShowError("يرجى إدخال كلمة المرور الحالية.")
            txtOldPassword.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(newPass) Then
            ShowError("يرجى إدخال كلمة المرور الجديدة.")
            txtNewPassword.Focus()
            Return
        End If

        If newPass.Length < 8 Then
            ShowError("كلمة المرور الجديدة يجب أن تكون 8 أحرف على الأقل.")
            txtNewPassword.Focus()
            Return
        End If

        If newPass = oldPass Then
            ShowError("كلمة المرور الجديدة يجب أن تختلف عن الحالية.")
            txtNewPassword.Focus()
            Return
        End If

        If newPass <> confirmPass Then
            ShowError("كلمة المرور الجديدة وتأكيدها غير متطابقتين.")
            txtConfirmPassword.Clear()
            txtConfirmPassword.Focus()
            Return
        End If

        ' --- التحقق من كلمة المرور الحالية عبر BCrypt مباشرة ---
        ' (لا نستخدم Authenticate حتى لا ندخل في login flow ثاني)
        Try
            If Not _userService.VerifyCurrentPassword(_userId, oldPass) Then
                ShowError("كلمة المرور الحالية غير صحيحة.")
                txtOldPassword.Clear()
                txtOldPassword.Focus()
                Return
            End If

            ' --- حفظ كلمة المرور الجديدة ---
            _userService.SetUserPassword(_userId, newPass, mustChangePassword:=False)
            AuditLogDAL.WriteLog("Update", "Users", _userId, "PasswordChanged")

            MessageBox.Show(
                "تم تغيير كلمة المرور بنجاح.",
                "تم",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As ArgumentException
            ShowError(ex.Message)
        Catch ex As Exception
            ShowError($"حدث خطأ أثناء تغيير كلمة المرور: {ex.Message}")
            Logger.LogError($"frmChangePassword: {ex.Message}")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If _forceChange Then
            Dim confirm = MessageBox.Show(
                "إذا ألغيت الآن، لن تتمكن من الدخول للنظام." & Environment.NewLine &
                "هل تريد الإلغاء وتسجيل الخروج؟",
                "تأكيد الإلغاء",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If confirm = DialogResult.Yes Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        Else
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Shared Sub ShowError(message As String)
        MessageBox.Show(message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

End Class
