Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' لوحة التحكم الرئيسية — تصميم متطور وعصري:
''' - Sidebar يمين بتدرج Slate داكن + شعار احترافي
''' - Header علوي: بحث + إشعارات + avatar المستخدم
''' - 4 بطاقات KPI محسّنة مع تأثير hover
''' - 3 أعمدة: فروع / مخطط إيرادات / مواعيد قادمة
''' - شريط إجراءات سريعة أسفل بتصميم gradient
''' </summary>
Public Class AdminDashboardPanel
    Inherits UserControl

    ' ── Data ─────────────────────────────────────────────────────────────
    Private ReadOnly _bal As New DashboardBAL()
    Private ReadOnly _perfDal As New PerformanceDAL()

    ' ── Events ───────────────────────────────────────────────────────────
    Public Event OpenBranchesRequested As EventHandler
    Public Event OpenAppointmentsRequested As EventHandler
    Public Event OpenPatientsRequested As EventHandler
    Public Event OpenDoctorsRequested As EventHandler
    Public Event OpenTreatmentsRequested As EventHandler
    Public Event OpenInvoicesRequested As EventHandler
    Public Event OpenReportsRequested As EventHandler
    Public Event OpenSettingsRequested As EventHandler
    Public Event OpenNotificationsRequested As EventHandler
    Public Event OpenWarehouseRequested As EventHandler
    Public Event OpenUsersRequested As EventHandler
    Public Event OpenRolesRequested As EventHandler
    Public Event OpenPermissionsRequested As EventHandler

    ' ── Layout regions ───────────────────────────────────────────────────
    Private pnlSidebar As Panel
    Private pnlMain As Panel
    Private pnlHeader As Panel
    Private pnlKpi As Panel
    Private pnlContent As Panel
    Private pnlQuickActions As Panel

    ' ── KPI labels ───────────────────────────────────────────────────────
    Private lblKpiPatients As Label
    Private lblKpiAppointments As Label
    Private lblKpiBranches As Label
    Private lblKpiRevenue As Label

    ' ── Header ───────────────────────────────────────────────────────────
    Private lblWelcome As Label
    Private lblSubWelcome As Label
    Private cboBranch As ComboBox
    Private pnlNotifIcon As Panel
    Private _unreadNotifCount As Integer = 0

    ' ── Content panels ───────────────────────────────────────────────────
    Private pnlBranchList As Panel
    Private pnlChart As Panel
    Private pnlAppList As Panel

    ' ── Chart data ───────────────────────────────────────────────────────
    Private _chartData As List(Of Decimal)
    Private _chartLabels As List(Of String)

    ' ── Active nav tracking ───────────────────────────────────────────────
    Private _activeNavPanel As Panel = Nothing

    ' ══════════════════════════════════════════════════════════════════════
    '  DESIGN TOKENS — Premium Slate + Teal Palette
    ' ══════════════════════════════════════════════════════════════════════
    ' Sidebar colours
    Private ReadOnly ClrSidebarTop As Color = Color.FromArgb(10, 18, 38)
    Private ReadOnly ClrSidebarBot As Color = Color.FromArgb(18, 30, 60)
    Private ReadOnly ClrSidebarHover As Color = Color.FromArgb(30, 48, 90)
    Private ReadOnly ClrSidebarActive As Color = Color.FromArgb(0, 168, 150)  ' Teal accent
    Private ReadOnly ClrSidebarActiveBg As Color = Color.FromArgb(25, 0, 168, 150)

    ' Main area
    Private ReadOnly ClrBg As Color = Color.FromArgb(240, 244, 250)
    Private ReadOnly ClrCard As Color = Color.White
    Private ReadOnly ClrCardHover As Color = Color.FromArgb(248, 250, 255)

    ' Accent colours
    Private ReadOnly ClrTeal As Color = Color.FromArgb(0, 168, 150)
    Private ReadOnly ClrBlue As Color = Color.FromArgb(59, 130, 246)
    Private ReadOnly ClrGreen As Color = Color.FromArgb(16, 185, 129)
    Private ReadOnly ClrOrange As Color = Color.FromArgb(245, 158, 11)
    Private ReadOnly ClrPurple As Color = Color.FromArgb(139, 92, 246)
    Private ReadOnly ClrRose As Color = Color.FromArgb(239, 68, 68)

    ' Text
    Private ReadOnly ClrText As Color = Color.FromArgb(15, 23, 42)
    Private ReadOnly ClrSubText As Color = Color.FromArgb(100, 116, 139)
    Private ReadOnly ClrBorder As Color = Color.FromArgb(226, 232, 240)

    ' ── Fonts ────────────────────────────────────────────────────────────
    Private ReadOnly FntLogo As New Font("Segoe UI", 16, FontStyle.Bold)
    Private ReadOnly FntLogoSub As New Font("Segoe UI", 8.5F)
    Private ReadOnly FntNav As New Font("Segoe UI", 10, FontStyle.Regular)
    Private ReadOnly FntNavBold As New Font("Segoe UI", 10, FontStyle.Bold)
    Private ReadOnly FntKpiValue As New Font("Segoe UI", 26, FontStyle.Bold)
    Private ReadOnly FntKpiTitle As New Font("Segoe UI", 9)
    Private ReadOnly FntKpiSub As New Font("Segoe UI", 8)
    Private ReadOnly FntSectionTitle As New Font("Segoe UI", 11, FontStyle.Bold)
    Private ReadOnly FntSmall As New Font("Segoe UI", 9)
    Private ReadOnly FntBadge As New Font("Segoe UI", 7.5F, FontStyle.Bold)
    Private ReadOnly FntWelcome As New Font("Segoe UI", 16, FontStyle.Bold)
    Private ReadOnly FntSub As New Font("Segoe UI", 9.5F)
    Private ReadOnly FntSearch As New Font("Segoe UI", 10)

    ' ═══════════════════════════════════════════════════════════════════════
    Public Sub New()
        Me.Dock = DockStyle.Fill
        Me.BackColor = ClrBg
        Me.RightToLeft = RightToLeft.Yes
        Me.DoubleBuffered = True
        BuildLayout()
        AddHandler Me.Load, AddressOf OnDashboardLoad
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' BUILD LAYOUT
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub BuildLayout()

        ' ── Sidebar ──────────────────────────────────────────────────────
        pnlSidebar = New Panel With {
            .Dock = DockStyle.Right,
            .Width = 260,
            .BackColor = ClrSidebarTop
        }
        AddHandler pnlSidebar.Paint, AddressOf PaintSidebarGradient
        BuildSidebar()

        ' ── Main area ────────────────────────────────────────────────────
        pnlMain = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = ClrBg,
            .Padding = New Padding(20, 16, 20, 16)
        }

        ' ── Header ───────────────────────────────────────────────────────
        pnlHeader = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 80,
            .BackColor = ClrCard,
            .Padding = New Padding(24, 0, 24, 0)
        }
        AddHandler pnlHeader.Paint, AddressOf PaintHeaderShadow
        BuildHeader()

        ' ── KPI row ──────────────────────────────────────────────────────
        pnlKpi = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 130,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 16, 0, 0)
        }
        BuildKpiCards()

        ' ── Content row (3 columns) ───────────────────────────────────────
        pnlContent = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 14, 0, 14)
        }
        BuildContentColumns()

        ' ── Quick actions ─────────────────────────────────────────────────
        pnlQuickActions = New Panel With {
            .Dock = DockStyle.Bottom,
            .Height = 100,
            .BackColor = ClrCard,
            .Padding = New Padding(20, 12, 20, 12)
        }
        AddHandler pnlQuickActions.Paint, AddressOf PaintQuickActionsTop
        BuildQuickActions()

        ' ── Assemble ──────────────────────────────────────────────────────
        pnlMain.Controls.Add(pnlContent)
        pnlMain.Controls.Add(pnlKpi)
        pnlMain.Controls.Add(pnlHeader)
        pnlMain.Controls.Add(pnlQuickActions)

        Me.Controls.Add(pnlMain)
        Me.Controls.Add(pnlSidebar)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' SIDEBAR
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub PaintSidebarGradient(sender As Object, e As PaintEventArgs)
        Dim p = DirectCast(sender, Panel)
        Using br As New LinearGradientBrush(
                New Point(0, 0), New Point(0, p.Height),
                ClrSidebarTop, ClrSidebarBot)
            e.Graphics.FillRectangle(br, p.ClientRectangle)
        End Using
        ' خط فاصل يسار
        Using pen As New Pen(Color.FromArgb(40, 255, 255, 255))
            e.Graphics.DrawLine(pen, 0, 0, 0, p.Height)
        End Using
    End Sub

    Private Sub BuildSidebar()
        ' ── Logo Panel ──────────────────────────────────────────────────
        Dim pnlLogo As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 90,
            .BackColor = Color.Transparent,
            .Padding = New Padding(16, 14, 16, 0)
        }
        AddHandler pnlLogo.Paint, Sub(s, e)
                                      Dim g = e.Graphics
                                      g.SmoothingMode = SmoothingMode.AntiAlias
                                      ' خلفية دائرية للشعار
                                      Dim iconRect = New Rectangle(220, 18, 50, 50)
                                      Using gradBr As New LinearGradientBrush(iconRect, ClrTeal, Color.FromArgb(0, 120, 110), LinearGradientMode.ForwardDiagonal)
                                          g.FillEllipse(gradBr, iconRect)
                                      End Using
                                      ' رسم سن مبسط
                                      Using br As New SolidBrush(Color.White)
                                          g.DrawString("🦷", New Font("Segoe UI Emoji", 20), br, iconRect.X + 7, iconRect.Y + 9)
                                      End Using
                                      ' نص الشعار
                                      Using br As New SolidBrush(Color.White)
                                          g.DrawString("ابتسامة", FntLogo, br, New RectangleF(10, 16, 200, 32))
                                      End Using
                                      Using br As New SolidBrush(Color.FromArgb(160, 200, 220))
                                          g.DrawString("نظام إدارة عيادات الأسنان", FntLogoSub, br, New RectangleF(10, 50, 200, 22))
                                      End Using
                                      ' خط فاصل سفلي
                                      Using pen As New Pen(Color.FromArgb(40, 255, 255, 255))
                                          g.DrawLine(pen, 16, 86, DirectCast(s, Panel).Width - 16, 86)
                                      End Using
                                  End Sub
        pnlSidebar.Controls.Add(pnlLogo)

        ' ── Navigation ───────────────────────────────────────────────────
        Dim navItems As New List(Of NavItemData) From {
            New NavItemData("🏠", "الرئيسية", True, "Home"),
            New NavItemData("📍", "الفروع", False, "Branches"),
            New NavItemData("📅", "المواعيد", False, "Appointments"),
            New NavItemData("👥", "المرضى", False, "Patients"),
            New NavItemData("👤", "الأطباء", False, "Doctors"),
            New NavItemData("🔑", "المستخدمين", False, "Users"),
            New NavItemData("📋", "الأدوار", False, "Roles"),
            New NavItemData("🔐", "الصلاحيات", False, "Permissions"),
            New NavItemData("🦷", "التشخيص والعلاج", False, "Treatments"),
            New NavItemData("🧾", "الفواتير", False, "Invoices"),
            New NavItemData("📦", "المخزون", False, "Warehouse"),
            New NavItemData("📊", "التقارير", False, "Reports"),
            New NavItemData("⚙️", "الإعدادات", False, "Settings")
        }

        Dim pnlNav As New Panel With {
            .Dock = DockStyle.Top,
            .Height = navItems.Count * 50,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 6, 0, 6)
        }

        For i As Integer = 0 To navItems.Count - 1
            Dim item = navItems(i)
            Dim y = i * 50

            Dim pnlItem As New Panel With {
                .Location = New Point(0, y),
                .Size = New Size(260, 48),
                .BackColor = If(item.IsActive, ClrSidebarActiveBg, Color.Transparent),
                .Cursor = Cursors.Hand,
                .Tag = item.Ev
            }

            If item.IsActive Then
                _activeNavPanel = pnlItem
            End If

            Dim isAct = item.IsActive
            Dim evTag = item.Ev

            AddHandler pnlItem.Paint, Sub(s, e)
                                          Dim pp = DirectCast(s, Panel)
                                          Dim g = e.Graphics
                                          g.SmoothingMode = SmoothingMode.AntiAlias
                                          ' شريط أيمن للعنصر النشط
                                          If pp.BackColor <> Color.Transparent AndAlso pp.BackColor = ClrSidebarActiveBg Then
                                              Using br As New SolidBrush(ClrTeal)
                                                  g.FillRectangle(br, pp.Width - 4, 8, 4, 32)
                                              End Using
                                          End If
                                      End Sub

            Dim lblIcon As New Label With {
                .Text = item.Icon,
                .Location = New Point(220, 13),
                .Size = New Size(28, 24),
                .Font = New Font("Segoe UI Emoji", 12),
                .ForeColor = If(item.IsActive, ClrTeal, Color.FromArgb(160, 190, 220)),
                .BackColor = Color.Transparent,
                .TextAlign = ContentAlignment.MiddleCenter
            }

            Dim lblText As New Label With {
                .Text = item.Caption,
                .Location = New Point(16, 13),
                .Size = New Size(196, 24),
                .Font = If(item.IsActive, FntNavBold, FntNav),
                .ForeColor = If(item.IsActive, Color.White, Color.FromArgb(160, 190, 220)),
                .BackColor = Color.Transparent,
                .TextAlign = ContentAlignment.MiddleRight
            }

            pnlItem.Controls.Add(lblIcon)
            pnlItem.Controls.Add(lblText)

            ' Hover effects
            AddHandler pnlItem.MouseEnter, Sub(s, e)
                                               Dim pp = DirectCast(s, Panel)
                                               If pp IsNot _activeNavPanel Then
                                                   pp.BackColor = ClrSidebarHover
                                               End If
                                           End Sub
            AddHandler pnlItem.MouseLeave, Sub(s, e)
                                               Dim pp = DirectCast(s, Panel)
                                               If pp IsNot _activeNavPanel Then
                                                   pp.BackColor = Color.Transparent
                                               End If
                                           End Sub
            AddHandler lblIcon.MouseEnter, Sub(s, e)
                                               Dim pp = DirectCast(DirectCast(s, Label).Parent, Panel)
                                               If pp IsNot _activeNavPanel Then pp.BackColor = ClrSidebarHover
                                           End Sub
            AddHandler lblIcon.MouseLeave, Sub(s, e)
                                               Dim pp = DirectCast(DirectCast(s, Label).Parent, Panel)
                                               If pp IsNot _activeNavPanel Then pp.BackColor = Color.Transparent
                                           End Sub
            AddHandler lblText.MouseEnter, Sub(s, e)
                                               Dim pp = DirectCast(DirectCast(s, Label).Parent, Panel)
                                               If pp IsNot _activeNavPanel Then pp.BackColor = ClrSidebarHover
                                           End Sub
            AddHandler lblText.MouseLeave, Sub(s, e)
                                               Dim pp = DirectCast(DirectCast(s, Label).Parent, Panel)
                                               If pp IsNot _activeNavPanel Then pp.BackColor = Color.Transparent
                                           End Sub

            AddHandler pnlItem.Click, AddressOf NavItem_Click
            AddHandler lblIcon.Click, AddressOf NavItem_Click
            AddHandler lblText.Click, AddressOf NavItem_Click

            pnlNav.Controls.Add(pnlItem)
        Next

        pnlSidebar.Controls.Add(pnlNav)

        ' ── User card at bottom ───────────────────────────────────────────
        Dim pnlUser As New Panel With {
            .Dock = DockStyle.Bottom,
            .Height = 72,
            .BackColor = Color.Transparent,
            .Padding = New Padding(14, 10, 14, 10)
        }
        AddHandler pnlUser.Paint, Sub(s, e)
                                      Dim g = e.Graphics
                                      g.SmoothingMode = SmoothingMode.AntiAlias
                                      ' خط فاصل علوي
                                      Using pen As New Pen(Color.FromArgb(40, 255, 255, 255))
                                          g.DrawLine(pen, 14, 0, DirectCast(s, Panel).Width - 14, 0)
                                      End Using
                                      ' دائرة Avatar
                                      Dim avRect = New Rectangle(DirectCast(s, Panel).Width - 52, 12, 42, 42)
                                      Using gradBr As New LinearGradientBrush(avRect, ClrTeal, Color.FromArgb(0, 100, 90), LinearGradientMode.ForwardDiagonal)
                                          g.FillEllipse(gradBr, avRect)
                                      End Using
                                      Using br As New SolidBrush(Color.White)
                                          g.DrawString("👤", New Font("Segoe UI Emoji", 14), br, avRect.X + 5, avRect.Y + 5)
                                      End Using
                                      ' اسم + دور
                                      Dim name = If(SessionManager.FullName, SessionManager.UserName)
                                      Using br As New SolidBrush(Color.White)
                                          g.DrawString(name, FntNavBold, br, New RectangleF(10, 14, 190, 22))
                                      End Using
                                      Using br As New SolidBrush(Color.FromArgb(140, 180, 210))
                                          g.DrawString(SessionManager.Role, FntSmall, br, New RectangleF(10, 38, 190, 18))
                                      End Using
                                  End Sub
        pnlSidebar.Controls.Add(pnlUser)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' HEADER
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub PaintHeaderShadow(sender As Object, e As PaintEventArgs)
        Dim p = DirectCast(sender, Panel)
        ' ظل سفلي ناعم
        For i As Integer = 0 To 3
            Using pen As New Pen(Color.FromArgb(10 - i * 2, 0, 0, 0))
                e.Graphics.DrawLine(pen, 0, p.Height - 4 + i, p.Width, p.Height - 4 + i)
            End Using
        Next
    End Sub

    Private Sub BuildHeader()
        ' ── Welcome section (right) ──────────────────────────────────────
        Dim pnlWelcome As New Panel With {
            .Dock = DockStyle.Right,
            .Width = 420,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 12, 20, 12)
        }

        lblWelcome = New Label With {
            .Text = "مرحبًا، " & If(SessionManager.FullName, SessionManager.UserName) & " 👋",
            .Dock = DockStyle.Top,
            .Height = 30,
            .Font = FntWelcome,
            .ForeColor = ClrText,
            .TextAlign = ContentAlignment.MiddleRight
        }

        lblSubWelcome = New Label With {
            .Text = DateTime.Now.ToString("dddd، d MMMM yyyy"),
            .Dock = DockStyle.Fill,
            .Font = FntSub,
            .ForeColor = ClrSubText,
            .TextAlign = ContentAlignment.TopRight
        }

        pnlWelcome.Controls.Add(lblSubWelcome)
        pnlWelcome.Controls.Add(lblWelcome)

        ' ── Actions (left): Notification + Branch Filter ─────────────────
        Dim pnlActions As New Panel With {
            .Dock = DockStyle.Left,
            .Width = 300,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 16, 0, 16)
        }

        ' Notification button
        pnlNotifIcon = New Panel With {
            .Size = New Size(46, 46),
            .Location = New Point(14, 17),
            .BackColor = Color.FromArgb(240, 244, 250),
            .Cursor = Cursors.Hand
        }
        AddHandler pnlNotifIcon.Paint, Sub(s, e)
                                           Dim g = e.Graphics
                                           g.SmoothingMode = SmoothingMode.AntiAlias
                                           Dim rc = DirectCast(s, Panel).ClientRectangle
                                           Using br As New SolidBrush(Color.FromArgb(240, 244, 250))
                                               g.FillEllipse(br, 2, 2, 40, 40)
                                           End Using
                                           Using pen As New Pen(ClrBorder)
                                               g.DrawEllipse(pen, 2, 2, 40, 40)
                                           End Using
                                           g.DrawString("🔔", New Font("Segoe UI Emoji", 14), Brushes.SlateGray, 9, 8)
                                           If _unreadNotifCount > 0 Then
                                               Using br As New SolidBrush(ClrRose)
                                                   g.FillEllipse(br, 28, 4, 16, 16)
                                               End Using
                                               Using br As New SolidBrush(Color.White)
                                                   Dim txt = If(_unreadNotifCount > 9, "9+", _unreadNotifCount.ToString())
                                                   Dim sz = g.MeasureString(txt, FntBadge)
                                                   g.DrawString(txt, FntBadge, br, CSng(36 - sz.Width / 2), 6)
                                               End Using
                                           End If
                                       End Sub
        AddHandler pnlNotifIcon.Click, Sub(s, e) RaiseEvent OpenNotificationsRequested(Me, EventArgs.Empty)

        ' Branch filter
        Dim pnlFilter As New Panel With {
            .Size = New Size(200, 46),
            .Location = New Point(70, 17),
            .BackColor = Color.Transparent
        }
        cboBranch = New ComboBox With {
            .Dock = DockStyle.Fill,
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Font = FntSmall,
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(240, 244, 250)
        }
        AddHandler cboBranch.SelectedIndexChanged, AddressOf OnBranchChanged
        pnlFilter.Controls.Add(cboBranch)

        pnlActions.Controls.Add(pnlNotifIcon)
        pnlActions.Controls.Add(pnlFilter)

        pnlHeader.Controls.Add(pnlWelcome)
        pnlHeader.Controls.Add(pnlActions)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' KPI CARDS
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub BuildKpiCards()
        Dim tbl As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 4,
            .RowCount = 1,
            .BackColor = Color.Transparent
        }
        For i = 0 To 3
            tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        Next

        Dim kpis As New List(Of KpiData) From {
            New KpiData("إجمالي المرضى", "مريض مسجّل", "patients", "👥", ClrPurple, Color.FromArgb(109, 40, 217)),
            New KpiData("مواعيد اليوم", "موعد محجوز", "appointments", "📅", ClrGreen, Color.FromArgb(5, 150, 105)),
            New KpiData("إجمالي الفروع", "فرع نشط", "branches", "🏢", ClrBlue, Color.FromArgb(37, 99, 235)),
            New KpiData("إيرادات الشهر", "جنيه مصري", "revenue", "💰", ClrOrange, Color.FromArgb(217, 119, 6))
        }

        For Each kpi In kpis
            Dim c1 = kpi.Clr1
            Dim c2 = kpi.Clr2
            Dim icon = kpi.Icon

            Dim card As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = ClrCard,
                .Margin = New Padding(6, 0, 6, 0),
                .Cursor = Cursors.Hand,
                .Tag = kpi.Key
            }

            ' رسم البطاقة مع الأيقونة والظل
            AddHandler card.Paint, Sub(s, eArgs)
                                       Dim pp = DirectCast(s, Panel)
                                       Dim g = eArgs.Graphics
                                       g.SmoothingMode = SmoothingMode.AntiAlias

                                       ' ظل ناعم
                                       Using pen As New Pen(Color.FromArgb(12, 0, 0, 0))
                                           g.DrawRectangle(pen, 1, 1, pp.Width - 3, pp.Height - 3)
                                       End Using
                                       Using pen As New Pen(Color.FromArgb(6, 0, 0, 0))
                                           g.DrawRectangle(pen, 0, 0, pp.Width - 1, pp.Height - 1)
                                       End Using

                                       ' شريط علوي ملون (gradient)
                                       Dim topRect = New Rectangle(0, 0, pp.Width, 5)
                                       Using br As New LinearGradientBrush(topRect, c1, c2, LinearGradientMode.Horizontal)
                                           g.FillRectangle(br, topRect)
                                       End Using

                                       ' أيقونة دائرية في اليمين
                                       Dim iconRect = New Rectangle(pp.Width - 68, 20, 48, 48)
                                       Using br As New SolidBrush(Color.FromArgb(18, c1.R, c1.G, c1.B))
                                           g.FillEllipse(br, iconRect)
                                       End Using
                                       Using br As New SolidBrush(Color.FromArgb(35, c1.R, c1.G, c1.B))
                                           g.FillEllipse(br, iconRect.X + 4, iconRect.Y + 4, 40, 40)
                                       End Using
                                       g.DrawString(icon, New Font("Segoe UI Emoji", 18), New SolidBrush(c1), iconRect.X + 5, iconRect.Y + 7)
                                   End Sub

            ' Labels داخل البطاقة
            Dim lblTitle As New Label With {
                .Text = kpi.Title,
                .Font = FntKpiTitle,
                .ForeColor = ClrSubText,
                .Location = New Point(14, 18),
                .AutoSize = True
            }
            Dim lblVal As New Label With {
                .Text = "...",
                .Font = FntKpiValue,
                .ForeColor = ClrText,
                .Location = New Point(10, 38),
                .AutoSize = True,
                .Tag = kpi.Key
            }
            Dim lblSub As New Label With {
                .Text = kpi.Sub1,
                .Font = FntKpiSub,
                .ForeColor = ClrSubText,
                .Location = New Point(14, 95),
                .AutoSize = True
            }

            If kpi.Key = "patients" Then lblKpiPatients = lblVal
            If kpi.Key = "appointments" Then lblKpiAppointments = lblVal
            If kpi.Key = "branches" Then lblKpiBranches = lblVal
            If kpi.Key = "revenue" Then lblKpiRevenue = lblVal

            ' Hover effect
            AddHandler card.MouseEnter, Sub(s, e)
                                            DirectCast(s, Panel).BackColor = ClrCardHover
                                        End Sub
            AddHandler card.MouseLeave, Sub(s, e)
                                            DirectCast(s, Panel).BackColor = ClrCard
                                        End Sub

            card.Controls.Add(lblTitle)
            card.Controls.Add(lblVal)
            card.Controls.Add(lblSub)
            tbl.Controls.Add(card)
        Next

        pnlKpi.Controls.Add(tbl)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' CONTENT: 3 columns
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub BuildContentColumns()
        Dim tbl As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 3,
            .RowCount = 1,
            .BackColor = Color.Transparent
        }
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40))
        tbl.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33))

        ' Column 1: Branch list
        pnlBranchList = BuildCardPanel("🏢  الفروع")
        Dim lnkBranches As New LinkLabel With {
            .Text = "← عرض جميع الفروع",
            .Dock = DockStyle.Bottom,
            .Height = 30,
            .Font = FntSmall,
            .LinkColor = ClrTeal,
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 10, 0)
        }
        AddHandler lnkBranches.LinkClicked, Sub(s, e) RaiseEvent OpenBranchesRequested(Me, EventArgs.Empty)
        pnlBranchList.Controls.Add(lnkBranches)

        ' Column 2: Revenue chart
        pnlChart = BuildCardPanel("📈  الإيرادات — آخر 6 أشهر")
        Dim chartCanvas As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent,
            .Tag = "chartcanvas"
        }
        AddHandler chartCanvas.Paint, AddressOf DrawRevenueChart
        pnlChart.Controls.Add(chartCanvas)

        ' Column 3: Upcoming appointments
        pnlAppList = BuildCardPanel("📅  المواعيد القادمة")
        Dim lnkApps As New LinkLabel With {
            .Text = "← عرض جميع المواعيد",
            .Dock = DockStyle.Bottom,
            .Height = 30,
            .Font = FntSmall,
            .LinkColor = ClrTeal,
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 10, 0)
        }
        AddHandler lnkApps.LinkClicked, Sub(s, e) RaiseEvent OpenAppointmentsRequested(Me, EventArgs.Empty)
        pnlAppList.Controls.Add(lnkApps)

        tbl.Controls.Add(pnlBranchList, 0, 0)
        tbl.Controls.Add(pnlChart, 1, 0)
        tbl.Controls.Add(pnlAppList, 2, 0)

        pnlContent.Controls.Add(tbl)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' QUICK ACTIONS
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub PaintQuickActionsTop(sender As Object, e As PaintEventArgs)
        Dim p = DirectCast(sender, Panel)
        ' خط علوي ملون
        Using br As New LinearGradientBrush(New Rectangle(0, 0, p.Width, 2), ClrTeal, ClrBlue, LinearGradientMode.Horizontal)
            e.Graphics.FillRectangle(br, 0, 0, p.Width, 2)
        End Using
    End Sub

    Private Sub BuildQuickActions()
        Dim lblTitle As New Label With {
            .Text = "⚡  إجراءات سريعة",
            .Dock = DockStyle.Right,
            .Width = 160,
            .Font = FntSectionTitle,
            .ForeColor = ClrText,
            .TextAlign = ContentAlignment.MiddleRight,
            .Padding = New Padding(0, 0, 6, 0)
        }

        Dim flow As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .WrapContents = False,
            .BackColor = Color.Transparent,
            .Padding = New Padding(0, 6, 10, 6)
        }

        Dim actions As New List(Of ActionItemData) From {
            New ActionItemData("📅", "حجز موعد جديد", "Appointments", ClrBlue, Color.FromArgb(37, 99, 235)),
            New ActionItemData("👥", "إضافة مريض", "Patients", ClrGreen, Color.FromArgb(5, 150, 105)),
            New ActionItemData("🦷", "إضافة علاج", "Treatments", ClrOrange, Color.FromArgb(217, 119, 6)),
            New ActionItemData("🧾", "فاتورة جديدة", "Invoices", ClrPurple, Color.FromArgb(109, 40, 217)),
            New ActionItemData("📊", "التقارير", "Reports", ClrTeal, Color.FromArgb(0, 120, 110))
        }

        For Each act In actions
            Dim a1 = act.Clr1
            Dim a2 = act.Clr2
            Dim actIcon = act.Icon
            Dim actCaption = act.Caption

            Dim btn As New Button With {
                .Text = act.Caption,
                .Size = New Size(145, 62),
                .Margin = New Padding(8, 0, 0, 0),
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .ForeColor = Color.White,
                .BackColor = a1,
                .TextAlign = ContentAlignment.BottomCenter,
                .Cursor = Cursors.Hand,
                .Tag = act.Ev,
                .ImageAlign = ContentAlignment.TopCenter,
                .TextImageRelation = TextImageRelation.ImageAboveText,
                .Padding = New Padding(0, 6, 0, 4)
            }
            btn.FlatAppearance.BorderSize = 0

            ' رسم gradient على الزر
            AddHandler btn.Paint, Sub(s, eArgs)
                                      Dim b = DirectCast(s, Button)
                                      Dim g = eArgs.Graphics
                                      g.SmoothingMode = SmoothingMode.AntiAlias
                                      Using br As New LinearGradientBrush(b.ClientRectangle, a1, a2, LinearGradientMode.ForwardDiagonal)
                                          g.FillRectangle(br, b.ClientRectangle)
                                      End Using
                                      ' أيقونة
                                      Dim iconFont = New Font("Segoe UI Emoji", 16)
                                      Dim iconSz = g.MeasureString(actIcon, iconFont)
                                      g.DrawString(actIcon, iconFont, Brushes.White, CSng((b.Width - iconSz.Width) / 2), 6.0F)
                                      ' نص
                                      Dim txtFont = New Font("Segoe UI", 8.5F, FontStyle.Bold)
                                      Dim txtSz = g.MeasureString(actCaption, txtFont)
                                      g.DrawString(actCaption, txtFont, Brushes.White, CSng((b.Width - txtSz.Width) / 2), CSng(b.Height - 20))
                                  End Sub

            AddHandler btn.MouseEnter, Sub(s, e)
                                           DirectCast(s, Button).BackColor = Color.FromArgb(220, a1.R, a1.G, a1.B)
                                       End Sub
            AddHandler btn.MouseLeave, Sub(s, e)
                                           DirectCast(s, Button).BackColor = a1
                                       End Sub
            AddHandler btn.Click, AddressOf QuickAction_Click

            flow.Controls.Add(btn)
        Next

        pnlQuickActions.Controls.Add(flow)
        pnlQuickActions.Controls.Add(lblTitle)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' LOAD & REFRESH
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub OnDashboardLoad(sender As Object, e As EventArgs)
        LoadBranchFilter()
        RefreshAll()
    End Sub

    Private Sub LoadBranchFilter()
        Try
            cboBranch.Items.Clear()
            cboBranch.Items.Add(New BranchItem(0, "🏢  جميع الفروع"))
            For Each row As DataRow In _perfDal.GetActiveBranches().Rows
                cboBranch.Items.Add(New BranchItem(CInt(row("BranchID")), row("BranchName").ToString()))
            Next
            cboBranch.SelectedIndex = 0
        Catch ex As Exception
            Logger.LogError("Dashboard.LoadBranchFilter: " & ex.Message)
        End Try
    End Sub

    Private Sub OnBranchChanged(sender As Object, e As EventArgs)
        RefreshAll()
    End Sub

    Public Sub RefreshAll()
        Try
            Dim branchId = GetSelectedBranch()
            RefreshKpi(branchId)
            RefreshBranchList(branchId)
            RefreshChartData(branchId)
            RefreshAppointments(branchId)
        Catch ex As Exception
            Logger.LogError("Dashboard.RefreshAll: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshKpi(branchId As Integer)
        Try
            Dim summary = _bal.GetSummary(branchId)
            SafeSetLabel(lblKpiAppointments, summary.TodayAppointments.ToString("N0"))
            SafeSetLabel(lblKpiRevenue, summary.MonthRevenue.ToString("N0") & " ج.م")

            Dim patCount = _perfDal.GetTotalPatientsCount(branchId)
            SafeSetLabel(lblKpiPatients, patCount.ToString("N0"))

            Dim dt = _perfDal.GetActiveBranches()
            Dim branchCount = If(branchId = 0, dt.Rows.Count, 1)
            SafeSetLabel(lblKpiBranches, branchCount.ToString())
        Catch ex As Exception
            Logger.LogError("Dashboard.RefreshKpi: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshBranchList(branchId As Integer)
        Try
            Dim toRemove As New List(Of Control)
            For Each c As Control In pnlBranchList.Controls
                If TypeOf c Is Panel AndAlso c.Tag IsNot Nothing AndAlso c.Tag.ToString() = "branch_scroll" Then
                    toRemove.Add(c)
                End If
            Next
            For Each c In toRemove : pnlBranchList.Controls.Remove(c) : Next

            Dim branches = _perfDal.GetActiveBranches()
            Dim pnlScroll As New Panel With {
                .Dock = DockStyle.Fill,
                .AutoScroll = True,
                .BackColor = Color.Transparent,
                .Tag = "branch_scroll"
            }

            Dim y = 0
            For Each row As DataRow In branches.Rows
                Dim rowPanel = BuildBranchRow(
                    row("BranchName").ToString(),
                    If(row.Table.Columns.Contains("City"), row("City").ToString(), ""),
                    y)
                pnlScroll.Controls.Add(rowPanel)
                y += 66
            Next

            pnlBranchList.Controls.Add(pnlScroll)
        Catch ex As Exception
            Logger.LogError("Dashboard.RefreshBranchList: " & ex.Message)
        End Try
    End Sub

    Private Function BuildBranchRow(name As String, city As String, y As Integer) As Panel
        Dim p As New Panel With {
            .Location = New Point(8, y + 4),
            .Size = New Size(pnlBranchList.Width - 24, 58),
            .BackColor = ClrBg,
            .Cursor = Cursors.Hand,
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        }

        AddHandler p.Paint, Sub(s, eArgs)
                                Dim pp = DirectCast(s, Panel)
                                Dim g = eArgs.Graphics
                                g.SmoothingMode = SmoothingMode.AntiAlias

                                ' بطاقة ناعمة مع حد خفيف
                                Using br As New SolidBrush(Color.White)
                                    g.FillRectangle(br, pp.ClientRectangle)
                                End Using
                                Using pen As New Pen(ClrBorder)
                                    g.DrawRectangle(pen, 0, 0, pp.Width - 1, pp.Height - 1)
                                End Using
                                ' شريط يمين بلون الفرع
                                Using br As New LinearGradientBrush(New Rectangle(pp.Width - 6, 0, 6, pp.Height), ClrTeal, ClrBlue, LinearGradientMode.Vertical)
                                    g.FillRectangle(br, pp.Width - 6, 4, 5, pp.Height - 8)
                                End Using
                                ' أيقونة بناء
                                g.DrawString("🏢", New Font("Segoe UI Emoji", 16), Brushes.CornflowerBlue, 8, 12)
                                ' اسم الفرع
                                Using br As New SolidBrush(ClrText)
                                    g.DrawString(name, FntNavBold, br, New RectangleF(38, 8, pp.Width - 60, 22))
                                End Using
                                ' المدينة
                                Using br As New SolidBrush(ClrSubText)
                                    g.DrawString("📍 " & city, FntSmall, br, New RectangleF(38, 32, pp.Width - 60, 18))
                                End Using
                                ' نقطة نشط
                                Using br As New SolidBrush(ClrGreen)
                                    g.FillEllipse(br, 8, 8, 8, 8)
                                End Using
                            End Sub

        AddHandler p.MouseEnter, Sub(s, e) DirectCast(s, Panel).BackColor = Color.FromArgb(248, 252, 255)
        AddHandler p.MouseLeave, Sub(s, e) DirectCast(s, Panel).BackColor = ClrBg

        Return p
    End Function

    Private Sub RefreshChartData(branchId As Integer)
        Try
            _chartData = New List(Of Decimal)
            _chartLabels = New List(Of String)

            For m As Integer = 5 To 0 Step -1
                Dim dt = DateTime.Now.AddMonths(-m)
                _chartLabels.Add(dt.ToString("MMM", New System.Globalization.CultureInfo("ar-EG")))
                _chartData.Add(_perfDal.GetMonthRevenue(branchId, dt.Year, dt.Month))
            Next

            pnlChart.Invalidate(True)
        Catch ex As Exception
            Logger.LogError("Dashboard.RefreshChartData: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshAppointments(branchId As Integer)
        Try
            Dim toRemove As New List(Of Control)
            For Each c As Control In pnlAppList.Controls
                If TypeOf c Is Panel AndAlso c.Tag IsNot Nothing AndAlso c.Tag.ToString() = "app_scroll" Then
                    toRemove.Add(c)
                End If
            Next
            For Each c In toRemove : pnlAppList.Controls.Remove(c) : Next

            Dim apps = _bal.GetTodayAppointments(branchId)
            Dim pnlScroll As New Panel With {
                .Dock = DockStyle.Fill,
                .AutoScroll = True,
                .BackColor = Color.Transparent,
                .Tag = "app_scroll"
            }

            Dim dotColors As Color() = {ClrBlue, ClrGreen, ClrOrange, ClrPurple, ClrTeal}
            Dim y = 0
            Dim i = 0
            For Each row As DataRow In apps.Rows
                If i >= 5 Then Exit For
                Dim time = If(apps.Columns.Contains("AppTime"), row("AppTime").ToString(), "--")
                Dim pat = If(apps.Columns.Contains("PatientName"), row("PatientName").ToString(), "")
                Dim doc = If(apps.Columns.Contains("DoctorName"), row("DoctorName").ToString(), "")
                Dim branch = If(apps.Columns.Contains("BranchName"), row("BranchName").ToString(), "")
                pnlScroll.Controls.Add(BuildAppRow(time, pat, doc, branch, dotColors(i Mod dotColors.Length), y))
                y += 82
                i += 1
            Next

            If i = 0 Then
                Dim lblEmpty As New Label With {
                    .Text = "😊  لا توجد مواعيد اليوم",
                    .Dock = DockStyle.Top,
                    .Height = 60,
                    .Font = FntSmall,
                    .ForeColor = ClrSubText,
                    .TextAlign = ContentAlignment.MiddleCenter
                }
                pnlScroll.Controls.Add(lblEmpty)
            End If

            pnlAppList.Controls.Add(pnlScroll)
        Catch ex As Exception
            Logger.LogError("Dashboard.RefreshAppointments: " & ex.Message)
        End Try
    End Sub

    Private Function BuildAppRow(time As String, patient As String, doctor As String,
                                 branch As String, dotColor As Color, y As Integer) As Panel
        Dim p As New Panel With {
            .Location = New Point(6, y + 4),
            .Size = New Size(pnlAppList.Width - 20, 74),
            .BackColor = Color.White,
            .Cursor = Cursors.Hand,
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        }

        AddHandler p.Paint, Sub(s, eArgs)
                                Dim pp = DirectCast(s, Panel)
                                Dim g = eArgs.Graphics
                                g.SmoothingMode = SmoothingMode.AntiAlias

                                ' حد ناعم
                                Using pen As New Pen(ClrBorder)
                                    g.DrawRectangle(pen, 0, 0, pp.Width - 1, pp.Height - 1)
                                End Using
                                ' شريط وقت ملون يمين
                                Using br As New SolidBrush(Color.FromArgb(20, dotColor.R, dotColor.G, dotColor.B))
                                    g.FillRectangle(br, pp.Width - 70, 0, 70, pp.Height)
                                End Using
                                Using br As New SolidBrush(dotColor)
                                    Dim timeSz = g.MeasureString(time, New Font("Segoe UI", 9, FontStyle.Bold))
                                    g.DrawString(time, New Font("Segoe UI", 9, FontStyle.Bold), br,
                             pp.Width - 70 + (70 - timeSz.Width) / 2, (pp.Height - timeSz.Height) / 2)
                                End Using
                                ' نقطة ملونة
                                Using br As New SolidBrush(dotColor)
                                    g.FillEllipse(br, pp.Width - 80, CInt(pp.Height / 2) - 5, 8, 8)
                                End Using
                                ' avatar مستخدم
                                Using br As New SolidBrush(Color.FromArgb(20, dotColor.R, dotColor.G, dotColor.B))
                                    g.FillEllipse(br, 10, 14, 38, 38)
                                End Using
                                g.DrawString("👤", New Font("Segoe UI Emoji", 14), New SolidBrush(dotColor), 12, 16)
                                ' اسم المريض
                                Using br As New SolidBrush(ClrText)
                                    g.DrawString(patient, FntNavBold, br, New RectangleF(54, 10, pp.Width - 140, 22))
                                End Using
                                ' الطبيب
                                Using br As New SolidBrush(ClrSubText)
                                    g.DrawString("د. " & doctor, FntSmall, br, New RectangleF(54, 34, pp.Width - 140, 18))
                                End Using
                                ' الفرع
                                Using br As New SolidBrush(ClrSubText)
                                    g.DrawString("📍 " & branch, FntSmall, br, New RectangleF(54, 52, pp.Width - 140, 16))
                                End Using
                            End Sub

        AddHandler p.MouseEnter, Sub(s, e)
                                     DirectCast(s, Panel).BackColor = Color.FromArgb(250, 252, 255)
                                 End Sub
        AddHandler p.MouseLeave, Sub(s, e)
                                     DirectCast(s, Panel).BackColor = Color.White
                                 End Sub

        Return p
    End Function

    ' ─────────────────────────────────────────────────────────────────────
    ' CHART PAINTING — Enhanced
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub DrawRevenueChart(sender As Object, e As PaintEventArgs)
        If _chartData Is Nothing OrElse _chartData.Count = 0 Then Return

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

        Dim canvas = DirectCast(sender, Panel)
        Dim rc = canvas.ClientRectangle
        Dim padL = 52, padR = 16, padT = 16, padB = 40
        Dim w = rc.Width - padL - padR
        Dim h = rc.Height - padT - padB

        Dim maxVal = CDec(_chartData.Max())
        If maxVal = 0 Then maxVal = 1

        ' خلفية المخطط
        Using br As New SolidBrush(Color.FromArgb(250, 252, 255))
            g.FillRectangle(br, padL, padT, w, h)
        End Using

        ' خطوط شبكة أفقية
        For j = 0 To 4
            Dim gy = padT + CInt(h * j / 4)
            Using gridPen As New Pen(Color.FromArgb(220, 228, 240), 1)
                gridPen.DashStyle = DashStyle.Dash
                g.DrawLine(gridPen, padL, gy, padL + w, gy)
            End Using
            Dim label = ((maxVal * (4 - j) / 4) / 1000).ToString("N0") & "K"
            Using br As New SolidBrush(ClrSubText)
                g.DrawString(label, New Font("Segoe UI", 7.5F), br, 2, gy - 9)
            End Using
        Next

        If _chartData.Count > 1 Then
            ' نقاط المخطط
            Dim pts(_chartData.Count - 1) As PointF
            For j = 0 To _chartData.Count - 1
                Dim cx = padL + CInt(w * j / (_chartData.Count - 1))
                Dim cy = padT + h - CInt(h * _chartData(j) / maxVal)
                pts(j) = New PointF(cx, cy)
            Next

            ' منطقة تعبئة gradient
            Dim fillPts(_chartData.Count + 1) As PointF
            Array.Copy(pts, fillPts, pts.Length)
            fillPts(pts.Length) = New PointF(padL + w, padT + h)
            fillPts(pts.Length + 1) = New PointF(padL, padT + h)

            Using br As New LinearGradientBrush(
                    New PointF(0, padT), New PointF(0, padT + h),
                    Color.FromArgb(70, ClrTeal.R, ClrTeal.G, ClrTeal.B),
                    Color.FromArgb(5, ClrTeal.R, ClrTeal.G, ClrTeal.B))
                g.FillPolygon(br, fillPts)
            End Using

            ' خط المخطط بتدرج Teal→Blue
            Using linePen As New Pen(ClrTeal, 3)
                linePen.StartCap = LineCap.Round
                linePen.EndCap = LineCap.Round
                linePen.LineJoin = LineJoin.Round
                g.DrawLines(linePen, pts)
            End Using

            ' نقاط البيانات + قيم
            For j = 0 To _chartData.Count - 1
                Dim cx = CInt(pts(j).X)
                Dim cy = CInt(pts(j).Y)

                ' حلقة خارجية
                Using br As New SolidBrush(Color.FromArgb(40, ClrTeal.R, ClrTeal.G, ClrTeal.B))
                    g.FillEllipse(br, cx - 8, cy - 8, 16, 16)
                End Using
                ' دائرة بيضاء
                Using br As New SolidBrush(Color.White)
                    g.FillEllipse(br, cx - 5, cy - 5, 10, 10)
                End Using
                ' دائرة Teal داخلية
                Using br As New SolidBrush(ClrTeal)
                    g.FillEllipse(br, cx - 3, cy - 3, 6, 6)
                End Using

                ' تسمية الشهر
                If _chartLabels IsNot Nothing AndAlso j < _chartLabels.Count Then
                    Using br As New SolidBrush(ClrSubText)
                        Dim lbl = _chartLabels(j)
                        Dim lblSz = g.MeasureString(lbl, New Font("Segoe UI", 8))
                        g.DrawString(lbl, New Font("Segoe UI", 8), br, CSng(cx - lblSz.Width / 2), CSng(padT + h + 6))
                    End Using
                End If

                ' قيمة فوق النقطة
                If _chartData(j) > 0 Then
                    Dim valTxt = (_chartData(j) / 1000).ToString("N0") & "K"
                    Using br As New SolidBrush(ClrTeal)
                        Dim valSz = g.MeasureString(valTxt, New Font("Segoe UI", 7.5F, FontStyle.Bold))
                        g.DrawString(valTxt, New Font("Segoe UI", 7.5F, FontStyle.Bold), br,
                                     CSng(cx - valSz.Width / 2), CSng(cy - 20))
                    End Using
                End If
            Next
        End If

        ' إطار المخطط
        Using pen As New Pen(ClrBorder)
            g.DrawRectangle(pen, padL, padT, w, h)
        End Using
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' HELPERS
    ' ─────────────────────────────────────────────────────────────────────
    Private Function BuildCardPanel(title As String) As Panel
        Dim card As New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = ClrCard,
            .Margin = New Padding(6, 0, 6, 0),
            .Padding = New Padding(0, 44, 0, 0)
        }

        AddHandler card.Paint, Sub(s, eArgs)
                                   Dim pp = DirectCast(s, Panel)
                                   Dim g = eArgs.Graphics
                                   g.SmoothingMode = SmoothingMode.AntiAlias

                                   ' ظل ناعم
                                   Using pen As New Pen(Color.FromArgb(8, 0, 0, 0))
                                       g.DrawRectangle(pen, 0, 0, pp.Width - 1, pp.Height - 1)
                                   End Using
                                   Using pen As New Pen(Color.FromArgb(14, 0, 0, 0))
                                       g.DrawRectangle(pen, 1, 1, pp.Width - 3, pp.Height - 3)
                                   End Using

                                   ' شريط عنوان علوي بتدرج خفيف
                                   Using br As New LinearGradientBrush(
                    New Rectangle(0, 0, pp.Width, 44),
                    Color.FromArgb(252, 254, 255),
                    Color.White, LinearGradientMode.Vertical)
                                       g.FillRectangle(br, 0, 0, pp.Width, 44)
                                   End Using

                                   ' عنوان القسم
                                   Using br As New SolidBrush(ClrText)
                                       g.DrawString(title, FntSectionTitle, br, New RectangleF(12, 10, pp.Width - 24, 26))
                                   End Using

                                   ' خط فاصل تحت العنوان
                                   Using pen As New Pen(ClrBorder)
                                       g.DrawLine(pen, 12, 42, pp.Width - 12, 42)
                                   End Using
                               End Sub

        Return card
    End Function

    Private Sub SafeSetLabel(lbl As Label, text As String)
        If lbl Is Nothing Then Return
        If lbl.InvokeRequired Then
            lbl.Invoke(Sub() lbl.Text = text)
        Else
            lbl.Text = text
        End If
    End Sub

    Private Function GetSelectedBranch() As Integer
        Dim item As BranchItem = Nothing
        If cboBranch IsNot Nothing Then
            item = TryCast(cboBranch.SelectedItem, BranchItem)
        End If
        Return If(item Is Nothing, 0, item.Id)
    End Function

    ' ─────────────────────────────────────────────────────────────────────
    ' EVENT HANDLERS
    ' ─────────────────────────────────────────────────────────────────────
    Private Sub NavItem_Click(sender As Object, e As EventArgs)
        Dim ctrl = DirectCast(sender, Control)
        Dim tag As String = Nothing
        If ctrl.Tag IsNot Nothing Then
            tag = ctrl.Tag.ToString()
        End If
        If tag Is Nothing AndAlso ctrl.Parent IsNot Nothing AndAlso ctrl.Parent.Tag IsNot Nothing Then
            tag = ctrl.Parent.Tag.ToString()
        End If

        ' تحديث الـ active state
        Dim clickedPanel As Panel = Nothing
        If TypeOf ctrl Is Panel Then
            clickedPanel = DirectCast(ctrl, Panel)
        ElseIf TypeOf ctrl Is Label Then
            clickedPanel = TryCast(ctrl.Parent, Panel)
        End If

        If clickedPanel IsNot Nothing AndAlso clickedPanel IsNot _activeNavPanel Then
            If _activeNavPanel IsNot Nothing Then
                _activeNavPanel.BackColor = Color.Transparent
                For Each c As Control In _activeNavPanel.Controls
                    If TypeOf c Is Label Then
                        Dim lbl = DirectCast(c, Label)
                        lbl.ForeColor = Color.FromArgb(160, 190, 220)
                        lbl.Font = FntNav
                    End If
                Next
                _activeNavPanel.Invalidate()
            End If
            clickedPanel.BackColor = ClrSidebarActiveBg
            For Each c As Control In clickedPanel.Controls
                If TypeOf c Is Label Then
                    Dim lbl = DirectCast(c, Label)
                    lbl.ForeColor = Color.White
                    lbl.Font = FntNavBold
                End If
            Next
            clickedPanel.Invalidate()
            _activeNavPanel = clickedPanel
        End If

        Select Case tag
            Case "Branches" : RaiseEvent OpenBranchesRequested(Me, EventArgs.Empty)
            Case "Appointments" : RaiseEvent OpenAppointmentsRequested(Me, EventArgs.Empty)
            Case "Patients" : RaiseEvent OpenPatientsRequested(Me, EventArgs.Empty)
            Case "Doctors" : RaiseEvent OpenDoctorsRequested(Me, EventArgs.Empty)
            Case "Users" : RaiseEvent OpenUsersRequested(Me, EventArgs.Empty)
            Case "Roles" : RaiseEvent OpenRolesRequested(Me, EventArgs.Empty)
            Case "Permissions" : RaiseEvent OpenPermissionsRequested(Me, EventArgs.Empty)
            Case "Treatments" : RaiseEvent OpenTreatmentsRequested(Me, EventArgs.Empty)
            Case "Invoices" : RaiseEvent OpenInvoicesRequested(Me, EventArgs.Empty)
            Case "Warehouse" : RaiseEvent OpenWarehouseRequested(Me, EventArgs.Empty)
            Case "Reports" : RaiseEvent OpenReportsRequested(Me, EventArgs.Empty)
            Case "Settings" : RaiseEvent OpenSettingsRequested(Me, EventArgs.Empty)
        End Select
    End Sub

    Private Sub QuickAction_Click(sender As Object, e As EventArgs)
        Dim btnCtrl = DirectCast(sender, Button)
        Dim tag As String = Nothing
        If btnCtrl.Tag IsNot Nothing Then tag = btnCtrl.Tag.ToString()
        Select Case tag
            Case "Appointments" : RaiseEvent OpenAppointmentsRequested(Me, EventArgs.Empty)
            Case "Patients" : RaiseEvent OpenPatientsRequested(Me, EventArgs.Empty)
            Case "Treatments" : RaiseEvent OpenTreatmentsRequested(Me, EventArgs.Empty)
            Case "Invoices" : RaiseEvent OpenInvoicesRequested(Me, EventArgs.Empty)
            Case "Reports" : RaiseEvent OpenReportsRequested(Me, EventArgs.Empty)
            Case "Branches" : RaiseEvent OpenBranchesRequested(Me, EventArgs.Empty)
        End Select
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' PUBLIC API
    ' ─────────────────────────────────────────────────────────────────────
    Public Sub SetUnreadNotificationCount(count As Integer)
        _unreadNotifCount = count
        If pnlNotifIcon IsNot Nothing Then pnlNotifIcon.Invalidate()
    End Sub

    ' ─────────────────────────────────────────────────────────────────────
    ' INNER CLASS
    ' ─────────────────────────────────────────────────────────────────────
    Private Class BranchItem
        Public ReadOnly Id As Integer
        Public ReadOnly Name As String
        Public Sub New(id As Integer, name As String)
            Me.Id = id
            Me.Name = name
        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Class NavItemData
        Public Icon As String
        Public Caption As String
        Public IsActive As Boolean
        Public Ev As String
        Public Sub New(icon As String, caption As String, isActive As Boolean, ev As String)
            Me.Icon = icon : Me.Caption = caption : Me.IsActive = isActive : Me.Ev = ev
        End Sub
    End Class

    Private Class KpiData
        Public Title As String
        Public Sub1 As String
        Public Key As String
        Public Icon As String
        Public Clr1 As Color
        Public Clr2 As Color
        Public Sub New(title As String, sub1 As String, key As String, icon As String, clr1 As Color, clr2 As Color)
            Me.Title = title : Me.Sub1 = sub1 : Me.Key = key : Me.Icon = icon : Me.Clr1 = clr1 : Me.Clr2 = clr2
        End Sub
    End Class

    Private Class ActionItemData
        Public Icon As String
        Public Caption As String
        Public Ev As String
        Public Clr1 As Color
        Public Clr2 As Color
        Public Sub New(icon As String, caption As String, ev As String, clr1 As Color, clr2 As Color)
            Me.Icon = icon : Me.Caption = caption : Me.Ev = ev : Me.Clr1 = clr1 : Me.Clr2 = clr2
        End Sub
    End Class

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'AdminDashboardPanel
        '
        Me.Name = "AdminDashboardPanel"
        Me.ResumeLayout(False)

    End Sub

    Private Sub AdminDashboardPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class