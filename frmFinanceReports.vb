Imports ReaLTaiizor.Forms

Public Class frmFinanceReports
    Inherits Form

    Private ReadOnly _bal As New FinanceReportBAL()
    Private dtpStart As DateTimePicker
    Private dtpEnd As DateTimePicker
    Private tabs As TabControl
    Private dgvBranches As DataGridView
    Private dgvDoctors As DataGridView
    Private btnViewReport As Button

    Public Sub New()
        dtpStart = New DateTimePicker With {.Location = New Point(110, 82), .Width = 150, .Format = DateTimePickerFormat.Short, .Value = New Date(Date.Today.Year, Date.Today.Month, 1)}
        dtpEnd = New DateTimePicker With {.Location = New Point(360, 82), .Width = 150, .Format = DateTimePickerFormat.Short, .Value = Date.Today}
        Dim btnLoad As New Button With {.Text = "عرض", .Location = New Point(540, 80), .Size = New Size(90, 32)}

        dgvBranches = New DataGridView With {.Dock = DockStyle.Fill}
        dgvDoctors = New DataGridView With {.Dock = DockStyle.Fill}
        tabs = New TabControl With {.Location = New Point(12, 130), .Size = New Size(900, 420), .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right}
        Dim tpBranches As New TabPage("أرباح الفروع")
        tpBranches.Controls.Add(dgvBranches)

        Dim tpDoctors As New TabPage("أداء الأطباء")
        tpDoctors.Controls.Add(dgvDoctors)

        tabs.TabPages.Add(tpBranches)
        tabs.TabPages.Add(tpDoctors)

        btnViewReport = New Button() With {
            .Text = "عرض تقرير",
            .Font = New Font("Times New Roman", 11, FontStyle.Bold),
            .BackColor = Color.FromArgb(0, 168, 150),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Size(120, 36),
            .Location = New Point(780, 80)
        }

        Controls.AddRange(New Control() {
            New Label With {.Text = "من تاريخ", .Location = New Point(12, 86), .AutoSize = True}, dtpStart,
            New Label With {.Text = "إلى تاريخ", .Location = New Point(285, 86), .AutoSize = True}, dtpEnd,
            btnLoad, btnViewReport, tabs
        })

        ClientSize = New Size(924, 565)
        Text = "التقارير المالية"
        StartPosition = FormStartPosition.CenterScreen

        AddHandler Me.Load, Sub(s, e)
                                UiTheme.ApplyFormTheme(Me, "التقارير المالية")
                                UiTheme.StyleDataGridView(dgvBranches)
                                UiTheme.StyleDataGridView(dgvDoctors)
                                LoadReports()
                            End Sub
        AddHandler btnLoad.Click, Sub(s, e) LoadReports()
        AddHandler btnViewReport.Click, AddressOf ShowReport
    End Sub

    Private Sub LoadReports()
        Try
            dgvBranches.DataSource = _bal.GetBranchProfit(dtpStart.Value.Date, dtpEnd.Value.Date)
            dgvDoctors.DataSource = _bal.GetDoctorPerformance(dtpStart.Value.Date, dtpEnd.Value.Date)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowReport(sender As Object, e As EventArgs)
        Try
            Dim dgv As DataGridView = Nothing
            Dim title As String = ""
            If tabs.SelectedIndex = 0 Then
                dgv = dgvBranches
                title = "أرباح الفروع"
            Else
                dgv = dgvDoctors
                title = "أداء الأطباء"
            End If
            Dim dt As DataTable = TryCast(dgv.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                MessageBox.Show("لا توجد بيانات. قم بالضغط على 'عرض' أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ReportHelper.ShowReportViewer(dt, title)
        Catch ex As Exception
            MessageBox.Show("خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
