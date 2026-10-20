''' <summary>
''' SessionManager — بيانات الجلسة في الذاكرة فقط.
'''
''' المشكلة القديمة:
'''   SaveToSettings() كانت تكتب UserID/Role/DoctorID/BranchID
'''   في My.Settings (ملف user.config على الديسك).
'''   أي مستخدم يعدّل هذا الملف يدوياً يستطيع رفع صلاحياته.
'''
''' الحل:
'''   - بيانات الجلسة تبقى في الذاكرة (Shared Properties) فقط.
'''   - My.Settings تُستخدم فقط لإعدادات الواجهة (لون/خلفية/RTL).
'''   - SaveToSettings/LoadFromSettings تقرأ/تكتب إعدادات UI فقط.
''' </summary>
Public NotInheritable Class SessionManager

    ' ── بيانات الجلسة (ذاكرة فقط — لا تُحفظ على الديسك) ──────────
    Public Shared Property UserID As Integer = 0
    Public Shared Property UserName As String = ""
    Public Shared Property FullName As String = ""
    Public Shared Property Role As String = ""
    Public Shared Property RoleCode As String = ""
    Public Shared Property RoleID As Integer = 0
    Public Shared Property DoctorID As Integer = 0
    Public Shared Property BranchID As Integer = 0

    ' ── إعدادات UI (مسموح بحفظها في My.Settings) ──────────────────
    Public Shared Property AppBackColor As Color = Color.Empty
    Public Shared Property AppBackgroundImagePath As String = ""
    Public Shared Property EnableRtl As Boolean = False
    Public Shared Property ShowSlideshowBackground As Boolean = True

    ' ── تحقق سريع ──────────────────────────────────────────────────
    Public Shared ReadOnly Property IsLoggedIn As Boolean
        Get
            Return UserID > 0 AndAlso Not String.IsNullOrWhiteSpace(UserName)
        End Get
    End Property

    ' ================================================================
    ' SaveToSettings — يحفظ إعدادات UI فقط، لا بيانات الجلسة
    ' ================================================================
    Public Shared Sub SaveToSettings()
        Try
            ' إعدادات UI — مقبول حفظها
            My.Settings.appbackcolor = AppBackColor          ' Color مباشرة
            My.Settings.appbackgroundimagepath = AppBackgroundImagePath
            My.Settings.EnableRtl = EnableRtl
            My.Settings.ShowSlideshowBackground = ShowSlideshowBackground

            ' بيانات الجلسة — نمسحها دائماً من الملف لمنع التلاعب
            My.Settings.userid = ""
            My.Settings.username = ""
            My.Settings.role = ""
            My.Settings.DoctorID = ""
            My.Settings.BranchID = ""

            My.Settings.Save()
        Catch ex As Exception
            Logger.LogError($"SessionManager.SaveToSettings: {ex.Message}")
        End Try
    End Sub

    ' ================================================================
    ' LoadFromSettings — يحمّل إعدادات UI فقط
    ' ================================================================
    Public Shared Sub LoadFromSettings()
        Try
            AppBackColor = My.Settings.appbackcolor          ' Color مباشرة
            AppBackgroundImagePath = If(My.Settings.appbackgroundimagepath, "")
            EnableRtl = My.Settings.EnableRtl
            ShowSlideshowBackground = My.Settings.ShowSlideshowBackground

            ' بيانات الجلسة لا تُحمَّل من الملف أبداً
            ' تبقى كما هي في الذاكرة (صفر/فارغ حتى تسجيل الدخول)
        Catch ex As Exception
            Logger.LogError($"SessionManager.LoadFromSettings: {ex.Message}")
        End Try
    End Sub

    ' ================================================================
    ' ClearSession — مسح بيانات الجلسة من الذاكرة + تنظيف الملف
    ' ================================================================
    Public Shared Sub ClearSession()
        ' مسح الذاكرة
        UserID = 0
        UserName = ""
        FullName = ""
        Role = ""
        RoleCode = ""
        RoleID = 0
        DoctorID = 0
        BranchID = 0

        ' تنظيف أي قيم جلسة قديمة من الملف (إن وجدت)
        Try
            My.Settings.UserID = ""
            My.Settings.username = ""
            My.Settings.Role = ""
            My.Settings.DoctorID = ""
            My.Settings.BranchID = ""
            My.Settings.Save()
        Catch ex As Exception
            Logger.LogError($"SessionManager.ClearSession: {ex.Message}")
        End Try
    End Sub

    ' ================================================================
    ' ValidateSession — للتحقق من صحة الجلسة في أي نقطة بالكود
    ' ================================================================
    Public Shared Function ValidateSession() As Boolean
        If Not IsLoggedIn Then
            MessageBox.Show(
                "انتهت جلستك أو لم تسجّل الدخول. يرجى تسجيل الدخول مجدداً.",
                "جلسة منتهية",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

End Class
