Imports System.IO
Imports System.Drawing

Partial Public Class frmSettings
    Implements IAppliableSettings

    Private ReadOnly _clinicSettingsBal As New ClinicSettingsBAL()
    Private selectedImagePath As String = ""
    Private previewFont As Font
    Private previewBackColor As Color = SystemColors.Control

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyMaterialForm(Me, "إعدادات النظام")
        LoadAppearanceSettings()
        LoadClinicSettings()
    End Sub

    Private Sub LoadAppearanceSettings()
        previewFont = If(My.Settings.appfont IsNot Nothing, My.Settings.appfont, SystemFonts.DefaultFont)
        lblFontPreview.Font = previewFont
        previewBackColor = If(My.Settings.appbackcolor <> Color.Empty, My.Settings.appbackcolor, SystemColors.Control)
        pnlBackColorPreview.BackColor = previewBackColor
        selectedImagePath = My.Settings.AppBackgroundImagePath
        LoadBackgroundPreview()
        chkApplyToAllForms.Checked = True
        chkShowSlideshow.Checked = My.Settings.ShowSlideshowBackground
        chkEnableRtl.Checked = My.Settings.EnableRtl
        ApplyRtlPreview()
    End Sub

    Private Sub LoadClinicSettings()
        Try
            Dim settings = _clinicSettingsBal.GetAllSettings()
            txtClinicName.Text = GetDictValue(settings, ClinicSettingsBAL.KeyClinicName)
            txtCurrency.Text = GetDictValue(settings, ClinicSettingsBAL.KeyDefaultCurrency, "EGP")
            txtVatRate.Text = GetDictValue(settings, ClinicSettingsBAL.KeyDefaultVatRate, "14")
            txtCountryCode.Text = GetDictValue(settings, ClinicSettingsBAL.KeyDefaultCountryCode, "EG")
            txtPatientFilesRoot.Text = GetDictValue(settings, ClinicSettingsBAL.KeyPatientFilesRoot)
            txtMaxFailedAttempts.Text = GetDictValue(settings, ClinicSettingsBAL.KeyMaxFailedLoginAttempts, "5")
            txtLockoutMinutes.Text = GetDictValue(settings, ClinicSettingsBAL.KeyLockoutMinutes, "15")
        Catch ex As Exception
            Logger.LogError($"frmSettings.LoadClinicSettings: {ex.Message}")
            MessageBox.Show("تعذر تحميل إعدادات العيادة. تأكد من تشغيل database-final.sql.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Shared Function GetDictValue(dict As Dictionary(Of String, String), key As String, Optional defaultValue As String = "") As String
        If dict IsNot Nothing AndAlso dict.ContainsKey(key) Then Return dict(key)
        Return defaultValue
    End Function

    Private Sub LoadBackgroundPreview()
        picBackgroundPreview.Image = Nothing
        If String.IsNullOrWhiteSpace(selectedImagePath) OrElse Not File.Exists(selectedImagePath) Then Return
        Try
            Using fs As New FileStream(selectedImagePath, FileMode.Open, FileAccess.Read)
                picBackgroundPreview.Image = Image.FromStream(fs)
            End Using
        Catch ex As Exception
            Logger.LogError($"frmSettings.LoadBackgroundPreview: {ex.Message}")
        End Try
    End Sub

    Private Sub btnChooseFont_Click(sender As Object, e As EventArgs) Handles btnChooseFont.Click
        FontDialog1.Font = previewFont
        If FontDialog1.ShowDialog() = DialogResult.OK Then
            previewFont = FontDialog1.Font
            lblFontPreview.Font = previewFont
        End If
    End Sub

    Private Sub btnChooseBackColor_Click(sender As Object, e As EventArgs) Handles btnChooseBackColor.Click
        ColorDialog1.Color = previewBackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            previewBackColor = ColorDialog1.Color
            pnlBackColorPreview.BackColor = previewBackColor
        End If
    End Sub

    Private Sub btnChooseBackground_Click(sender As Object, e As EventArgs) Handles btnChooseBackground.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            selectedImagePath = OpenFileDialog1.FileName
            LoadBackgroundPreview()
        End If
    End Sub

    Private Sub btnBrowseFilesRoot_Click(sender As Object, e As EventArgs) Handles btnBrowseFilesRoot.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtPatientFilesRoot.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' حفظ إعدادات UI عبر SessionManager
            SessionManager.AppBackColor = previewBackColor
            SessionManager.AppBackgroundImagePath = If(selectedImagePath IsNot Nothing, selectedImagePath, "")
            SessionManager.ShowSlideshowBackground = chkShowSlideshow.Checked
            SessionManager.EnableRtl = chkEnableRtl.Checked
            SessionManager.SaveToSettings()

            Dim clinicSettings As New Dictionary(Of String, String) From {
                {ClinicSettingsBAL.KeyClinicName, txtClinicName.Text.Trim()},
                {ClinicSettingsBAL.KeyDefaultCurrency, txtCurrency.Text.Trim()},
                {ClinicSettingsBAL.KeyDefaultVatRate, txtVatRate.Text.Trim()},
                {ClinicSettingsBAL.KeyDefaultCountryCode, txtCountryCode.Text.Trim()},
                {ClinicSettingsBAL.KeyPatientFilesRoot, txtPatientFilesRoot.Text.Trim()},
                {ClinicSettingsBAL.KeyMaxFailedLoginAttempts, txtMaxFailedAttempts.Text.Trim()},
                {ClinicSettingsBAL.KeyLockoutMinutes, txtLockoutMinutes.Text.Trim()}
            }
            _clinicSettingsBal.SaveSettings(clinicSettings)

            If chkApplyToAllForms.Checked Then
                ApplySettingsToAllForms()
            End If

            MessageBox.Show("تم حفظ الإعدادات بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            Logger.LogError($"frmSettings.btnSave: {ex.Message}")
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplySettingsToAllForms()
        Dim newFont As Font = My.Settings.appfont
        Dim newBackColor As Color = My.Settings.appbackcolor
        Dim bgPath As String = My.Settings.appbackgroundimagepath

        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is IAppliableSettings Then
                DirectCast(frm, IAppliableSettings).ApplySettings(newFont, newBackColor, bgPath)
            Else
                Dim mi = frm.GetType().GetMethod("ApplySettings", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                If mi IsNot Nothing Then
                    mi.Invoke(frm, New Object() {newFont, newBackColor, bgPath})
                End If
            End If
            If My.Settings.EnableRtl Then
                frm.RightToLeft = RightToLeft.Yes
                frm.RightToLeftLayout = True
            End If
        Next
    End Sub

    Public Sub ApplySettings(newFont As Font, newBackColor As Color, backgroundImagePath As String) Implements IAppliableSettings.ApplySettings
        If newFont IsNot Nothing Then Me.Font = newFont
        If newBackColor <> Color.Empty Then Me.BackColor = newBackColor
    End Sub

    Private Sub ApplyRtlPreview()
        If chkEnableRtl.Checked Then
            Me.RightToLeft = RightToLeft.Yes
            Me.RightToLeftLayout = True
        Else
            Me.RightToLeft = RightToLeft.No
            Me.RightToLeftLayout = False
        End If
    End Sub

    Private Sub chkEnableRtl_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableRtl.CheckedChanged
        ApplyRtlPreview()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSettings_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class

Public Interface IAppliableSettings
    Sub ApplySettings(newFont As Font, newBackColor As Color, backgroundImagePath As String)
End Interface
