Imports System.IO
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Net.NetworkInformation
Imports System.Configuration
Imports System.Management
Imports Microsoft.Win32
Imports System.Text
Imports System.Security.Cryptography
Public Class FrmMain
    Public Shared SuppressCloseConfirmation As Boolean = False
    Private processorId As String
    Private images As List(Of Image)
    Private currentImageIndex As Integer = 0
    Private adminDashboard As AdminDashboardPanel
    Private slideshowTimer As Timer
    Private lblUsername, lblDate, lblDay, lblKeyboardLang, lblProgramStatus, lblTime, lblDbStatus, lblMemory, lblNetwork, lblCapsLock, lblVersion As ToolStripStatusLabel
    Private lastNetworkCheck As DateTime = DateTime.MinValue
    Private Const NetworkCheckInterval As Integer = 5000 ' تحقق من الشبكة كل 5 ثوانٍ

    Private Sub ClearSession()
        SessionManager.ClearSession()

    End Sub

    Public Sub ApplySettings(newFont As Font, newBackColor As Color, backgroundImagePath As String)
        Try
            ' تطبيق الخط
            If newFont IsNot Nothing Then
                Me.Font = newFont
                ApplyFontToControls(Me, newFont)
            End If

            ' تطبيق لون الخلفية
            If newBackColor <> Color.Empty Then
                Me.BackColor = newBackColor
            Else
                Me.BackColor = SystemColors.Control
            End If

            ' تطبيق صورة الخلفية
            If Me.BackgroundImage IsNot Nothing Then
                Me.BackgroundImage.Dispose()
                Me.BackgroundImage = Nothing
            End If

            If Not String.IsNullOrEmpty(backgroundImagePath) AndAlso File.Exists(backgroundImagePath) Then
                Try
                    Dim bgImage As Image = Image.FromFile(backgroundImagePath)
                    Me.BackgroundImage = bgImage
                    Me.BackgroundImageLayout = ImageLayout.Stretch
                Catch imgEx As Exception
                    MessageBox.Show($"خطأ أثناء تحميل صورة الخلفية '{backgroundImagePath}': {imgEx.Message}",
                                    "خطأ صورة الخلفية", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Logger.LogError($"خطأ تحميل صورة الخلفية: {imgEx.Message}")
                    Me.BackgroundImage = Nothing
                    Me.BackColor = If(newBackColor <> Color.Empty, newBackColor, SystemColors.Control)
                End Try
            Else
                Me.BackgroundImage = Nothing
                Me.BackColor = If(newBackColor <> Color.Empty, newBackColor, SystemColors.Control)
            End If
        Catch ex As Exception
            MessageBox.Show($"حدث خطأ أثناء تطبيق الإعدادات على {Me.Name}: {ex.Message}",
                            "خطأ تطبيق الإعدادات", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ تطبيق الإعدادات: {ex.Message}")
        End Try
    End Sub


    Private Sub ApplyFontToControls(parent As Control, fontToApply As Font)
        For Each ctrl As Control In parent.Controls
            Try
                ctrl.Font = fontToApply
                If ctrl.HasChildren Then
                    ApplyFontToControls(ctrl, fontToApply)
                End If
            Catch ex As Exception
                Logger.LogError($"خطأ تطبيق الخط على عنصر {ctrl.Name}: {ex.Message}")
            End Try
        Next
    End Sub


    Private Function IsDatabaseConnected() As Boolean
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ClinicDBConnection").ConnectionString)
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            Logger.LogError($"خطأ التحقق من اتصال قاعدة البيانات: {ex.Message}")
            Return False
        End Try
    End Function


    Private Function GetProcessorId() As String
        Try
            Dim mbs As New ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor")
            For Each mo As ManagementObject In mbs.Get()
                Return mo("ProcessorId")?.ToString()
            Next
        Catch ex As Exception
            MessageBox.Show($"تعذر الحصول على معرف المعالج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ الحصول على معرف المعالج: {ex.Message}")
        End Try
        Return "UNKNOWN"
    End Function

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

    Private Function GetRegistryValue(keyName As String) As String
        Try
            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\managementclinic")
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

    Private Function IsTrialValid() As Boolean
        Try
            Dim installDate As String = GetRegistryValue("InstallDate")
            Dim installHash As String = GetRegistryValue("InstallHash")
            Dim trialDays As Integer = 30

            If String.IsNullOrEmpty(installDate) OrElse String.IsNullOrEmpty(installHash) Then
                Return False
            End If

            If EncryptString(installDate) <> installHash Then
                Logger.LogError("تم اكتشاف تلاعب في بيانات التجربة")
                Return False
            End If

            Dim installDt As DateTime
            If DateTime.TryParseExact(installDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, installDt) Then
                If DateTime.Now < installDt Then
                    Logger.LogError("تم اكتشاف تلاعب في تاريخ النظام")
                    Return False
                End If
                Return (DateTime.Now - installDt).TotalDays <= trialDays
            End If
            Return False
        Catch ex As Exception
            Logger.LogError($"خطأ التحقق من انتهاء التجربة: {ex.Message}")
            Return False
        End Try
    End Function

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
    Private Sub InitializeAdminDashboard()
        adminDashboard = New AdminDashboardPanel()
        adminDashboard.Dock = DockStyle.Fill

        ' ── ربط كل أحداث Sidebar ────────────────────────────────────
        AddHandler adminDashboard.OpenBranchesRequested, Sub(s, ev) Managementclinic.frmBranches.ShowDialog()
        AddHandler adminDashboard.OpenPatientsRequested, Sub(s, ev) Managementclinic.frmpatients.ShowDialog()
        AddHandler adminDashboard.OpenAppointmentsRequested, Sub(s, ev) Managementclinic.frmappointments.ShowDialog()
        AddHandler adminDashboard.OpenDoctorsRequested, Sub(s, ev) Managementclinic.frmDoctors.ShowDialog()
        AddHandler adminDashboard.OpenUsersRequested, Sub(s, ev) Managementclinic.frmUsers.ShowDialog()
        AddHandler adminDashboard.OpenTreatmentsRequested, Sub(s, ev) Managementclinic.frmDiagnoses.ShowDialog()
        AddHandler adminDashboard.OpenInvoicesRequested, Sub(s, ev)
                                                             Using f As New frmInvoices() : f.ShowDialog() : End Using
                                                         End Sub
        AddHandler adminDashboard.OpenWarehouseRequested, Sub(s, ev)
                                                              Using f As New frmWarehouse() : f.ShowDialog() : End Using
                                                          End Sub
        AddHandler adminDashboard.OpenReportsRequested, Sub(s, ev)
                                                            Using f As New frmFinanceReports() : f.ShowDialog() : End Using
                                                        End Sub
        AddHandler adminDashboard.OpenSettingsRequested, Sub(s, ev) Managementclinic.frmSettings.ShowDialog()
        AddHandler adminDashboard.OpenNotificationsRequested, Sub(s, ev) OpenNotificationsForm()
        AddHandler adminDashboard.OpenRolesRequested, Sub(s, ev) Managementclinic.frmRoles.ShowDialog()
        AddHandler adminDashboard.OpenPermissionsRequested, Sub(s, ev) Managementclinic.frmPermissions.ShowDialog()

        Controls.Add(adminDashboard)
        adminDashboard.BringToFront()
        PictureBox1.SendToBack()
        panelHeader.BringToFront()
        MenuStrip1.BringToFront()
        StatusStrip1.BringToFront()

        Dim dashboardMenu As New ToolStripMenuItem("لوحة التحكم")
        AddHandler dashboardMenu.Click, AddressOf DashboardToolStripMenuItem_Click
        MenuStrip1.Items.Insert(0, dashboardMenu)

        Dim notificationsMenu As New ToolStripMenuItem("الإشعارات")
        AddHandler notificationsMenu.Click, Sub(s, ev) OpenNotificationsForm()
        MenuStrip1.Items.Insert(1, notificationsMenu)

        Dim billingMenu As New ToolStripMenuItem("الفواتير")
        Dim mnuInvoices As New ToolStripMenuItem("الفواتير")
        AddHandler mnuInvoices.Click, Sub(s, ev)
                                          Using form As New frmInvoices()
                                              form.ShowDialog()
                                          End Using
                                      End Sub

        Dim mnuPayments As New ToolStripMenuItem("المدفوعات")
        AddHandler mnuPayments.Click, Sub(s, ev)
                                          Using form As New frmPayments()
                                              form.ShowDialog()
                                          End Using
                                      End Sub

        Dim mnuExpenses As New ToolStripMenuItem("المصروفات")
        AddHandler mnuExpenses.Click, Sub(s, ev)
                                          Using form As New frmExpenses()
                                              form.ShowDialog()
                                          End Using
                                      End Sub

        Dim mnuFinanceReports As New ToolStripMenuItem("التقارير المالية")
        AddHandler mnuFinanceReports.Click, Sub(s, ev)
                                                Using form As New frmFinanceReports()
                                                    form.ShowDialog()
                                                End Using
                                            End Sub

        billingMenu.DropDownItems.AddRange(New ToolStripItem() {mnuInvoices, mnuPayments, mnuExpenses, mnuFinanceReports})
        MenuStrip1.Items.Insert(2, billingMenu)

        Dim calendarMenu As New ToolStripMenuItem("تقويم المواعيد")
        AddHandler calendarMenu.Click, AddressOf ClinicCalendarToolStripMenuItem_Click
        MenuStrip1.Items.Insert(3, calendarMenu)

        Dim switchUserMenu As New ToolStripMenuItem("تبديل المستخدم")
        AddHandler switchUserMenu.Click, AddressOf SwitchUserToolStripMenuItem_Click
        MenuStrip1.Items.Add(switchUserMenu)

        ApplyMenuPermissions()
    End Sub

    Private Sub ApplyMenuPermissions()
        SetMenuVisible(DoctorsToolStripMenuItem, PermissionService.HasAnyPermission("PATIENT_MANAGE", "APPT_MANAGE", "CLINICAL_WRITE"))
        SetMenuVisible(UsersToolStripMenuItem, PermissionService.HasPermission("USER_MANAGE"))
        SetMenuVisible(BranchesToolStripMenuItem, PermissionService.HasPermission("BRANCH_VIEW"))
        SetMenuVisible(PatientsToolStripMenuItem, PermissionService.HasPermission("PATIENT_VIEW"))
        SetMenuVisible(AppointmentsToolStripMenuItem, PermissionService.HasPermission("APPT_VIEW"))
        SetMenuVisible(DiagnosesToolStripMenuItem, PermissionService.HasPermission("CLINICAL_WRITE"))
        SetMenuVisible(CalendarAppointmentItemToolStripMenuItem, PermissionService.HasPermission("REPORT_VIEW"))
    End Sub

    Private Shared Sub SetMenuVisible(item As ToolStripMenuItem, visible As Boolean)
        If item IsNot Nothing Then item.Visible = visible
    End Sub

    Private Sub ApplyMainContentMode()
        If My.Settings.ShowSlideshowBackground AndAlso images IsNot Nothing AndAlso images.Count > 0 Then
            adminDashboard.Visible = False
            PictureBox1.Visible = True
            If slideshowTimer Is Nothing Then
                slideshowTimer = New Timer With {.Interval = 8000}
                AddHandler slideshowTimer.Tick, AddressOf SlideshowTimer_Tick
            End If
            slideshowTimer.Start()
            ShowSlideshowImage()
        Else
            adminDashboard.Visible = True
            PictureBox1.Visible = False
            If slideshowTimer IsNot Nothing Then slideshowTimer.Stop()
        End If
    End Sub

    Private Sub SlideshowTimer_Tick(sender As Object, e As EventArgs)
        If images Is Nothing OrElse images.Count = 0 Then Return
        currentImageIndex = (currentImageIndex + 1) Mod images.Count
        ShowSlideshowImage()
    End Sub

    Private Sub ShowSlideshowImage()
        If images IsNot Nothing AndAlso images.Count > 0 Then
            PictureBox1.Image = images(currentImageIndex)
        End If
    End Sub

    Private Sub DashboardToolStripMenuItem_Click(sender As Object, e As EventArgs)
        adminDashboard.Visible = True
        PictureBox1.Visible = False
        If slideshowTimer IsNot Nothing Then slideshowTimer.Stop()
        adminDashboard.RefreshAll()
    End Sub

    Public Sub RefreshDashboardNotifications(unreadCount As Integer)
        If adminDashboard IsNot Nothing Then
            adminDashboard.SetUnreadNotificationCount(unreadCount)
        End If
    End Sub

    Private Sub OpenNotificationsForm()
        Using frm As New frmNotifications()
            frm.ShowDialog(Me)
        End Using
        UpdateNotificationBadge()
    End Sub

    Private Sub UpdateNotificationBadge()
        Try
            If SessionManager.UserID <= 0 Then Return
            Dim count = New NotificationBAL().GetUnreadCount(SessionManager.UserID)
            RefreshDashboardNotifications(count)
            If lblNotificationsBadge IsNot Nothing Then
                lblNotificationsBadge.Text = If(count > 0, $"إشعارات ({count})", "إشعارات")
            End If
        Catch ex As Exception
            Logger.LogError($"FrmMain.UpdateNotificationBadge: {ex.Message}")
        End Try
    End Sub

    Private lblNotificationsBadge As ToolStripStatusLabel
    Private lblActivationStatus As ToolStripStatusLabel
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' التحقق من حالة التفعيل
            Dim savedHash As String = GetRegistryValue("ActivationHash")
            Dim currentHash As String = GenerateActivationKey(GetProcessorId())

            ' If savedHash <> currentHash AndAlso Not IsTrialValid() Then
            'Dim activationForm As New Activation()
            'Me.Hide()
            'activationForm.ShowDialog()
            'Me.Close()
            'Return
            'End If

            UiTheme.ApplyMaterialForm(Me, "Clinic Management System")
            UiTheme.StyleMenuStrip(MenuStrip1)
            UiTheme.StyleStatusStrip(StatusStrip1)
            UiTheme.StyleHeaderPanel(panelHeader, lblWelcome)

            ApplySettings(My.Settings.appfont, My.Settings.appbackcolor, My.Settings.appbackgroundimagepath)
            Me.MaximizeBox = True
            processorId = GetProcessorId()

            ' تهيئة قائمة الصور
            images = New List(Of Image)()
            Dim imageFolder As String = Path.Combine(Application.StartupPath, "Backgrounds")
            If Directory.Exists(imageFolder) Then
                For Each imgPath In Directory.GetFiles(imageFolder, "*.jpg")
                    Try
                        images.Add(Image.FromFile(imgPath))
                    Catch ex As Exception
                        Logger.LogError($"خطأ تحميل صورة {imgPath}: {ex.Message}")
                    End Try
                Next
            End If
            Dim displayName = If(String.IsNullOrWhiteSpace(SessionManager.FullName), SessionManager.UserName, SessionManager.FullName)
            lblWelcome.Text = "Welcome, " & displayName

            ' تهيئة شريط الحالة
            lblUsername = New ToolStripStatusLabel()
            lblDate = New ToolStripStatusLabel()
            lblDay = New ToolStripStatusLabel()
            lblKeyboardLang = New ToolStripStatusLabel()
            lblProgramStatus = New ToolStripStatusLabel()
            lblTime = New ToolStripStatusLabel()
            lblDbStatus = New ToolStripStatusLabel()
            lblMemory = New ToolStripStatusLabel()
            lblNetwork = New ToolStripStatusLabel()
            lblCapsLock = New ToolStripStatusLabel()
            lblVersion = New ToolStripStatusLabel()

            ' في بداية دالة FrmMain_Load، بعد تعريف التسميات الأخرى
            lblActivationStatus = New ToolStripStatusLabel()
            lblNotificationsBadge = New ToolStripStatusLabel()

            StatusStrip1.Items.AddRange(New ToolStripItem() {lblUsername, lblDate, lblDay, lblKeyboardLang, lblProgramStatus, lblTime, lblDbStatus, lblMemory, lblNetwork, lblCapsLock, lblVersion, lblNotificationsBadge, lblActivationStatus})
            ' تحديث شريط الحالة
            UpdateStatusBar()

            ' إعداد المؤقت
            Dim timer As New Timer()
            timer.Interval = 5000
            AddHandler timer.Tick, Sub()
                                       UpdateStatusBar()
                                   End Sub
            timer.Start()

            InitializeAdminDashboard()
            ApplyMainContentMode()

            Dim notifTimer As New Timer With {.Interval = 60000}
            AddHandler notifTimer.Tick, Sub(s, ev) UpdateNotificationBadge()
            notifTimer.Start()
            UpdateNotificationBadge()

        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحميل النموذج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Logger.LogError($"خطأ تحميل FrmMain: {ex.Message}")
        End Try
    End Sub
    Private Function GetRemainingTrialDays() As Integer
        Try
            Dim installDate As String = GetRegistryValue("InstallDate")
            Dim trialDays As Integer = 30 ' مدة التجربة
            If Not String.IsNullOrEmpty(installDate) Then
                Dim installDt As DateTime
                If DateTime.TryParseExact(installDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, installDt) Then
                    Dim daysPassed As Integer = (DateTime.Now - installDt).TotalDays
                    Return trialDays - daysPassed
                End If
            End If
            Return 0
        Catch ex As Exception
            Logger.LogError($"خطأ حساب المدة المتبقية: {ex.Message}")
            Return 0
        End Try
    End Function
    Private Sub UpdateStatusBar()
        Try
            lblUsername.Text = "المستخدم: " & SessionManager.UserName
            lblDate.Text = "التاريخ: " & DateTime.Now.ToString("yyyy-MM-dd")
            lblDay.Text = "اليوم: " & DateTime.Now.DayOfWeek.ToString()
            lblKeyboardLang.Text = "اللغة: " & InputLanguage.CurrentInputLanguage.Culture.EnglishName
            lblTime.Text = "الوقت: " & DateTime.Now.ToString("HH:mm:ss")
            lblDbStatus.Text = "قاعدة البيانات: " & If(IsDatabaseConnected(), "متصل", "غير متصل")
            lblMemory.Text = "الذاكرة: " & (Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024) & " ميجابايت"
            If (DateTime.Now - lastNetworkCheck).TotalMilliseconds >= NetworkCheckInterval Then
                lblNetwork.Text = "الشبكة: " & If(NetworkInterface.GetIsNetworkAvailable(), "متصل", "غير متصل")
                lastNetworkCheck = DateTime.Now
            End If
            lblCapsLock.Text = "Caps: " & If(Control.IsKeyLocked(Keys.CapsLock), "مفعل", "غير مفعل")
            lblVersion.Text = "الإصدار: " & Application.ProductVersion

            ' تحديث حالة التفعيل
            Dim savedHash As String = GetRegistryValue("ActivationHash")
            Dim currentHash As String = GenerateActivationKey(GetProcessorId())
            If savedHash = currentHash Then
                lblActivationStatus.Text = "حالة التفعيل: مفعل"
            ElseIf IsTrialValid() Then
                Dim remainingDays As Integer = GetRemainingTrialDays()
                lblActivationStatus.Text = $"حالة التفعيل: تجريبي ({remainingDays} أيام متبقية)"
            Else
                lblActivationStatus.Text = "حالة التفعيل: غير مفعل"
            End If
        Catch ex As Exception
            Logger.LogError($"خطأ تحديث شريط الحالة: {ex.Message}")
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        Try
            If images IsNot Nothing AndAlso images.Count > 0 Then
                If Me.BackgroundImage IsNot Nothing Then
                    Me.BackgroundImage.Dispose()
                End If
                currentImageIndex = (currentImageIndex + 1) Mod images.Count
                Me.BackgroundImage = images(currentImageIndex)
                Me.BackgroundImageLayout = ImageLayout.Stretch
            End If
        Catch ex As Exception
            Logger.LogError($"خطأ تغيير صورة الخلفية: {ex.Message}")
        End Try
    End Sub

    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If images IsNot Nothing Then
                For Each img As Image In images
                    img.Dispose()
                Next
                images.Clear()
            End If
            If Me.BackgroundImage IsNot Nothing Then
                Me.BackgroundImage.Dispose()
            End If

            If Not SuppressCloseConfirmation Then
                Dim result As DialogResult = MessageBox.Show("هل أنت متأكد من الخروج؟", "تأكيد الخروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.No Then
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Logger.LogError($"خطأ إغلاق النموذج: {ex.Message}")
        End Try
    End Sub

    ' أحداث القوائم
    Private Sub ReportDoctorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportDoctorsToolStripMenuItem.Click
        Managementclinic.reportdoctors.ShowDialog()
    End Sub

    Private Sub ReportUsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportUsersToolStripMenuItem.Click
        Managementclinic.reportusers.ShowDialog()
    End Sub

    Private Sub ReportBranchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportBranchesToolStripMenuItem.Click
        Managementclinic.ReportBranches.ShowDialog()
    End Sub

    Private Sub ReportPatientsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportPatientsToolStripMenuItem.Click
        Managementclinic.ReportPatients.ShowDialog()
    End Sub

    Private Sub ReportsAppontmentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsAppontmentsToolStripMenuItem.Click
        Managementclinic.reportappointments.ShowDialog()
    End Sub

    Private Sub ReportsDiagnosesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsDiagnosesToolStripMenuItem.Click
        Managementclinic.ReportDiagnoses.ShowDialog()
    End Sub

    Private Sub SettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingToolStripMenuItem.Click
        Managementclinic.frmSettings.ShowDialog()
        frmSettings.BringToFront()
    End Sub

    Private Sub SwitchUserToolStripMenuItem_Click(sender As Object, e As EventArgs)
        AuditLogDAL.WriteLog("Logout", "Users", SessionManager.UserID, SessionManager.UserName)
        ClearSession()
        PermissionService.ClearCache()
        Dim loginForm As New frmLogin()
        loginForm.ShowDialog()
        If SessionManager.UserID = 0 Then
            Me.Close()
        Else
            ApplyMenuPermissions()
            UpdateNotificationBadge()
            adminDashboard?.RefreshAll()
        End If
    End Sub

    Private Sub DoctorsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DoctorsToolStripMenuItem1.Click
        Managementclinic.frmDoctors.ShowDialog()
    End Sub

    Private Sub CalendarAppointmentItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalendarAppointmentItemToolStripMenuItem.Click
        Dim frm As New frmPerformance()
        frm.Show()
    End Sub

    Private Sub ClinicCalendarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frm As New frmClinicCalendar()
        frm.Show()
    End Sub

    Private Sub UsersToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UsersToolStripMenuItem1.Click
        Managementclinic.frmUsers.ShowDialog()
    End Sub

    Private Sub RolesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RolesToolStripMenuItem.Click
        Managementclinic.frmRoles.ShowDialog()
    End Sub

    Private Sub PermissionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PermissionsToolStripMenuItem.Click
        Managementclinic.frmPermissions.ShowDialog()
    End Sub

    Private Sub BranchesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BranchesToolStripMenuItem1.Click
        Managementclinic.frmBranches.ShowDialog()
    End Sub

    Private Sub PatientsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PatientsToolStripMenuItem1.Click
        Managementclinic.frmpatients.ShowDialog()
    End Sub

    Private Sub AppointmentsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AppointmentsToolStripMenuItem1.Click
        Managementclinic.frmappointments.ShowDialog()
    End Sub

    Private Sub DiagnosesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DiagnosesToolStripMenuItem1.Click
        Managementclinic.frmDiagnoses.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Managementclinic.about.ShowDialog()
    End Sub


    Private Sub AppointmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AppointmToolStripMenuItem.Click
        Try
            If SessionManager.UserID = 0 Then
                MessageBox.Show("يرجى تسجيل الدخول أولاً.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Dim loginForm As New frmLogin()
                Me.Hide()
                loginForm.ShowDialog()
                If SessionManager.UserID = 0 Then
                    Me.Close()
                Else
                    Me.Show()
                End If
                Return
            End If

            If Not String.Equals(SessionManager.RoleCode, "DOCTOR", StringComparison.OrdinalIgnoreCase) AndAlso
               Not String.Equals(SessionManager.Role, "Doctor", StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("هذه الوظيفة متاحة فقط للأطباء.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If SessionManager.DoctorID = 0 OrElse SessionManager.BranchID = 0 Then
                MessageBox.Show("بيانات الدكتور أو الفرع غير مكتملة. يرجى التحقق من إعدادات الحساب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SessionManager.ClearSession()
                Dim loginForm As New frmLogin()
                Me.Hide()
                loginForm.ShowDialog()
                If SessionManager.UserID = 0 Then
                    Me.Close()
                Else
                    Me.Show()
                End If
                Return
            End If

            ' التحقق من وجود مرضى مرتبطين
            Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("ClinicDBConnection").ConnectionString)
                Dim patientQuery As String = "SELECT COUNT(*) FROM Patients WHERE DoctorID = @DoctorID AND BranchID = @BranchID"
                Using command As New SqlCommand(patientQuery, connection)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)
                    connection.Open()
                    Dim patientCount As Integer = Convert.ToInt32(command.ExecuteScalar())
                    If patientCount = 0 Then
                        MessageBox.Show("لا يوجد مرضى مسجلين باسمك حاليًا.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                End Using
            End Using

            Dim doctorDashboard As frmDoctorDashboard = Nothing
            For Each form As Form In Application.OpenForms
                If TypeOf form Is frmDoctorDashboard Then
                    doctorDashboard = CType(form, frmDoctorDashboard)
                    Exit For
                End If
            Next

            If doctorDashboard Is Nothing OrElse doctorDashboard.IsDisposed Then
                doctorDashboard = New frmDoctorDashboard()
            End If

            doctorDashboard.ShowDialog()
            Me.Show()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء فتح لوحة تحكم الطبيب: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetUserRole(userId As Integer) As String
        Try
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ClinicDBConnection").ConnectionString)
                Dim query As String = "SELECT Role FROM Users WHERE UserID = @UserID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", userId)
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), "")
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"خطأ الحصول على دور المستخدم: {ex.Message}")
            Return ""
        End Try
    End Function


    Private Sub ActivationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivationToolStripMenuItem.Click
        Dim activationForm As New Activation()
        activationForm.ShowDialog()

    End Sub
    Private Sub FrmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' تحديث حجم PictureBox عند تغيير حجم الفورم
        PictureBox1.Size = Me.ClientSize

    End Sub


End Class


