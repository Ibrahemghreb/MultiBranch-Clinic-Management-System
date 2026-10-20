''' <summary>
''' فورم الإعداد الأولي — يظهر مرة واحدة فقط عند أول تشغيل للبرنامج
''' عندما يكون hash المدير placeholder في قاعدة البيانات.
''' المدير يختار كلمة مروره بنفسه — لا توجد كلمة مرور افتراضية.
'''
''' طريقة الاستخدام في المشروع:
'''   1- أضف فورم جديد باسم frmFirstRunSetup (Add → Windows Form)
'''   2- ضع العناصر في الـ Designer بنفس الأسماء:
'''        lblTitle, lblInstructions, txtNewPassword, txtConfirmPassword, btnSetPassword
'''   3- احذف أي InitializeComponent من هذا الملف
''' </summary>
Public Class frmFirstRunSetup

    Private ReadOnly _userService As New UserBAL()

    Private Sub frmFirstRunSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "الإعداد الأولي للنظام"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.RightToLeft = RightToLeft.Yes
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ControlBox = False   ' يمنع الإغلاق بزر X — الإعداد إلزامي
        Me.Size = New Size(500, 330)

        lblTitle.Text = "مرحباً بك في نظام إدارة العيادة"
        lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.TextAlign = ContentAlignment.MiddleCenter

        lblInstructions.Text =
            "هذا أول تشغيل للبرنامج." & Environment.NewLine &
            "يرجى تعيين كلمة مرور قوية لحساب المدير (admin)." & Environment.NewLine &
            "الحد الأدنى: 8 أحرف."

        txtNewPassword.PasswordChar = "*"c
        txtConfirmPassword.PasswordChar = "*"c
        txtNewPassword.Focus()
    End Sub

    Private Sub btnSetPassword_Click(sender As Object, e As EventArgs) Handles btnSetPassword.Click
        Dim newPass As String = txtNewPassword.Text.Trim()
        Dim confirmPass As String = txtConfirmPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(newPass) Then
            ShowError("يرجى إدخال كلمة المرور.")
            txtNewPassword.Focus()
            Return
        End If

        If newPass.Length < 8 Then
            ShowError("كلمة المرور يجب أن تكون 8 أحرف على الأقل.")
            txtNewPassword.Focus()
            Return
        End If

        If newPass <> confirmPass Then
            ShowError("كلمة المرور وتأكيدها غير متطابقتين.")
            txtConfirmPassword.Clear()
            txtConfirmPassword.Focus()
            Return
        End If

        Try
            _userService.SetAdminPasswordOnFirstRun(newPass)

            MessageBox.Show(
                "تم تعيين كلمة مرور المدير بنجاح." & Environment.NewLine &
                "اسم المستخدم: admin",
                "تم الإعداد",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As ArgumentException
            ShowError(ex.Message)
        Catch ex As Exception
            ShowError($"حدث خطأ أثناء حفظ كلمة المرور: {ex.Message}")
            Logger.LogError($"frmFirstRunSetup.SetPassword: {ex.Message}")
        End Try
    End Sub

    Private Shared Sub ShowError(message As String)
        MessageBox.Show(message, "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

End Class
