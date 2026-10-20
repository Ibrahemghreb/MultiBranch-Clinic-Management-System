Imports System.Drawing
Imports System.Windows.Forms
Imports ReaLTaiizor.Forms

''' <summary>
''' نموذج تقويم الأداء الشهري للعيادة
''' يعرض: التقويم الشهري + فلاتر الفرع والطبيب + قائمة مواعيد اليوم + ملخص الشهر
''' </summary>
Public Class frmPerformance
    Inherits MaterialForm

    ' ── BAL ──────────────────────────────────────────────────────────────────
    Private ReadOnly _bal As New PerformanceBAL()

    ' ── حالة العرض ────────────────────────────────────────────────────────────
    Private _displayDate As Date = New Date(Now.Year, Now.Month, 1)
    Private _selectedDate As Date = Date.Today
    Private _branchId As Integer = 0
    Private _doctorId As Integer = 0
    Private _isLoading As Boolean = False
    ' ── عناصر التحكم المُنشأة برمجياً ───────────────────────────────────────

    ' شريط العنوان العلوي
    Private pnlTop As Panel
    Private lblTitle As Label
    Private lblMonthYear As Label
    Private btnToday As Button
    Private btnPrevMonth As Button
    Private btnNextMonth As Button
    Private btnAddAppoint As Button

    ' فلاتر
    Private pnlFilters As Panel
    Private cmbBranch As ComboBox
    Private cmbDoctor As ComboBox
    Private lblBranchFilter As Label
    Private lblDoctorFilter As Label

    ' التقويم (Custom Control)
    Private _calendar As PerformanceCalendarDesigner

    ' اللوحة الجانبية - مواعيد اليوم
    Private pnlSidebar As Panel
    Private lblTodayTitle As Label
    Private lstAppointments As ListBox

    ' شريط الملخص السفلي
    Private pnlSummary As Panel
    Private lblTotalApp As Label
    Private lblTotalPat As Label
    Private lblTotalRev As Label

    ' ── تهيئة النموذج ────────────────────────────────────────────────────────

    Public Sub New()
        InitializeComponent()
        BuildUI()
    End Sub

    Private Sub frmPerformance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        LoadFilters()
        RefreshCalendar()
        RefreshSidebarForDate(_selectedDate)
    End Sub

    ' ── بناء الواجهة برمجياً ─────────────────────────────────────────────────

    Private Sub BuildUI()
        Me.Text = "Monthly Clinic Performance"
        Me.Size = New Size(1100, 720)
        Me.MinimumSize = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 248, 252)
        Me.Font = New Font("Segoe UI", 9.5F)

        BuildTopBar()
        BuildFilterBar()
        BuildSummaryBar()
        BuildSidebar()
        BuildCalendarPanel()
    End Sub

    Private Sub BuildTopBar()
        pnlTop = New Panel()
        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 56
        pnlTop.BackColor = Color.FromArgb(44, 62, 80)
        pnlTop.Padding = New Padding(12, 8, 12, 8)

        lblTitle = New Label()
        lblTitle.Text = "Dental Clinic Management"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(12, 16)

        lblMonthYear = New Label()
        lblMonthYear.ForeColor = Color.White
        lblMonthYear.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        lblMonthYear.AutoSize = True
        lblMonthYear.Location = New Point(260, 18)

        ' أزرار التنقل
        btnPrevMonth = CreateNavButton("◀ Previous Month", 500)
        btnNextMonth = CreateNavButton("Next Month ▶", 660)
        btnToday = CreateNavButton("Today", 820)
        btnToday.BackColor = Color.FromArgb(0, 150, 136)

        btnAddAppoint = New Button()
        btnAddAppoint.Text = "+ New Appointment"
        btnAddAppoint.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnAddAppoint.ForeColor = Color.White
        btnAddAppoint.BackColor = Color.FromArgb(0, 150, 136)
        btnAddAppoint.FlatStyle = FlatStyle.Flat
        btnAddAppoint.FlatAppearance.BorderSize = 0
        btnAddAppoint.Size = New Size(150, 32)
        btnAddAppoint.Location = New Point(Me.Width - 180, 12)
        btnAddAppoint.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        pnlTop.Controls.AddRange({lblTitle, lblMonthYear,
                                   btnPrevMonth, btnNextMonth, btnToday, btnAddAppoint})
        Me.Controls.Add(pnlTop)

        AddHandler btnPrevMonth.Click, AddressOf btnPrevMonth_Click
        AddHandler btnNextMonth.Click, AddressOf btnNextMonth_Click
        AddHandler btnToday.Click, AddressOf btnToday_Click
        AddHandler btnAddAppoint.Click, AddressOf btnAddAppoint_Click
    End Sub

    Private Function CreateNavButton(text As String, x As Integer) As Button
        Dim btn = New Button()
        btn.Text = text
        btn.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        btn.ForeColor = Color.White
        btn.BackColor = Color.FromArgb(70, 90, 110)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Size = New Size(140, 32)
        btn.Location = New Point(x, 12)
        btn.Cursor = Cursors.Hand
        Return btn
    End Function

    Private Sub BuildFilterBar()
        pnlFilters = New Panel()
        pnlFilters.Dock = DockStyle.Top
        pnlFilters.Height = 48
        pnlFilters.BackColor = Color.FromArgb(236, 240, 245)
        pnlFilters.Padding = New Padding(12, 8, 12, 8)

        lblBranchFilter = New Label()
        lblBranchFilter.Text = "Branch Filter:"
        lblBranchFilter.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblBranchFilter.AutoSize = True
        lblBranchFilter.Location = New Point(12, 15)

        cmbBranch = New ComboBox()
        cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBranch.Font = New Font("Segoe UI", 9.5F)
        cmbBranch.Size = New Size(200, 28)
        cmbBranch.Location = New Point(100, 10)

        lblDoctorFilter = New Label()
        lblDoctorFilter.Text = "Doctor Filter:"
        lblDoctorFilter.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblDoctorFilter.AutoSize = True
        lblDoctorFilter.Location = New Point(330, 15)

        cmbDoctor = New ComboBox()
        cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDoctor.Font = New Font("Segoe UI", 9.5F)
        cmbDoctor.Size = New Size(200, 28)
        cmbDoctor.Location = New Point(420, 10)

        pnlFilters.Controls.AddRange({lblBranchFilter, cmbBranch,
                                       lblDoctorFilter, cmbDoctor})
        Me.Controls.Add(pnlFilters)

        AddHandler cmbBranch.SelectedIndexChanged, AddressOf cmbBranch_Changed
        AddHandler cmbDoctor.SelectedIndexChanged, AddressOf cmbDoctor_Changed
    End Sub

    Private Sub BuildSummaryBar()
        pnlSummary = New Panel()
        pnlSummary.Dock = DockStyle.Bottom
        pnlSummary.Height = 44
        pnlSummary.BackColor = Color.FromArgb(44, 62, 80)
        pnlSummary.Padding = New Padding(16, 8, 16, 8)

        lblTotalApp = CreateSummaryLabel("Appointments: —", 20)
        lblTotalPat = CreateSummaryLabel("Patients: —", 250)
        lblTotalRev = CreateSummaryLabel("Revenue: —", 480)

        pnlSummary.Controls.AddRange({lblTotalApp, lblTotalPat, lblTotalRev})
        Me.Controls.Add(pnlSummary)
    End Sub

    Private Function CreateSummaryLabel(text As String, x As Integer) As Label
        Dim lbl = New Label()
        lbl.Text = text
        lbl.ForeColor = Color.White
        lbl.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lbl.AutoSize = True
        lbl.Location = New Point(x, 12)
        Return lbl
    End Function

    Private Sub BuildSidebar()
        pnlSidebar = New Panel()
        pnlSidebar.Dock = DockStyle.Right
        pnlSidebar.Width = 220
        pnlSidebar.BackColor = Color.White
        pnlSidebar.Padding = New Padding(8)

        ' خط فاصل يسار
        AddHandler pnlSidebar.Paint, AddressOf pnlSidebar_Paint

        lblTodayTitle = New Label()
        lblTodayTitle.Text = "Today's Appointments"
        lblTodayTitle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblTodayTitle.ForeColor = Color.FromArgb(44, 62, 80)
        lblTodayTitle.Dock = DockStyle.Top
        lblTodayTitle.Height = 36
        lblTodayTitle.TextAlign = ContentAlignment.MiddleLeft
        lblTodayTitle.Padding = New Padding(4, 0, 0, 0)

        lstAppointments = New ListBox()
        lstAppointments.Dock = DockStyle.Fill
        lstAppointments.Font = New Font("Segoe UI", 9.5F)
        lstAppointments.BorderStyle = BorderStyle.None
        lstAppointments.ItemHeight = 28
        lstAppointments.BackColor = Color.White
        lstAppointments.ForeColor = Color.FromArgb(44, 62, 80)
        lstAppointments.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler lstAppointments.DrawItem, AddressOf DrawAppointmentItem

        pnlSidebar.Controls.Add(lstAppointments)
        pnlSidebar.Controls.Add(lblTodayTitle)
        Me.Controls.Add(pnlSidebar)
    End Sub

    Private Sub DrawAppointmentItem(sender As Object, e As DrawItemEventArgs)
        If e.Index < 0 Then Return
        e.DrawBackground()
        Dim item = lstAppointments.Items(e.Index).ToString()

        ' خط فاصل
        e.Graphics.DrawLine(New Pen(Color.FromArgb(240, 245, 250)),
                            e.Bounds.Left, e.Bounds.Bottom - 1,
                            e.Bounds.Right, e.Bounds.Bottom - 1)

        Dim dotColor = Color.FromArgb(0, 150, 136)
        Using dotBrush As New SolidBrush(dotColor)
            e.Graphics.FillEllipse(dotBrush, e.Bounds.Left + 6,
                                   e.Bounds.Top + 9, 8, 8)
        End Using

        Using fnt As New Font("Segoe UI", 9.0F)
            Using fBrush As New SolidBrush(Color.FromArgb(44, 62, 80))
                e.Graphics.DrawString(item, fnt, fBrush,
                                      New RectangleF(e.Bounds.Left + 20, e.Bounds.Top + 2,
                                                     e.Bounds.Width - 22, e.Bounds.Height - 2))
            End Using
        End Using
        e.DrawFocusRectangle()
    End Sub

    Private Sub pnlSidebar_Paint(sender As Object, e As PaintEventArgs)
        ' رسم خط فاصل على الجانب الأيسر من الـ Sidebar
        e.Graphics.DrawLine(New Pen(Color.FromArgb(220, 230, 240), 1),
                            0, 0, 0, pnlSidebar.Height)
    End Sub

    Private Sub BuildCalendarPanel()
        _calendar = New PerformanceCalendarDesigner()
        _calendar.Dock = DockStyle.Fill
        _calendar.DisplayYear = _displayDate.Year
        _calendar.DisplayMonth = _displayDate.Month
        Me.Controls.Add(_calendar)

        AddHandler _calendar.DayClicked, AddressOf Calendar_DayClicked
    End Sub

    ' ── تحميل البيانات ───────────────────────────────────────────────────────

    Private Sub LoadFilters()
        _isLoading = True
        Try
            ' فروع
            Dim branchDt = _bal.GetBranchesForFilter()
            cmbBranch.DataSource = branchDt
            cmbBranch.DisplayMember = "BranchName"
            cmbBranch.ValueMember = "BranchID"
            cmbBranch.SelectedIndex = 0

            ' أطباء
            RefreshDoctorFilter()
        Finally
            _isLoading = False
        End Try
    End Sub

    Private Sub RefreshDoctorFilter()
        If _isLoading Then Return
        _isLoading = True
        Try
            Dim doctorDt = _bal.GetDoctorsForFilter(_branchId)
            cmbDoctor.DataSource = doctorDt
            cmbDoctor.DisplayMember = "DoctorName"
            cmbDoctor.ValueMember = "DoctorID"
            cmbDoctor.SelectedIndex = 0
            _doctorId = 0
        Finally
            _isLoading = False
        End Try
    End Sub

    Private Sub RefreshCalendar()
        Try
            lblMonthYear.Text = _displayDate.ToString("MMMM yyyy")
            _calendar.DisplayYear = _displayDate.Year
            _calendar.DisplayMonth = _displayDate.Month

            Dim map = _bal.GetMonthlyCountMap(_displayDate.Year, _displayDate.Month,
                                              _branchId, _doctorId)
            _calendar.LoadData(map)
            RefreshSummary()
        Catch ex As Exception
            ShowError("Error loading calendar data", ex)
        End Try
    End Sub

    Private Sub RefreshSidebarForDate(targetDate As Date)
        Try
            lblTodayTitle.Text = $"Appointments — {targetDate:ddd, MMM d}"
            lstAppointments.Items.Clear()
            Dim items = _bal.GetDayAppointmentList(targetDate, _branchId, _doctorId)
            If items.Count = 0 Then
                lstAppointments.Items.Add("No appointments this day")
            Else
                For Each item In items
                    lstAppointments.Items.Add(item.DisplayText)
                Next
            End If
        Catch ex As Exception
            ShowError("Error loading day appointments", ex)
        End Try
    End Sub

    Private Sub RefreshSummary()
        Try
            Dim summary = _bal.GetMonthSummary(_displayDate.Year, _displayDate.Month,
                                               _branchId, _doctorId)
            lblTotalApp.Text = $"Appointments: {summary.TotalAppointments:N0}"
            lblTotalPat.Text = $"Patients: {summary.TotalPatients:N0}"
            lblTotalRev.Text = $"Revenue: {summary.TotalRevenue:N2} EGP"
        Catch ex As Exception
            lblTotalApp.Text = "Appointments: —"
            lblTotalPat.Text = "Patients: —"
            lblTotalRev.Text = "Revenue: —"
        End Try
    End Sub

    ' ── معالجات الأحداث ───────────────────────────────────────────────────────

    Private Sub Calendar_DayClicked(sender As Object, day As Integer)
        _selectedDate = New Date(_displayDate.Year, _displayDate.Month, day)
        RefreshSidebarForDate(_selectedDate)
    End Sub

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs)
        _displayDate = _displayDate.AddMonths(-1)
        _selectedDate = _displayDate  ' اختر اليوم الأول من الشهر الجديد
        _calendar.SelectedDay = 1
        RefreshCalendar()
        RefreshSidebarForDate(_displayDate)
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs)
        _displayDate = _displayDate.AddMonths(1)
        _selectedDate = _displayDate
        _calendar.SelectedDay = 1
        RefreshCalendar()
        RefreshSidebarForDate(_displayDate)
    End Sub

    Private Sub btnToday_Click(sender As Object, e As EventArgs)
        _displayDate = New Date(Date.Today.Year, Date.Today.Month, 1)
        _selectedDate = Date.Today
        _calendar.SelectedDay = Date.Today.Day
        RefreshCalendar()
        RefreshSidebarForDate(_selectedDate)
    End Sub

    Private Sub btnAddAppoint_Click(sender As Object, e As EventArgs)
        ' فتح نموذج إضافة موعد إذا كان موجوداً
        Try
            Dim frm As New frmappointments()
            frm.Show()
        Catch ex As Exception
            MessageBox.Show("لم يتم العثور على نموذج المواعيد.", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmbBranch_Changed(sender As Object, e As EventArgs)
        If _isLoading Then Return
        Dim row = TryCast(cmbBranch.SelectedItem, DataRowView)
        If row Is Nothing Then Return
        _branchId = CInt(row("BranchID"))
        RefreshDoctorFilter()
        RefreshCalendar()
        RefreshSidebarForDate(_selectedDate)
    End Sub

    Private Sub cmbDoctor_Changed(sender As Object, e As EventArgs)
        If _isLoading Then Return
        Dim row = TryCast(cmbDoctor.SelectedItem, DataRowView)
        If row Is Nothing Then Return
        _doctorId = CInt(row("DoctorID"))
        RefreshCalendar()
        RefreshSidebarForDate(_selectedDate)
    End Sub

    Private Sub frmPerformance_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Left : btnPrevMonth_Click(Me, EventArgs.Empty)
            Case Keys.Right : btnNextMonth_Click(Me, EventArgs.Empty)
            Case Keys.Home : btnToday_Click(Me, EventArgs.Empty)
            Case Keys.Escape : Me.Close()
        End Select
    End Sub

    ' ── دوال مساعدة ──────────────────────────────────────────────────────────

    Private Sub ShowError(message As String, ex As Exception)
        Logger.LogError($"frmPerformance — {message}: {ex.Message}")
        MessageBox.Show($"{message}:{Environment.NewLine}{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.ClientSize = New Size(1100, 720)
        Me.Name = "frmPerformance"
        Me.ResumeLayout(False)
    End Sub

End Class