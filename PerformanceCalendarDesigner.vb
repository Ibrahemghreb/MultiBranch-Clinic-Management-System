Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' محرك رسم التقويم الشهري - Custom Control يرسم الخلايا والأيقونات
''' يُستخدَم داخل frmPerformance كـ Panel مرسوم
''' </summary>
Public Class PerformanceCalendarDesigner
    Inherits Panel

    ' ── ألوان ──────────────────────────────────────────────────────────────
    Private ReadOnly ClrHeader      As Color = Color.FromArgb(44, 62, 80)       ' رأس الأيام
    Private ReadOnly ClrHeaderText  As Color = Color.White
    Private ReadOnly ClrWeekend     As Color = Color.FromArgb(240, 248, 255)    ' خلفية السبت/الأحد
    Private ReadOnly ClrWeekday     As Color = Color.White
    Private ReadOnly ClrBadge       As Color = Color.FromArgb(0, 150, 136)      ' شارة المواعيد
    Private ReadOnly ClrBadgeHigh   As Color = Color.FromArgb(0, 121, 107)      ' شارة مواعيد كثيرة
    Private ReadOnly ClrTodayBorder As Color = Color.FromArgb(0, 150, 136)
    Private ReadOnly ClrTodayBg     As Color = Color.FromArgb(232, 245, 250)
    Private ReadOnly ClrSelectedBg  As Color = Color.FromArgb(178, 235, 242)
    Private ReadOnly ClrGridLine    As Color = Color.FromArgb(200, 215, 225)
    Private ReadOnly ClrDayNum      As Color = Color.FromArgb(44, 62, 80)
    Private ReadOnly ClrDayNumOther As Color = Color.FromArgb(180, 190, 200)

    ' ── بيانات ─────────────────────────────────────────────────────────────
    Private _year         As Integer = Now.Year
    Private _month        As Integer = Now.Month
    Private _countMap     As New Dictionary(Of Integer, Integer)()
    Private _selectedDay  As Integer = Now.Day
    Private _today        As Date = Date.Today

    ' ── أبعاد الخلية ────────────────────────────────────────────────────────
    Private Const HeaderH As Integer = 36
    Private _cellW As Integer
    Private _cellH As Integer

    ' ── أسماء أيام الأسبوع ──────────────────────────────────────────────────
    Private ReadOnly DayNames() As String = {"Sunday", "Monday", "Tuesday",
                                              "Wednesday", "Thursday", "Friday", "Saturday"}

    ' ── أحداث ───────────────────────────────────────────────────────────────
    Public Event DayClicked(sender As Object, day As Integer)

    Public Sub New()
        Me.DoubleBuffered = True
        Me.BackColor      = Color.FromArgb(245, 248, 252)
        Me.Cursor         = Cursors.Hand
    End Sub

    ' ── خصائص عامة ──────────────────────────────────────────────────────────

    Public Property DisplayYear As Integer
        Get
            Return _year
        End Get
        Set(value As Integer)
            _year = value
            Invalidate()
        End Set
    End Property

    Public Property DisplayMonth As Integer
        Get
            Return _month
        End Get
        Set(value As Integer)
            _month = value
            Invalidate()
        End Set
    End Property

    Public Property SelectedDay As Integer
        Get
            Return _selectedDay
        End Get
        Set(value As Integer)
            _selectedDay = value
            Invalidate()
        End Set
    End Property

    ''' <summary>يُغذّي التقويم ببيانات الأداء (يوم → عدد مواعيد)</summary>
    Public Sub LoadData(countMap As Dictionary(Of Integer, Integer))
        _countMap = If(countMap, New Dictionary(Of Integer, Integer)())
        Invalidate()
    End Sub

    ' ── الرسم الرئيسي ────────────────────────────────────────────────────────

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode      = SmoothingMode.AntiAlias
        g.TextRenderingHint  = Drawing.Text.TextRenderingHint.ClearTypeGridFit

        _cellW = Me.Width  \ 7
        _cellH = If(Me.Height - HeaderH > 0, (Me.Height - HeaderH) \ 6, 80)

        DrawDayHeaders(g)
        DrawCells(g)
    End Sub

    Private Sub DrawDayHeaders(g As Graphics)
        Using hBrush As New SolidBrush(ClrHeader)
            g.FillRectangle(hBrush, 0, 0, Me.Width, HeaderH)
        End Using
        Using fnt   As New Font("Segoe UI", 9.5F, FontStyle.Bold)
            Using fBrush As New SolidBrush(ClrHeaderText)
                For col As Integer = 0 To 6
                    Dim r = New Rectangle(col * _cellW, 0, _cellW, HeaderH)
                    Dim sf As New StringFormat() With {
                        .Alignment = StringAlignment.Center,
                        .LineAlignment = StringAlignment.Center
                    }
                    g.DrawString(DayNames(col), fnt, fBrush, r, sf)
                Next
            End Using
        End Using
    End Sub

    Private Sub DrawCells(g As Graphics)
        Dim firstDay  = New Date(_year, _month, 1)
        Dim daysInMon = Date.DaysInMonth(_year, _month)
        Dim startCol  = CInt(firstDay.DayOfWeek)   ' 0=Sun

        Dim row As Integer = 0
        Dim col As Integer = startCol

        ' أيام الشهر السابق (رمادية)
        Dim prevMonDays = Date.DaysInMonth(
            If(_month = 1, _year - 1, _year),
            If(_month = 1, 12, _month - 1))
        For d = prevMonDays - startCol + 1 To prevMonDays
            DrawCell(g, row, col - (prevMonDays - d + 1), d, isCurrentMonth:=False, count:=0)
        Next

        ' أيام الشهر الحالي
        For day As Integer = 1 To daysInMon
            Dim cnt = 0
            If _countMap.ContainsKey(day) Then cnt = _countMap(day)
            DrawCell(g, row, col, day, isCurrentMonth:=True, count:=cnt)
            col += 1
            If col > 6 Then
                col = 0
                row += 1
            End If
        Next

        ' أيام الشهر التالي
        Dim nextDay = 1
        Do While col <= 6 AndAlso row < 6
            DrawCell(g, row, col, nextDay, isCurrentMonth:=False, count:=0)
            nextDay += 1
            col += 1
            If col > 6 Then
                col = 0
                row += 1
            End If
        Loop
    End Sub

    Private Sub DrawCell(g As Graphics, row As Integer, col As Integer,
                         day As Integer, isCurrentMonth As Boolean, count As Integer)
        Dim x = col * _cellW
        Dim y = HeaderH + row * _cellH
        Dim r = New Rectangle(x, y, _cellW, _cellH)

        Dim isToday    = isCurrentMonth AndAlso
                         _year = _today.Year AndAlso _month = _today.Month AndAlso day = _today.Day
        Dim isSelected = isCurrentMonth AndAlso day = _selectedDay
        Dim isWeekend  = (col = 0 OrElse col = 6)

        ' خلفية الخلية
        Dim bgColor = If(isToday, ClrTodayBg,
                      If(isSelected, ClrSelectedBg,
                      If(isWeekend, ClrWeekend, ClrWeekday)))
        Using bg As New SolidBrush(bgColor)
            g.FillRectangle(bg, r)
        End Using

        ' حدود التقويم
        Using pen As New Pen(ClrGridLine, 0.5F)
            g.DrawRectangle(pen, r)
        End Using

        ' حد خاص لليوم الحالي
        If isToday Then
            Using pen As New Pen(ClrTodayBorder, 2)
                g.DrawRectangle(pen, New Rectangle(x + 1, y + 1, _cellW - 2, _cellH - 2))
            End Using
        End If

        ' رقم اليوم
        Dim dayColor = If(isCurrentMonth, ClrDayNum, ClrDayNumOther)
        Using fnt As New Font("Segoe UI", 9.5F, If(isToday, FontStyle.Bold, FontStyle.Regular))
            Using brush As New SolidBrush(dayColor)
                g.DrawString(day.ToString(), fnt, brush,
                             New PointF(x + 6, y + 5))
            End Using
        End Using

        ' شارة المواعيد
        If isCurrentMonth AndAlso count > 0 Then
            DrawBadge(g, x, y, count)
        End If
    End Sub

    Private Sub DrawBadge(g As Graphics, cellX As Integer, cellY As Integer, count As Integer)
        Dim badgeColor = If(count >= 8, ClrBadgeHigh, ClrBadge)
        Dim bW = _cellW - 12
        Dim bH = 22
        Dim bX = cellX + 6
        Dim bY = cellY + _cellH - bH - 5

        Dim rr = New Rectangle(bX, bY, bW, bH)

        ' خلفية الشارة مدورة
        Using path As New GraphicsPath()
            Dim radius = 6
            path.AddArc(rr.X, rr.Y, radius * 2, radius * 2, 180, 90)
            path.AddArc(rr.Right - radius * 2, rr.Y, radius * 2, radius * 2, 270, 90)
            path.AddArc(rr.Right - radius * 2, rr.Bottom - radius * 2, radius * 2, radius * 2, 0, 90)
            path.AddArc(rr.X, rr.Bottom - radius * 2, radius * 2, radius * 2, 90, 90)
            path.CloseFigure()
            Using br As New SolidBrush(badgeColor)
                g.FillPath(br, path)
            End Using
        End Using

        ' نص الشارة
        Using fnt As New Font("Segoe UI", 7.5F, FontStyle.Bold)
            Using fBrush As New SolidBrush(Color.White)
                Dim sf As New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
                }
                g.DrawString($"{count} appointments", fnt, fBrush, rr, sf)
            End Using
        End Using
    End Sub

    ' ── معالجة النقر ─────────────────────────────────────────────────────────

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)

        If e.Y < HeaderH Then Return  ' نقر على رأس الأيام

        Dim col = e.X \ _cellW
        Dim row = (e.Y - HeaderH) \ _cellH

        Dim firstDay = New Date(_year, _month, 1)
        Dim startCol = CInt(firstDay.DayOfWeek)
        Dim clickedIndex = row * 7 + col - startCol + 1

        Dim daysInMon = Date.DaysInMonth(_year, _month)
        If clickedIndex >= 1 AndAlso clickedIndex <= daysInMon Then
            _selectedDay = clickedIndex
            Invalidate()
            RaiseEvent DayClicked(Me, clickedIndex)
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

End Class
