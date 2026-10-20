Imports ReaLTaiizor.Forms

Public Class frmAdminDashboard
    Inherits MaterialForm

    Private ReadOnly dashboardPanel As New AdminDashboardPanel()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        dashboardPanel.Dock = DockStyle.Fill
        Me.Controls.Add(dashboardPanel)
        Me.ClientSize = New Size(1280, 800)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "لوحة التحكم"
        Me.ResumeLayout(False)
    End Sub

    Private Sub frmAdminDashboard_Load(sender As Object, e As EventArgs) Handles Me.Load
        UiTheme.ApplyMaterialForm(Me, "لوحة التحكم — إدارة العيادة")
        WireSidebarEvents()
        BuildMenuStrip()
    End Sub

    Private Sub WireSidebarEvents()
        AddHandler dashboardPanel.OpenBranchesRequested, Sub(s, ev) Managementclinic.frmBranches.ShowDialog()
        AddHandler dashboardPanel.OpenPatientsRequested, Sub(s, ev) Managementclinic.frmpatients.ShowDialog()
        AddHandler dashboardPanel.OpenAppointmentsRequested, Sub(s, ev) Managementclinic.frmappointments.ShowDialog()
        AddHandler dashboardPanel.OpenDoctorsRequested, Sub(s, ev) Managementclinic.frmDoctors.ShowDialog()
        AddHandler dashboardPanel.OpenUsersRequested, Sub(s, ev) Managementclinic.frmUsers.ShowDialog()
        AddHandler dashboardPanel.OpenRolesRequested, Sub(s, ev)
                                                          Using f As New frmRoles() : f.ShowDialog() : End Using
                                                      End Sub
        AddHandler dashboardPanel.OpenPermissionsRequested, Sub(s, ev)
                                                                Using f As New frmPermissions() : f.ShowDialog() : End Using
                                                            End Sub
        AddHandler dashboardPanel.OpenTreatmentsRequested, Sub(s, ev) Managementclinic.frmDiagnoses.ShowDialog()
        AddHandler dashboardPanel.OpenInvoicesRequested, Sub(s, ev)
                                                             Using f As New frmInvoices() : f.ShowDialog() : End Using
                                                         End Sub
        AddHandler dashboardPanel.OpenWarehouseRequested, Sub(s, ev)
                                                              Using f As New frmWarehouse() : f.ShowDialog() : End Using
                                                          End Sub
        AddHandler dashboardPanel.OpenReportsRequested, Sub(s, ev)
                                                            Using f As New frmFinanceReports() : f.ShowDialog() : End Using
                                                        End Sub
        AddHandler dashboardPanel.OpenSettingsRequested, Sub(s, ev) Managementclinic.frmSettings.ShowDialog()
        AddHandler dashboardPanel.OpenNotificationsRequested, Sub(s, ev)
                                                                  Using f As New frmNotifications() : f.ShowDialog() : End Using
                                                              End Sub
    End Sub

    Private Sub BuildMenuStrip()
        Dim ms As New MenuStrip()
        ms.BackColor = Color.FromArgb(10, 18, 38)
        ms.ForeColor = Color.White
        ms.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        ms.Padding = New Padding(5, 2, 0, 2)

        ' Doctors
        Dim mnuDoctors As New ToolStripMenuItem("Doctors")
        Dim mnuReportDoctors As New ToolStripMenuItem("Report Doctors")
        AddHandler mnuReportDoctors.Click, Sub(s, ev) Managementclinic.reportdoctors.ShowDialog()
        Dim mnuDoctorsList As New ToolStripMenuItem("Doctors")
        AddHandler mnuDoctorsList.Click, Sub(s, ev) Managementclinic.frmDoctors.ShowDialog()
        mnuDoctors.DropDownItems.AddRange(New ToolStripItem() {mnuReportDoctors, mnuDoctorsList})

        ' Users
        Dim mnuUsers As New ToolStripMenuItem("Users")
        Dim mnuReportUsers As New ToolStripMenuItem("Report Users")
        AddHandler mnuReportUsers.Click, Sub(s, ev) Managementclinic.reportusers.ShowDialog()
        Dim mnuRoles As New ToolStripMenuItem("Roles")
        AddHandler mnuRoles.Click, Sub(s, ev)
                                       Using f As New frmRoles() : f.ShowDialog() : End Using
                                   End Sub
        Dim mnuPermissions As New ToolStripMenuItem("Permissions")
        AddHandler mnuPermissions.Click, Sub(s, ev)
                                             Using f As New frmPermissions() : f.ShowDialog() : End Using
                                         End Sub
        Dim mnuUsersList As New ToolStripMenuItem("Users")
        AddHandler mnuUsersList.Click, Sub(s, ev) Managementclinic.frmUsers.ShowDialog()
        mnuUsers.DropDownItems.AddRange(New ToolStripItem() {mnuReportUsers, mnuRoles, mnuPermissions, mnuUsersList})

        ' Branches
        Dim mnuBranches As New ToolStripMenuItem("Branches")
        Dim mnuReportBranches As New ToolStripMenuItem("Report Branches")
        AddHandler mnuReportBranches.Click, Sub(s, ev) Managementclinic.ReportBranches.ShowDialog()
        Dim mnuBranchesList As New ToolStripMenuItem("Branches")
        AddHandler mnuBranchesList.Click, Sub(s, ev) Managementclinic.frmBranches.ShowDialog()
        mnuBranches.DropDownItems.AddRange(New ToolStripItem() {mnuReportBranches, mnuBranchesList})

        ' Patients
        Dim mnuPatients As New ToolStripMenuItem("Patients")
        Dim mnuReportPatients As New ToolStripMenuItem("Report Patients")
        AddHandler mnuReportPatients.Click, Sub(s, ev) Managementclinic.ReportPatients.ShowDialog()
        Dim mnuPatientsList As New ToolStripMenuItem("Patients")
        AddHandler mnuPatientsList.Click, Sub(s, ev) Managementclinic.frmpatients.ShowDialog()
        mnuPatients.DropDownItems.AddRange(New ToolStripItem() {mnuReportPatients, mnuPatientsList})

        ' Appointments
        Dim mnuAppointments As New ToolStripMenuItem("Appointments")
        Dim mnuReportAppts As New ToolStripMenuItem("Report Appointments")
        AddHandler mnuReportAppts.Click, Sub(s, ev) Managementclinic.reportappointments.ShowDialog()
        Dim mnuApptsList As New ToolStripMenuItem("Appointments")
        AddHandler mnuApptsList.Click, Sub(s, ev) Managementclinic.frmappointments.ShowDialog()
        Dim mnuApptsDay As New ToolStripMenuItem("Appointments Day")
        AddHandler mnuApptsDay.Click, Sub(s, ev)
                                          Dim f As New frmClinicCalendar() : f.Show()
                                      End Sub
        mnuAppointments.DropDownItems.AddRange(New ToolStripItem() {mnuReportAppts, mnuApptsList, mnuApptsDay})

        ' Diagnoses
        Dim mnuDiagnoses As New ToolStripMenuItem("Diagnoses")
        Dim mnuReportDiagnoses As New ToolStripMenuItem("Report Diagnoses")
        AddHandler mnuReportDiagnoses.Click, Sub(s, ev) Managementclinic.ReportDiagnoses.ShowDialog()
        Dim mnuDiagnosesList As New ToolStripMenuItem("Diagnoses")
        AddHandler mnuDiagnosesList.Click, Sub(s, ev) Managementclinic.frmDiagnoses.ShowDialog()
        mnuDiagnoses.DropDownItems.AddRange(New ToolStripItem() {mnuReportDiagnoses, mnuDiagnosesList})

        ' Invoices (Billing)
        Dim mnuBilling As New ToolStripMenuItem("الفواتير")
        Dim mnuInvoicesList As New ToolStripMenuItem("الفواتير")
        AddHandler mnuInvoicesList.Click, Sub(s, ev)
                                              Using f As New frmInvoices() : f.ShowDialog() : End Using
                                          End Sub
        Dim mnuPayments As New ToolStripMenuItem("المدفوعات")
        AddHandler mnuPayments.Click, Sub(s, ev)
                                          Using f As New frmPayments() : f.ShowDialog() : End Using
                                      End Sub
        Dim mnuExpenses As New ToolStripMenuItem("المصروفات")
        AddHandler mnuExpenses.Click, Sub(s, ev)
                                          Using f As New frmExpenses() : f.ShowDialog() : End Using
                                      End Sub
        Dim mnuFinanceReports As New ToolStripMenuItem("التقارير المالية")
        AddHandler mnuFinanceReports.Click, Sub(s, ev)
                                                Using f As New frmFinanceReports() : f.ShowDialog() : End Using
                                            End Sub
        mnuBilling.DropDownItems.AddRange(New ToolStripItem() {mnuInvoicesList, mnuPayments, mnuExpenses, mnuFinanceReports})

        ' Settings
        Dim mnuSettings As New ToolStripMenuItem("Settings")
        AddHandler mnuSettings.Click, Sub(s, ev) Managementclinic.frmSettings.ShowDialog()

        ' About
        Dim mnuAbout As New ToolStripMenuItem("About")
        AddHandler mnuAbout.Click, Sub(s, ev) Managementclinic.about.ShowDialog()

        ' Activation
        Dim mnuActivation As New ToolStripMenuItem("Activation")
        AddHandler mnuActivation.Click, Sub(s, ev)
                                            Using f As New Activation() : f.ShowDialog() : End Using
                                        End Sub

        ' Performance
        Dim mnuPerformance As New ToolStripMenuItem("Performance")
        AddHandler mnuPerformance.Click, Sub(s, ev)
                                             Dim f As New frmPerformance() : f.Show()
                                         End Sub

        ' Notifications
        Dim mnuNotifications As New ToolStripMenuItem("الإشعارات")
        AddHandler mnuNotifications.Click, Sub(s, ev)
                                               Using f As New frmNotifications() : f.ShowDialog() : End Using
                                           End Sub

        ms.Items.AddRange(New ToolStripItem() {
            mnuDoctors, mnuUsers, mnuBranches, mnuPatients,
            mnuAppointments, mnuDiagnoses, mnuBilling,
            mnuSettings, mnuAbout, mnuActivation, mnuPerformance,
            mnuNotifications
        })

        Me.Controls.Add(ms)
        ms.BringToFront()
    End Sub
End Class