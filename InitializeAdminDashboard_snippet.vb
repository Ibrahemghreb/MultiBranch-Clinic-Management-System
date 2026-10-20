' ================================================================
' استبدل الدالة InitializeAdminDashboard في FrmMain.vb بهذه
' ================================================================
Private Sub InitializeAdminDashboard()
    adminDashboard = New AdminDashboardPanel()
    adminDashboard.Dock = DockStyle.Fill

    ' ── ربط كل أحداث التنقل ──────────────────────────────────────
    AddHandler adminDashboard.OpenPatientsRequested,
        Sub(s, ev) Managementclinic.frmpatients.ShowDialog()

    AddHandler adminDashboard.OpenAppointmentsRequested,
        Sub(s, ev) Managementclinic.frmAppointments.ShowDialog()

    AddHandler adminDashboard.OpenDoctorsRequested,
        Sub(s, ev) Managementclinic.frmdoctors.ShowDialog()

    AddHandler adminDashboard.OpenBranchesRequested,
        Sub(s, ev) Managementclinic.frmbranches.ShowDialog()

    AddHandler adminDashboard.OpenTreatmentsRequested,
        Sub(s, ev) Managementclinic.frmDiagnoses.ShowDialog()

    AddHandler adminDashboard.OpenInvoicesRequested,
        Sub(s, ev)
            Using f As New frmInvoices()
                f.ShowDialog()
            End Using
        End Sub

    AddHandler adminDashboard.OpenReportsRequested,
        Sub(s, ev) Managementclinic.frmReports.ShowDialog()

    AddHandler adminDashboard.OpenSettingsRequested,
        Sub(s, ev) Managementclinic.frmsettings.ShowDialog()

    AddHandler adminDashboard.OpenNotificationsRequested,
        Sub(s, ev) OpenNotificationsForm()

    ' ── إضافة للـ Form ───────────────────────────────────────────
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
End Sub

' ── تحديث الـ Dashboard عند العودة إليه ─────────────────────────
Private Sub DashboardToolStripMenuItem_Click(sender As Object, e As EventArgs)
    adminDashboard.Visible = True
    adminDashboard.BringToFront()
    adminDashboard.RefreshAll()   ' ← كان RefreshDashboard
End Sub

' ── تحديث بعد إضافة بيانات جديدة ────────────────────────────────
' استبدل في أي مكان في FrmMain يستدعي adminDashboard.RefreshDashboard(...)
' بـ adminDashboard.RefreshAll()
