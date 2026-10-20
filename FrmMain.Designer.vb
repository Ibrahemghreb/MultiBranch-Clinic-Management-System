Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits ReaLTaiizor.Forms.MaterialForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.DoctorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportDoctorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DoctorsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportUsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RolesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PermissionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BranchesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportBranchesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BranchesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportPatientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatientsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsAppontmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmentsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppointmToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiagnosesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsDiagnosesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiagnosesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActivationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalendarAppointmentItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.panelHeader = New System.Windows.Forms.Panel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        Me.panelHeader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.MenuStrip1.ForeColor = System.Drawing.Color.White
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DoctorsToolStripMenuItem, Me.UsersToolStripMenuItem, Me.BranchesToolStripMenuItem, Me.PatientsToolStripMenuItem, Me.AppointmentsToolStripMenuItem, Me.DiagnosesToolStripMenuItem, Me.SettingToolStripMenuItem, Me.AboutToolStripMenuItem, Me.ActivationToolStripMenuItem, Me.CalendarAppointmentItemToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 78)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1274, 31)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DoctorsToolStripMenuItem
        '
        Me.DoctorsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportDoctorsToolStripMenuItem, Me.DoctorsToolStripMenuItem1})
        Me.DoctorsToolStripMenuItem.Name = "DoctorsToolStripMenuItem"
        Me.DoctorsToolStripMenuItem.Size = New System.Drawing.Size(86, 27)
        Me.DoctorsToolStripMenuItem.Text = "Doctors"
        '
        'ReportDoctorsToolStripMenuItem
        '
        Me.ReportDoctorsToolStripMenuItem.Name = "ReportDoctorsToolStripMenuItem"
        Me.ReportDoctorsToolStripMenuItem.Size = New System.Drawing.Size(216, 28)
        Me.ReportDoctorsToolStripMenuItem.Text = "Report Doctors"
        '
        'DoctorsToolStripMenuItem1
        '
        Me.DoctorsToolStripMenuItem1.Name = "DoctorsToolStripMenuItem1"
        Me.DoctorsToolStripMenuItem1.Size = New System.Drawing.Size(216, 28)
        Me.DoctorsToolStripMenuItem1.Text = "Doctors"
        '
        'UsersToolStripMenuItem
        '
        Me.UsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportUsersToolStripMenuItem, Me.RolesToolStripMenuItem, Me.PermissionsToolStripMenuItem, Me.UsersToolStripMenuItem1})
        Me.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem"
        Me.UsersToolStripMenuItem.Size = New System.Drawing.Size(71, 27)
        Me.UsersToolStripMenuItem.Text = " Users"
        '
        'ReportUsersToolStripMenuItem
        '
        Me.ReportUsersToolStripMenuItem.Name = "ReportUsersToolStripMenuItem"
        Me.ReportUsersToolStripMenuItem.Size = New System.Drawing.Size(196, 28)
        Me.ReportUsersToolStripMenuItem.Text = "Report Users"
        '
        'RolesToolStripMenuItem
        '
        Me.RolesToolStripMenuItem.Name = "RolesToolStripMenuItem"
        Me.RolesToolStripMenuItem.Size = New System.Drawing.Size(196, 28)
        Me.RolesToolStripMenuItem.Text = "Roles"
        '
        'PermissionsToolStripMenuItem
        '
        Me.PermissionsToolStripMenuItem.Name = "PermissionsToolStripMenuItem"
        Me.PermissionsToolStripMenuItem.Size = New System.Drawing.Size(196, 28)
        Me.PermissionsToolStripMenuItem.Text = "Permissions"
        '
        'UsersToolStripMenuItem1
        '
        Me.UsersToolStripMenuItem1.Name = "UsersToolStripMenuItem1"
        Me.UsersToolStripMenuItem1.Size = New System.Drawing.Size(196, 28)
        Me.UsersToolStripMenuItem1.Text = "Users"
        '
        'BranchesToolStripMenuItem
        '
        Me.BranchesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportBranchesToolStripMenuItem, Me.BranchesToolStripMenuItem1})
        Me.BranchesToolStripMenuItem.Name = "BranchesToolStripMenuItem"
        Me.BranchesToolStripMenuItem.Size = New System.Drawing.Size(95, 27)
        Me.BranchesToolStripMenuItem.Text = "Branches"
        '
        'ReportBranchesToolStripMenuItem
        '
        Me.ReportBranchesToolStripMenuItem.Name = "ReportBranchesToolStripMenuItem"
        Me.ReportBranchesToolStripMenuItem.Size = New System.Drawing.Size(225, 28)
        Me.ReportBranchesToolStripMenuItem.Text = "Report Branches"
        '
        'BranchesToolStripMenuItem1
        '
        Me.BranchesToolStripMenuItem1.Name = "BranchesToolStripMenuItem1"
        Me.BranchesToolStripMenuItem1.Size = New System.Drawing.Size(225, 28)
        Me.BranchesToolStripMenuItem1.Text = "Branches"
        '
        'PatientsToolStripMenuItem
        '
        Me.PatientsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportPatientsToolStripMenuItem, Me.PatientsToolStripMenuItem1})
        Me.PatientsToolStripMenuItem.Name = "PatientsToolStripMenuItem"
        Me.PatientsToolStripMenuItem.Size = New System.Drawing.Size(88, 27)
        Me.PatientsToolStripMenuItem.Text = "Patients"
        '
        'ReportPatientsToolStripMenuItem
        '
        Me.ReportPatientsToolStripMenuItem.Name = "ReportPatientsToolStripMenuItem"
        Me.ReportPatientsToolStripMenuItem.Size = New System.Drawing.Size(218, 28)
        Me.ReportPatientsToolStripMenuItem.Text = "Report Patients"
        '
        'PatientsToolStripMenuItem1
        '
        Me.PatientsToolStripMenuItem1.Name = "PatientsToolStripMenuItem1"
        Me.PatientsToolStripMenuItem1.Size = New System.Drawing.Size(218, 28)
        Me.PatientsToolStripMenuItem1.Text = "Patients"
        '
        'AppointmentsToolStripMenuItem
        '
        Me.AppointmentsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportsAppontmentsToolStripMenuItem, Me.AppointmentsToolStripMenuItem1, Me.AppointmToolStripMenuItem})
        Me.AppointmentsToolStripMenuItem.Name = "AppointmentsToolStripMenuItem"
        Me.AppointmentsToolStripMenuItem.Size = New System.Drawing.Size(139, 27)
        Me.AppointmentsToolStripMenuItem.Text = "Appointments"
        '
        'ReportsAppontmentsToolStripMenuItem
        '
        Me.ReportsAppontmentsToolStripMenuItem.Name = "ReportsAppontmentsToolStripMenuItem"
        Me.ReportsAppontmentsToolStripMenuItem.Size = New System.Drawing.Size(269, 28)
        Me.ReportsAppontmentsToolStripMenuItem.Text = "Report Appointments"
        '
        'AppointmentsToolStripMenuItem1
        '
        Me.AppointmentsToolStripMenuItem1.Name = "AppointmentsToolStripMenuItem1"
        Me.AppointmentsToolStripMenuItem1.Size = New System.Drawing.Size(269, 28)
        Me.AppointmentsToolStripMenuItem1.Text = "Appointments"
        '
        'AppointmToolStripMenuItem
        '
        Me.AppointmToolStripMenuItem.Name = "AppointmToolStripMenuItem"
        Me.AppointmToolStripMenuItem.Size = New System.Drawing.Size(269, 28)
        Me.AppointmToolStripMenuItem.Text = "Appointments Day"
        '
        'DiagnosesToolStripMenuItem
        '
        Me.DiagnosesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReportsDiagnosesToolStripMenuItem, Me.DiagnosesToolStripMenuItem1})
        Me.DiagnosesToolStripMenuItem.Name = "DiagnosesToolStripMenuItem"
        Me.DiagnosesToolStripMenuItem.Size = New System.Drawing.Size(105, 27)
        Me.DiagnosesToolStripMenuItem.Text = "Diagnoses"
        '
        'ReportsDiagnosesToolStripMenuItem
        '
        Me.ReportsDiagnosesToolStripMenuItem.Name = "ReportsDiagnosesToolStripMenuItem"
        Me.ReportsDiagnosesToolStripMenuItem.Size = New System.Drawing.Size(235, 28)
        Me.ReportsDiagnosesToolStripMenuItem.Text = "Report Diagnoses"
        '
        'DiagnosesToolStripMenuItem1
        '
        Me.DiagnosesToolStripMenuItem1.Name = "DiagnosesToolStripMenuItem1"
        Me.DiagnosesToolStripMenuItem1.Size = New System.Drawing.Size(235, 28)
        Me.DiagnosesToolStripMenuItem1.Text = "Diagnoses"
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(83, 27)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(71, 27)
        Me.AboutToolStripMenuItem.Text = "about"
        '
        'ActivationToolStripMenuItem
        '
        Me.ActivationToolStripMenuItem.Name = "ActivationToolStripMenuItem"
        Me.ActivationToolStripMenuItem.Size = New System.Drawing.Size(106, 27)
        Me.ActivationToolStripMenuItem.Text = "Activation"
        '
        'CalendarAppointmentItemToolStripMenuItem
        '
        Me.CalendarAppointmentItemToolStripMenuItem.Name = "CalendarAppointmentItemToolStripMenuItem"
        Me.CalendarAppointmentItemToolStripMenuItem.Size = New System.Drawing.Size(125, 27)
        Me.CalendarAppointmentItemToolStripMenuItem.Text = "Performance"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.StatusStrip1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Location = New System.Drawing.Point(3, 775)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(8, 0, 13, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1274, 22)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'panelHeader
        '
        Me.panelHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(18, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.panelHeader.Controls.Add(Me.lblWelcome)
        Me.panelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelHeader.Location = New System.Drawing.Point(3, 109)
        Me.panelHeader.Name = "panelHeader"
        Me.panelHeader.Padding = New System.Windows.Forms.Padding(20, 6, 20, 6)
        Me.panelHeader.Size = New System.Drawing.Size(1274, 0)
        Me.panelHeader.TabIndex = 16
        Me.panelHeader.Visible = False
        '
        'lblWelcome
        '
        Me.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(20, 6)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(1234, 0)
        Me.lblWelcome.TabIndex = 15
        Me.lblWelcome.Text = "مرحباً"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 109)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1274, 666)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.panelHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(1024, 640)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinic Management System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.panelHeader.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents DoctorsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportDoctorsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportUsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RolesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BranchesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportBranchesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PatientsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportPatientsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AppointmentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsAppontmentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DiagnosesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsDiagnosesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DoctorsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents BranchesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PatientsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AppointmentsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DiagnosesToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents AppointmToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ActivationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PermissionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblWelcome As Label
    Friend WithEvents panelHeader As Panel
    Friend WithEvents CalendarAppointmentItemToolStripMenuItem As ToolStripMenuItem
End Class
