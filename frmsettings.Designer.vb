Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits ReaLTaiizor.Forms.MaterialForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents tabSettings As TabControl
    Friend WithEvents tabAppearance As TabPage
    Friend WithEvents tabClinic As TabPage
    Friend WithEvents tabSecurity As TabPage
    Friend WithEvents tabSystem As TabPage
    Friend WithEvents lblFontPreview As Label
    Friend WithEvents btnChooseFont As Button
    Friend WithEvents pnlBackColorPreview As Panel
    Friend WithEvents btnChooseBackColor As Button
    Friend WithEvents btnChooseBackground As Button
    Friend WithEvents picBackgroundPreview As PictureBox
    Friend WithEvents txtClinicName As TextBox
    Friend WithEvents txtCurrency As TextBox
    Friend WithEvents txtVatRate As TextBox
    Friend WithEvents txtCountryCode As TextBox
    Friend WithEvents txtPatientFilesRoot As TextBox
    Friend WithEvents btnBrowseFilesRoot As Button
    Friend WithEvents txtMaxFailedAttempts As TextBox
    Friend WithEvents txtLockoutMinutes As TextBox
    Friend WithEvents chkEnableRtl As CheckBox
    Friend WithEvents chkApplyToAllForms As CheckBox
    Friend WithEvents chkShowSlideshow As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        tabSettings = New TabControl()
        tabAppearance = New TabPage()
        tabClinic = New TabPage()
        tabSecurity = New TabPage()
        tabSystem = New TabPage()
        lblFontPreview = New Label()
        btnChooseFont = New Button()
        pnlBackColorPreview = New Panel()
        btnChooseBackColor = New Button()
        btnChooseBackground = New Button()
        picBackgroundPreview = New PictureBox()
        txtClinicName = New TextBox()
        txtCurrency = New TextBox()
        txtVatRate = New TextBox()
        txtCountryCode = New TextBox()
        txtPatientFilesRoot = New TextBox()
        btnBrowseFilesRoot = New Button()
        txtMaxFailedAttempts = New TextBox()
        txtLockoutMinutes = New TextBox()
        chkEnableRtl = New CheckBox()
        chkApplyToAllForms = New CheckBox()
        chkShowSlideshow = New CheckBox()
        btnSave = New Button()
        btnClose = New Button()
        FontDialog1 = New FontDialog()
        ColorDialog1 = New ColorDialog()
        OpenFileDialog1 = New OpenFileDialog()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        tabSettings.SuspendLayout()
        tabAppearance.SuspendLayout()
        tabClinic.SuspendLayout()
        tabSecurity.SuspendLayout()
        tabSystem.SuspendLayout()
        CType(picBackgroundPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()

        tabSettings.Controls.Add(tabAppearance)
        tabSettings.Controls.Add(tabClinic)
        tabSettings.Controls.Add(tabSecurity)
        tabSettings.Controls.Add(tabSystem)
        tabSettings.Dock = DockStyle.Top
        tabSettings.Location = New Point(3, 78)
        tabSettings.Name = "tabSettings"
        tabSettings.Size = New Size(794, 420)
        tabSettings.TabIndex = 0

        tabAppearance.Text = "المظهر"
        tabAppearance.Padding = New Padding(12)
        tabAppearance.Controls.Add(lblFontPreview)
        tabAppearance.Controls.Add(btnChooseFont)
        tabAppearance.Controls.Add(pnlBackColorPreview)
        tabAppearance.Controls.Add(btnChooseBackColor)
        tabAppearance.Controls.Add(btnChooseBackground)
        tabAppearance.Controls.Add(picBackgroundPreview)

        lblFontPreview.BorderStyle = BorderStyle.FixedSingle
        lblFontPreview.Location = New Point(16, 16)
        lblFontPreview.Size = New Size(300, 36)
        lblFontPreview.Text = "معاينة الخط"
        lblFontPreview.TextAlign = ContentAlignment.MiddleCenter

        btnChooseFont.Location = New Point(330, 16)
        btnChooseFont.Size = New Size(140, 36)
        btnChooseFont.Text = "اختيار الخط"

        pnlBackColorPreview.BorderStyle = BorderStyle.FixedSingle
        pnlBackColorPreview.Location = New Point(16, 68)
        pnlBackColorPreview.Size = New Size(300, 36)

        btnChooseBackColor.Location = New Point(330, 68)
        btnChooseBackColor.Size = New Size(140, 36)
        btnChooseBackColor.Text = "لون الخلفية"

        btnChooseBackground.Location = New Point(16, 120)
        btnChooseBackground.Size = New Size(140, 36)
        btnChooseBackground.Text = "صورة الخلفية"

        picBackgroundPreview.BorderStyle = BorderStyle.FixedSingle
        picBackgroundPreview.Location = New Point(170, 120)
        picBackgroundPreview.Size = New Size(300, 120)
        picBackgroundPreview.SizeMode = PictureBoxSizeMode.Zoom

        tabClinic.Text = "العيادة"
        tabClinic.Padding = New Padding(12)
        AddLabel(tabClinic, "اسم العيادة", 16, 20)
        txtClinicName.Location = New Point(160, 16)
        txtClinicName.Size = New Size(280, 27)
        AddLabel(tabClinic, "العملة", 16, 60)
        txtCurrency.Location = New Point(160, 56)
        txtCurrency.Size = New Size(120, 27)
        AddLabel(tabClinic, "VAT %", 300, 60)
        txtVatRate.Location = New Point(360, 56)
        txtVatRate.Size = New Size(80, 27)
        AddLabel(tabClinic, "رمز الدولة", 16, 100)
        txtCountryCode.Location = New Point(160, 96)
        txtCountryCode.Size = New Size(120, 27)
        AddLabel(tabClinic, "مسار ملفات المرضى", 16, 140)
        txtPatientFilesRoot.Location = New Point(160, 136)
        txtPatientFilesRoot.Size = New Size(280, 27)
        btnBrowseFilesRoot.Location = New Point(450, 136)
        btnBrowseFilesRoot.Size = New Size(80, 27)
        btnBrowseFilesRoot.Text = "..."
        tabClinic.Controls.Add(txtClinicName)
        tabClinic.Controls.Add(txtCurrency)
        tabClinic.Controls.Add(txtVatRate)
        tabClinic.Controls.Add(txtCountryCode)
        tabClinic.Controls.Add(txtPatientFilesRoot)
        tabClinic.Controls.Add(btnBrowseFilesRoot)

        tabSecurity.Text = "الأمان"
        tabSecurity.Padding = New Padding(12)
        AddLabel(tabSecurity, "محاولات فاشلة قبل القفل", 16, 20)
        txtMaxFailedAttempts.Location = New Point(220, 16)
        txtMaxFailedAttempts.Size = New Size(80, 27)
        AddLabel(tabSecurity, "مدة القفل (دقيقة)", 16, 60)
        txtLockoutMinutes.Location = New Point(220, 56)
        txtLockoutMinutes.Size = New Size(80, 27)
        tabSecurity.Controls.Add(txtMaxFailedAttempts)
        tabSecurity.Controls.Add(txtLockoutMinutes)

        tabSystem.Text = "النظام"
        tabSystem.Padding = New Padding(12)
        chkEnableRtl.Location = New Point(16, 20)
        chkEnableRtl.Text = "تفعيل واجهة RTL (عربي)"
        chkEnableRtl.AutoSize = True
        chkApplyToAllForms.Location = New Point(16, 56)
        chkApplyToAllForms.Text = "تطبيق الإعدادات على جميع النماذج المفتوحة"
        chkApplyToAllForms.AutoSize = True
        chkShowSlideshow.Location = New Point(16, 92)
        chkShowSlideshow.Text = "عرض خلفية متحركة في الشاشة الرئيسية"
        chkShowSlideshow.AutoSize = True
        tabSystem.Controls.Add(chkEnableRtl)
        tabSystem.Controls.Add(chkApplyToAllForms)
        tabSystem.Controls.Add(chkShowSlideshow)

        btnSave.Location = New Point(560, 510)
        btnSave.Size = New Size(110, 40)
        btnSave.Text = "حفظ"
        btnClose.Location = New Point(440, 510)
        btnClose.Size = New Size(110, 40)
        btnClose.Text = "إغلاق"

        OpenFileDialog1.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 560)
        Controls.Add(btnClose)
        Controls.Add(btnSave)
        Controls.Add(tabSettings)
        MaximizeBox = False
        Name = "frmSettings"
        StartPosition = FormStartPosition.CenterScreen
        Text = "إعدادات النظام"

        tabSettings.ResumeLayout(False)
        tabAppearance.ResumeLayout(False)
        tabClinic.ResumeLayout(False)
        tabClinic.PerformLayout()
        tabSecurity.ResumeLayout(False)
        tabSecurity.PerformLayout()
        tabSystem.ResumeLayout(False)
        tabSystem.PerformLayout()
        CType(picBackgroundPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Private Sub AddLabel(parent As Control, text As String, x As Integer, y As Integer)
        Dim lbl As New Label With {
            .Text = text,
            .Location = New Point(x, y + 4),
            .AutoSize = True
        }
        parent.Controls.Add(lbl)
    End Sub
End Class
