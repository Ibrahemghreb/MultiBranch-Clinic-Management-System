Imports System.Drawing.Drawing2D

Public Class frmClinicCalendar
    Inherits Form

    Private ReadOnly appointmentBal As New AppointmentBAL()
    Private ReadOnly navy As Color = Color.FromArgb(38, 61, 112)
    Private ReadOnly teal As Color = Color.FromArgb(44, 166, 157)
    Private ReadOnly blue As Color = Color.FromArgb(73, 132, 187)
    Private ReadOnly softBack As Color = Color.FromArgb(240, 244, 248)
    Private ReadOnly borderColor As Color = Color.FromArgb(214, 222, 232)

    Private currentMonth As DateTime = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
    Private selectedDate As DateTime = DateTime.Today.Date

    Private monthPicker As DateTimePicker
    Private branchFilter As ComboBox
    Private doctorFilter As ComboBox
    Private titleLabel As Label
    Private calendarGrid As TableLayoutPanel
    Private appointmentsPanel As FlowLayoutPanel
    Private appointmentsTitle As Label
    Private monthRangeStart As DateTime
    Private monthRangeEnd As DateTime

    Public Sub New()
        InitializeCalendarForm()
        LoadFilters()
        RefreshCalendar()
    End Sub

    Private Sub InitializeCalendarForm()
        Text = "Appointments Calendar"
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        MinimumSize = New Size(1100, 700)
        BackColor = softBack
        Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)

        Dim root As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .RowCount = 1,
            .BackColor = softBack
        }
        root.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 62))
        root.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
        Controls.Add(root)

        root.Controls.Add(BuildSidebar(), 0, 0)
        root.Controls.Add(BuildMainArea(), 1, 0)
    End Sub

    Private Function BuildSidebar() As Control
        Dim sidebar As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .BackColor = navy,
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False,
            .Padding = New Padding(0, 18, 0, 0)
        }

        sidebar.Controls.Add(MakeSideButton("O", True))
        sidebar.Controls.Add(MakeSideButton("H", False))
        sidebar.Controls.Add(MakeSideButton("D", False))
        sidebar.Controls.Add(MakeSideButton("C", True))
        sidebar.Controls.Add(MakeSideButton("R", False))
        sidebar.Controls.Add(MakeSideButton("S", False))
        Return sidebar
    End Function

    Private Function MakeSideButton(text As String, active As Boolean) As Label
        Return New Label With {
            .Text = text,
            .BackColor = If(active, Color.White, navy),
            .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Margin = New Padding(0, 0, 0, 12),
            .Size = New Size(62, 48),
            .ForeColor = If(active, navy, Color.White)
        }
    End Function

    Private Function BuildMainArea() As Control
        Dim main As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .RowCount = 3,
            .ColumnCount = 1,
            .Padding = New Padding(18, 14, 18, 18),
            .BackColor = softBack
        }
        main.RowStyles.Add(New RowStyle(SizeType.Absolute, 58))
        main.RowStyles.Add(New RowStyle(SizeType.Absolute, 72))
        main.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        main.Controls.Add(BuildHeader(), 0, 0)
        main.Controls.Add(BuildToolbar(), 0, 1)
        main.Controls.Add(BuildContent(), 0, 2)
        Return main
    End Function

    Private Function BuildHeader() As Control
        Dim header As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 3,
            .BackColor = softBack
        }
        header.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45))
        header.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 35))
        header.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20))

        Dim appTitle As New Label With {
            .Text = "Dental Clinic Management",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
            .ForeColor = Color.Black
        }

        Dim filters As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False,
            .Padding = New Padding(0, 4, 0, 0)
        }

        branchFilter = MakeFilterCombo()
        doctorFilter = MakeFilterCombo()
        AddHandler branchFilter.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler doctorFilter.SelectedIndexChanged, AddressOf FilterChanged
        filters.Controls.Add(branchFilter)
        filters.Controls.Add(doctorFilter)

        Dim profile As New Label With {
            .Text = "●   Alerts   User",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleRight,
            .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
            .ForeColor = Color.Black
        }

        header.Controls.Add(appTitle, 0, 0)
        header.Controls.Add(filters, 1, 0)
        header.Controls.Add(profile, 2, 0)
        Return header
    End Function

    Private Function BuildToolbar() As Control
        Dim toolbar As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .BackColor = softBack
        }
        toolbar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        toolbar.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))

        monthPicker = New DateTimePicker With {
            .Format = DateTimePickerFormat.Custom,
            .CustomFormat = "MMMM yyyy",
            .Value = currentMonth,
            .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
            .Width = 210,
            .Height = 42,
            .Margin = New Padding(0, 12, 8, 0)
        }
        AddHandler monthPicker.ValueChanged, AddressOf MonthPickerChanged

        Dim leftTools As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False
        }
        leftTools.Controls.Add(monthPicker)

        titleLabel = New Label With {
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 22.0F, FontStyle.Bold),
            .ForeColor = Color.Black
        }

        Dim rightTools As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .WrapContents = False,
            .Padding = New Padding(0, 12, 0, 0)
        }

        Dim todayButton = MakeCommandButton("Today", navy, Color.White)
        Dim nextButton = MakeCommandButton("Next Month", Color.White, Color.Black)
        Dim prevButton = MakeCommandButton("Previous Month", Color.White, Color.Black)
        AddHandler todayButton.Click, AddressOf TodayClicked
        AddHandler nextButton.Click, AddressOf NextMonthClicked
        AddHandler prevButton.Click, AddressOf PreviousMonthClicked
        rightTools.Controls.Add(todayButton)
        rightTools.Controls.Add(nextButton)
        rightTools.Controls.Add(prevButton)

        toolbar.Controls.Add(titleLabel, 0, 0)
        toolbar.Controls.Add(rightTools, 1, 0)
        Return toolbar
    End Function

    Private Function BuildContent() As Control
        Dim content As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .BackColor = softBack
        }
        content.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 78))
        content.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 22))

        calendarGrid = New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 7,
            .RowCount = 7,
            .BackColor = Color.White,
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        }

        For i As Integer = 0 To 6
            calendarGrid.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F / 7.0F))
        Next

        calendarGrid.RowStyles.Add(New RowStyle(SizeType.Absolute, 46))
        For i As Integer = 1 To 6
            calendarGrid.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F / 6.0F))
        Next

        Dim rightPanel As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.White,
            .Padding = New Padding(16, 12, 16, 12)
        }

        appointmentsTitle = New Label With {
            .Dock = DockStyle.Top,
            .Height = 46,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
            .ForeColor = Color.Black
        }

        appointmentsPanel = New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False,
            .AutoScroll = True,
            .BackColor = Color.White
        }

        rightPanel.Controls.Add(appointmentsPanel)
        rightPanel.Controls.Add(appointmentsTitle)

        content.Controls.Add(calendarGrid, 0, 0)
        content.Controls.Add(rightPanel, 1, 0)
        Return content
    End Function

    Private Function MakeFilterCombo() As ComboBox
        Return New ComboBox With {
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
            .Width = 190,
            .Height = 38,
            .Margin = New Padding(0, 0, 14, 0)
        }
    End Function

    Private Function MakeCommandButton(text As String, backColor As Color, foreColor As Color) As Button
        Return New Button With {
            .Text = text,
            .BackColor = backColor,
            .ForeColor = foreColor,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
            .Height = 40,
            .Width = If(text.Length > 6, 145, 88),
            .Margin = New Padding(8, 0, 0, 0)
        }
    End Function

    Private Sub LoadFilters()
        branchFilter.Items.Clear()
        branchFilter.Items.Add(New PatientListItem(0, "All Branches"))
        For Each item In appointmentBal.GetBranches()
            branchFilter.Items.Add(item)
        Next
        branchFilter.SelectedIndex = 0

        doctorFilter.Items.Clear()
        doctorFilter.Items.Add(New PatientListItem(0, "All Doctors"))
        For Each item In appointmentBal.GetDoctors()
            doctorFilter.Items.Add(item)
        Next
        doctorFilter.SelectedIndex = 0
    End Sub

    Private Sub RefreshCalendar()
        titleLabel.Text = currentMonth.ToString("MMMM yyyy")
        If monthPicker.Value.Month <> currentMonth.Month OrElse monthPicker.Value.Year <> currentMonth.Year Then
            monthPicker.Value = currentMonth
        End If

        calendarGrid.SuspendLayout()
        calendarGrid.Controls.Clear()

        Dim dayNames = New String() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}
        For col As Integer = 0 To 6
            calendarGrid.Controls.Add(MakeDayHeader(dayNames(col)), col, 0)
        Next

        Dim firstDay As New DateTime(currentMonth.Year, currentMonth.Month, 1)
        Dim startOffset As Integer = CInt(firstDay.DayOfWeek)
        monthRangeStart = firstDay.AddDays(-startOffset)
        monthRangeEnd = monthRangeStart.AddDays(42)

        Dim counts = appointmentBal.GetAppointmentCountsByDate(monthRangeStart, monthRangeEnd, GetSelectedBranchId(), GetSelectedDoctorId())

        For index As Integer = 0 To 41
            Dim day As DateTime = monthRangeStart.AddDays(index)
            Dim row As Integer = (index \ 7) + 1
            Dim col As Integer = index Mod 7
            Dim count As Integer = If(counts.ContainsKey(day.Date), counts(day.Date), 0)
            calendarGrid.Controls.Add(MakeDayCell(day, count), col, row)
        Next

        calendarGrid.ResumeLayout()
        RefreshAppointmentsList()
    End Sub

    Private Function MakeDayHeader(text As String) As Control
        Return New Label With {
            .Text = text,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = navy,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        }
    End Function

    Private Function MakeDayCell(day As DateTime, appointmentCount As Integer) As Control
        Dim panel As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = If(day.Month = currentMonth.Month, Color.White, Color.FromArgb(248, 250, 252)),
            .Padding = New Padding(8),
            .Tag = day.Date,
            .Cursor = Cursors.Hand
        }

        If day.Date = selectedDate.Date Then
            panel.BackColor = Color.FromArgb(232, 240, 255)
        End If

        Dim dayLabel As New Label With {
            .Text = day.Day.ToString("00"),
            .Dock = DockStyle.Top,
            .Height = 28,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
            .ForeColor = If(day.Month = currentMonth.Month, Color.Black, Color.Gray),
            .Tag = day.Date,
            .Cursor = Cursors.Hand
        }

        AddHandler panel.Click, AddressOf DayCellClicked
        AddHandler dayLabel.Click, AddressOf DayCellClicked
        panel.Controls.Add(dayLabel)

        If appointmentCount > 0 Then
            Dim badge As New Label With {
                .Text = appointmentCount.ToString() & " appointments",
                .AutoSize = False,
                .Height = 24,
                .Dock = DockStyle.Top,
                .TextAlign = ContentAlignment.MiddleCenter,
                .BackColor = If(appointmentCount >= 8, teal, blue),
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold),
                .Tag = day.Date,
                .Cursor = Cursors.Hand
            }
            AddHandler badge.Click, AddressOf DayCellClicked
            panel.Controls.Add(badge)
            badge.BringToFront()
        End If

        Return panel
    End Function

    Private Sub RefreshAppointmentsList()
        appointmentsPanel.Controls.Clear()
        appointmentsTitle.Text = selectedDate.ToString("dddd, MMM dd")

        Dim items = appointmentBal.GetAppointmentsByDate(selectedDate, GetSelectedBranchId(), GetSelectedDoctorId())

        If items.Count = 0 Then
            appointmentsPanel.Controls.Add(New Label With {
                .Text = "No appointments",
                .AutoSize = False,
                .Width = Math.Max(220, appointmentsPanel.Width - 28),
                .Height = 34,
                .TextAlign = ContentAlignment.MiddleLeft,
                .ForeColor = Color.Gray
            })
            Return
        End If

        For Each item In items
            appointmentsPanel.Controls.Add(MakeAppointmentRow(item))
        Next
    End Sub

    Private Function MakeAppointmentRow(item As CalendarAppointmentItem) As Control
        Dim row As New Label With {
            .AutoSize = False,
            .Width = Math.Max(240, appointmentsPanel.Width - 30),
            .Height = 36,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 10.0F, FontStyle.Regular),
            .ForeColor = Color.Black,
            .Text = item.AppointmentTime.ToString("HH:mm") & " - " & item.PatientName,
            .Margin = New Padding(0, 0, 0, 4)
        }
        Return row
    End Function

    Private Function GetSelectedBranchId() As Integer
        If branchFilter Is Nothing OrElse branchFilter.SelectedItem Is Nothing Then
            Return 0
        End If

        Return DirectCast(branchFilter.SelectedItem, PatientListItem).ID
    End Function

    Private Function GetSelectedDoctorId() As Integer
        If doctorFilter Is Nothing OrElse doctorFilter.SelectedItem Is Nothing Then
            Return 0
        End If

        Return DirectCast(doctorFilter.SelectedItem, PatientListItem).ID
    End Function

    Private Sub DayCellClicked(sender As Object, e As EventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)
        selectedDate = DirectCast(ctrl.Tag, DateTime)
        RefreshCalendar()
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs)
        If calendarGrid IsNot Nothing Then
            RefreshCalendar()
        End If
    End Sub

    Private Sub MonthPickerChanged(sender As Object, e As EventArgs)
        Dim picked = monthPicker.Value
        currentMonth = New DateTime(picked.Year, picked.Month, 1)
        If selectedDate.Month <> currentMonth.Month OrElse selectedDate.Year <> currentMonth.Year Then
            selectedDate = currentMonth
        End If
        RefreshCalendar()
    End Sub

    Private Sub PreviousMonthClicked(sender As Object, e As EventArgs)
        currentMonth = currentMonth.AddMonths(-1)
        selectedDate = currentMonth
        RefreshCalendar()
    End Sub

    Private Sub NextMonthClicked(sender As Object, e As EventArgs)
        currentMonth = currentMonth.AddMonths(1)
        selectedDate = currentMonth
        RefreshCalendar()
    End Sub

    Private Sub TodayClicked(sender As Object, e As EventArgs)
        selectedDate = DateTime.Today
        currentMonth = New DateTime(selectedDate.Year, selectedDate.Month, 1)
        RefreshCalendar()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmClinicCalendar
        '
        Me.ClientSize = New System.Drawing.Size(1175, 702)
        Me.Name = "frmClinicCalendar"
        Me.ResumeLayout(False)

    End Sub
End Class
