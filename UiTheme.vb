Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports ReaLTaiizor.Forms

''' <summary>ألوان وخطوط موحّدة مع ReaLTaiizor Material (Slate + Teal palette).</summary>
Public Module UiTheme
    ' Primary palette — Slate Dark + Teal accent (matches AdminDashboardPanel)
    Public ReadOnly PrimaryColor As Color = Color.FromArgb(0, 168, 150)       ' Teal accent
    Public ReadOnly PrimaryDarkColor As Color = Color.FromArgb(10, 18, 38)    ' Slate dark
    Public ReadOnly SidebarActiveColor As Color = Color.FromArgb(0, 168, 150) ' Teal active
    Public ReadOnly SurfaceColor As Color = Color.FromArgb(240, 244, 250)     ' Light bg
    Public ReadOnly MenuForeColor As Color = Color.White
    Public ReadOnly SubTextColor As Color = Color.FromArgb(160, 190, 220)     ' Muted blue-grey

    Public ReadOnly AppFont As New Font("Times New Roman", 10.0F, FontStyle.Bold)
    Public ReadOnly MenuFont As New Font("Times New Roman", 10.0F, FontStyle.Bold)
    Public ReadOnly TitleFont As New Font("Times New Roman", 14.0F, FontStyle.Bold)

    Public Sub ApplyFormTheme(owner As Form, Optional title As String = "")
        ' Generic form theming that works for MaterialForm and regular Forms.
        owner.StartPosition = FormStartPosition.CenterScreen
        owner.Font = AppFont
        owner.BackColor = SurfaceColor
        If Not String.IsNullOrWhiteSpace(title) Then
            owner.Text = title
        End If

        ' If it's a MaterialForm, apply additional padding used by ReaLTaiizor
        Dim mf = TryCast(owner, MaterialForm)
        If mf IsNot Nothing Then
            mf.Padding = New Padding(3, 78, 3, 3)
        End If

        ' Style common top-level strips if present
        For Each ctrl As Control In owner.Controls
            If TypeOf ctrl Is MenuStrip Then
                StyleMenuStrip(DirectCast(ctrl, MenuStrip))
            ElseIf TypeOf ctrl Is StatusStrip Then
                StyleStatusStrip(DirectCast(ctrl, StatusStrip))
            End If
        Next

        ' Apply consistent control styling across the form (labels, inputs, buttons)
        StyleFormControls(owner)
    End Sub

    Public Sub ApplyMaterialForm(form As Form, title As String)
        ApplyFormTheme(form, title)
    End Sub

    Public Sub StyleMenuStrip(menu As MenuStrip)
        menu.BackColor = PrimaryDarkColor     ' Slate dark — matches new sidebar
        menu.ForeColor = MenuForeColor
        menu.Font = MenuFont
        menu.RenderMode = ToolStripRenderMode.System

        For Each item As ToolStripItem In menu.Items
            StyleMenuItem(item)
        Next
    End Sub

    Public Sub StyleStatusStrip(status As StatusStrip)
        status.BackColor = PrimaryDarkColor   ' Matching dark bar
        status.ForeColor = SubTextColor       ' Soft blue-grey text
        status.Font = New Font("Times New Roman", 8.5F, FontStyle.Bold)
    End Sub

    Public Sub StyleHeaderPanel(panel As Panel, welcomeLabel As Label)
        panel.BackColor = PrimaryDarkColor    ' Slate dark
        panel.Visible = False                 ' Header is now inside AdminDashboardPanel
        panel.Height = 0

        welcomeLabel.ForeColor = MenuForeColor
        welcomeLabel.Font = TitleFont
        welcomeLabel.TextAlign = ContentAlignment.MiddleLeft
    End Sub

    Public Sub StyleInputPanel(panel As Panel)
        panel.BackColor = Color.White
        panel.Dock = DockStyle.Top
        panel.Padding = New Padding(16, 12, 16, 8)
    End Sub

    Public Sub StyleButtonPanel(panel As Panel)
        panel.BackColor = Color.FromArgb(245, 245, 245)
        panel.Dock = DockStyle.Bottom
        panel.Height = 88
        panel.Padding = New Padding(8, 8, 8, 8)
    End Sub

    Public Sub ApplyIconButtons(owner As Form, ParamArray buttons As Button())
        Dim resources As New ComponentResourceManager(owner.GetType())
        For Each button As Button In buttons
            StyleIconButton(button, resources)
        Next
    End Sub

    Public Sub StyleIconButton(button As Button, resources As ComponentResourceManager)
        Dim image = TryCast(resources.GetObject(button.Name & ".BackgroundImage"), Image)
        If image Is Nothing Then
            ' Fallback: try loading from other forms' resources (reuse existing icons)
            Dim fallbackTypes As Type() = {
                GetType(frmappointments),
                GetType(frmDiagnoses),
                GetType(frmpatients),
                GetType(frmusers)
            }

            For Each t As Type In fallbackTypes
                Try
                    Dim altRes As New ComponentResourceManager(t)
                    image = TryCast(altRes.GetObject(button.Name & ".BackgroundImage"), Image)
                    If image IsNot Nothing Then Exit For
                Catch
                End Try
            Next
        End If

        If image Is Nothing Then
            StyleActionButton(button)
            Return
        End If

        button.BackgroundImage = image
        button.BackgroundImageLayout = ImageLayout.Stretch
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.Font = New Font("Times New Roman", 9.0F, FontStyle.Bold)
        button.Size = New Size(128, 74)
        button.Margin = New Padding(4, 4, 4, 4)
        button.TextAlign = ContentAlignment.BottomCenter
        button.UseVisualStyleBackColor = True
    End Sub

    Public Sub StyleActionButton(button As Button, Optional isPrimary As Boolean = False)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.Font = MenuFont
        button.Height = 40
        button.Width = 110
        button.Cursor = Cursors.Hand

        If isPrimary Then
            button.BackColor = PrimaryColor
            button.ForeColor = MenuForeColor
        Else
            button.BackColor = Color.White
            button.ForeColor = Color.FromArgb(33, 33, 33)
            button.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200)
            button.FlatAppearance.BorderSize = 1
        End If
    End Sub

    Public Sub StyleDataGridView(grid As DataGridView)
        grid.BackgroundColor = SurfaceColor
        grid.BorderStyle = BorderStyle.FixedSingle
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryDarkColor
        grid.ColumnHeadersDefaultCellStyle.ForeColor = MenuForeColor
        grid.ColumnHeadersDefaultCellStyle.Font = MenuFont
        grid.DefaultCellStyle.Font = AppFont
        grid.RowTemplate.Height = 32
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.ReadOnly = True
        grid.MultiSelect = False
        grid.RowHeadersVisible = False
    End Sub

    Public Sub StyleFieldLabel(label As Label)
        label.Font = AppFont
        label.ForeColor = Color.FromArgb(66, 66, 66)
        label.AutoSize = True
    End Sub

    Public Sub StyleTextInput(textBox As TextBox)
        textBox.Font = AppFont
        textBox.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Public Sub StyleFormControls(owner As Form)
        Dim isRtl As Boolean = (owner.RightToLeft = RightToLeft.Yes) OrElse System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft
        For Each ctrl As Control In owner.Controls
            StyleControlRecursive(ctrl, isRtl)
        Next
    End Sub

    Private Sub StyleControlRecursive(ctrl As Control, isRtl As Boolean)
        If TypeOf ctrl Is Label Then
            Dim lbl As Label = CType(ctrl, Label)
            lbl.Font = AppFont
            lbl.ForeColor = Color.FromArgb(66, 66, 66)
            If isRtl Then
                lbl.TextAlign = ContentAlignment.MiddleRight
                lbl.Padding = New Padding(0, 4, 6, 4)
            Else
                lbl.TextAlign = ContentAlignment.MiddleLeft
                lbl.Padding = New Padding(0, 4, 6, 4)
            End If
        ElseIf TypeOf ctrl Is TextBox Then
            Dim tb As TextBox = CType(ctrl, TextBox)
            tb.Font = AppFont
            tb.BorderStyle = BorderStyle.FixedSingle
        ElseIf TypeOf ctrl Is Button Then
            Dim btn As Button = CType(ctrl, Button)
            If btn.BackgroundImage Is Nothing AndAlso btn.Image Is Nothing Then
                If btn.Width <= 140 AndAlso btn.Height <= 80 Then
                    StyleActionButton(btn)
                End If
            Else
                Try
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText
                    btn.ImageAlign = If(isRtl, ContentAlignment.MiddleRight, ContentAlignment.MiddleLeft)
                    btn.Padding = New Padding(8, 0, 8, 0)
                Catch ex As Exception
                End Try
            End If
        End If

        For Each child As Control In ctrl.Controls
            StyleControlRecursive(child, isRtl)
        Next
    End Sub

    Private Sub StyleMenuItem(item As ToolStripItem)
        item.ForeColor = MenuForeColor
        item.BackColor = PrimaryColor

        Dim menuItem = TryCast(item, ToolStripMenuItem)
        If menuItem Is Nothing Then
            Return
        End If

        For Each subItem As ToolStripItem In menuItem.DropDownItems
            subItem.ForeColor = Color.Black
            subItem.BackColor = Color.White
        Next
    End Sub

    Public Sub AlignInputLabels(panel As Panel)
        Dim inputs As New List(Of Control)()
        For Each ctrl As Control In panel.Controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                inputs.Add(ctrl)
            End If
        Next

        For Each lbl As Control In panel.Controls
            If Not TypeOf lbl Is Label Then Continue For

            Dim closest As Control = Nothing
            Dim closestDist As Integer = Integer.MaxValue

            For Each inp As Control In inputs
                If inp.Left > lbl.Left AndAlso Math.Abs(inp.Top - lbl.Top) < 40 Then
                    Dim dist = inp.Left - (lbl.Left + lbl.Width)
                    If dist >= 0 AndAlso dist < closestDist Then
                        closestDist = dist
                        closest = inp
                    End If
                End If
            Next

            If closest IsNot Nothing Then
                Dim newTop As Integer = closest.Top + (closest.Height - lbl.Height) \ 2
                If newTop < 0 Then newTop = 0
                lbl.Top = newTop
            End If
        Next
    End Sub

    Public Sub ApplyReportForm(form As Form, title As String)
        ApplyFormTheme(form, title)
        form.BackColor = SurfaceColor
        For Each ctrl As Control In form.Controls
            If TypeOf ctrl Is DataGridView Then
                StyleDataGridView(DirectCast(ctrl, DataGridView))
            ElseIf TypeOf ctrl Is Button Then
                StyleActionButton(DirectCast(ctrl, Button))
            End If
        Next
    End Sub
End Module
