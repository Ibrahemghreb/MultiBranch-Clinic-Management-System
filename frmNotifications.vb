Imports ReaLTaiizor.Forms

Public Class frmNotifications
    Inherits MaterialForm

    Private ReadOnly _notificationBal As New NotificationBAL()
    Private dgvNotifications As DataGridView
    Private cboFilter As ComboBox
    Private cboSeverity As ComboBox
    Private btnMarkRead As Button
    Private btnMarkAll As Button
    Private btnDelete As Button
    Private btnRefresh As Button
    Private btnClose As Button

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        cboFilter = New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Point(12, 88), .Width = 140}
        cboFilter.Items.AddRange(New Object() {"الكل", "غير مقروء"})
        cboFilter.SelectedIndex = 0

        cboSeverity = New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Point(170, 88), .Width = 120}
        cboSeverity.Items.AddRange(New Object() {"الكل", "Info", "Warning", "Error"})
        cboSeverity.SelectedIndex = 0

        btnRefresh = New Button With {.Text = "تحديث", .Location = New Point(310, 86), .Size = New Size(80, 30)}
        btnMarkRead = New Button With {.Text = "تحديد كمقروء", .Location = New Point(400, 86), .Size = New Size(110, 30)}
        btnMarkAll = New Button With {.Text = "تحديد الكل", .Location = New Point(520, 86), .Size = New Size(100, 30)}
        btnDelete = New Button With {.Text = "حذف", .Location = New Point(630, 86), .Size = New Size(80, 30)}
        btnClose = New Button With {.Text = "إغلاق", .Location = New Point(720, 86), .Size = New Size(80, 30)}

        dgvNotifications = New DataGridView With {
            .Location = New Point(12, 130),
            .Size = New Size(860, 420),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right,
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }

        Me.ClientSize = New Size(884, 560)
        Me.Controls.AddRange(New Control() {cboFilter, cboSeverity, btnRefresh, btnMarkRead, btnMarkAll, btnDelete, btnClose, dgvNotifications})
        Me.Text = "الإشعارات"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ResumeLayout(False)

        AddHandler btnRefresh.Click, AddressOf LoadNotifications
        AddHandler btnMarkRead.Click, AddressOf MarkSelectedRead
        AddHandler btnMarkAll.Click, AddressOf MarkAllRead
        AddHandler btnDelete.Click, AddressOf DeleteSelected
        AddHandler btnClose.Click, Sub(s, e) Me.Close()
        AddHandler cboFilter.SelectedIndexChanged, AddressOf LoadNotifications
        AddHandler cboSeverity.SelectedIndexChanged, AddressOf LoadNotifications
        AddHandler Me.Load, AddressOf frmNotifications_Load
        AddHandler Me.KeyDown, AddressOf frmNotifications_KeyDown
    End Sub

    Private Sub frmNotifications_Load(sender As Object, e As EventArgs)
        Me.KeyPreview = True
        UiTheme.ApplyMaterialForm(Me, "الإشعارات")
        UiTheme.StyleDataGridView(dgvNotifications)
        LoadNotifications(Nothing, EventArgs.Empty)
    End Sub

    Private Sub LoadNotifications(sender As Object, e As EventArgs)
        Try
            If SessionManager.UserID <= 0 Then Return
            Dim unreadOnly = (cboFilter.SelectedIndex = 1)
            Dim severity = If(cboSeverity.SelectedIndex <= 0, "", cboSeverity.SelectedItem.ToString())
            dgvNotifications.DataSource = _notificationBal.GetNotifications(SessionManager.UserID, unreadOnly, severity)
            If dgvNotifications.Columns.Contains("NotificationID") Then dgvNotifications.Columns("NotificationID").Visible = False
            If dgvNotifications.Columns.Contains("UserID") Then dgvNotifications.Columns("UserID").Visible = False
        Catch ex As Exception
            Logger.LogError($"frmNotifications.LoadNotifications: {ex.Message}")
            MessageBox.Show("تعذر تحميل الإشعارات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MarkSelectedRead(sender As Object, e As EventArgs)
        If dgvNotifications.CurrentRow Is Nothing Then Return
        Dim id = Convert.ToInt32(dgvNotifications.CurrentRow.Cells("NotificationID").Value)
        _notificationBal.MarkRead(id)
        LoadNotifications(Nothing, EventArgs.Empty)
    End Sub

    Private Sub MarkAllRead(sender As Object, e As EventArgs)
        _notificationBal.MarkAllRead(SessionManager.UserID)
        LoadNotifications(Nothing, EventArgs.Empty)
    End Sub

    Private Sub DeleteSelected(sender As Object, e As EventArgs)
        If dgvNotifications.CurrentRow Is Nothing Then Return
        If MessageBox.Show("حذف الإشعار المحدد؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Dim id = Convert.ToInt32(dgvNotifications.CurrentRow.Cells("NotificationID").Value)
        _notificationBal.Delete(id)
        LoadNotifications(Nothing, EventArgs.Empty)
    End Sub

    Private Sub frmNotifications_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class
