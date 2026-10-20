Imports ReaLTaiizor.Forms

Public Class frmExpenses
    Inherits MaterialForm

    Private ReadOnly _bal As New ExpenseBAL()
    Private dgv As DataGridView
    Private cboBranch As ComboBox
    Private cboCategory As ComboBox
    Private txtAmount As TextBox
    Private dtpDate As DateTimePicker
    Private txtDescription As TextBox

    Public Sub New()
        dgv = New DataGridView With {.Location = New Point(12, 185), .Size = New Size(900, 360), .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right}
        cboBranch = New ComboBox With {.Location = New Point(120, 82), .Width = 220, .DropDownStyle = ComboBoxStyle.DropDownList}
        cboCategory = New ComboBox With {.Location = New Point(455, 82), .Width = 220, .DropDownStyle = ComboBoxStyle.DropDownList}
        txtAmount = New TextBox With {.Location = New Point(775, 82), .Width = 120}
        dtpDate = New DateTimePicker With {.Location = New Point(120, 122), .Width = 220, .Format = DateTimePickerFormat.Short}
        txtDescription = New TextBox With {.Location = New Point(455, 122), .Width = 220}

        Dim btnSave As New Button With {.Text = "حفظ", .Location = New Point(705, 118), .Size = New Size(90, 32)}
        Dim btnRefresh As New Button With {.Text = "تحديث", .Location = New Point(805, 118), .Size = New Size(90, 32)}

        Controls.AddRange(New Control() {
            New Label With {.Text = "الفرع", .Location = New Point(12, 86), .AutoSize = True}, cboBranch,
            New Label With {.Text = "التصنيف", .Location = New Point(370, 86), .AutoSize = True}, cboCategory,
            New Label With {.Text = "المبلغ", .Location = New Point(710, 86), .AutoSize = True}, txtAmount,
            New Label With {.Text = "التاريخ", .Location = New Point(12, 126), .AutoSize = True}, dtpDate,
            New Label With {.Text = "الوصف", .Location = New Point(370, 126), .AutoSize = True}, txtDescription,
            btnSave, btnRefresh, dgv
        })

        ClientSize = New Size(924, 560)
        Text = "المصروفات"
        StartPosition = FormStartPosition.CenterScreen

        AddHandler Me.Load, Sub(s, e)
                                UiTheme.ApplyMaterialForm(Me, "المصروفات")
                                UiTheme.StyleDataGridView(dgv)
                                LoadLookups()
                                LoadExpenses()
                            End Sub
        AddHandler btnSave.Click, Sub(s, e) SaveExpense()
        AddHandler btnRefresh.Click, Sub(s, e)
                                         LoadLookups()
                                         LoadExpenses()
                                     End Sub
    End Sub

    Private Sub LoadLookups()
        cboBranch.Items.Clear()
        For Each row As DataRow In _bal.GetBranches().Rows
            cboBranch.Items.Add(New ComboItem(CInt(row("BranchID")), row("BranchName").ToString()))
        Next
        If cboBranch.Items.Count > 0 Then cboBranch.SelectedIndex = 0

        cboCategory.Items.Clear()
        For Each row As DataRow In _bal.GetCategories().Rows
            cboCategory.Items.Add(New ComboItem(CInt(row("CategoryID")), row("CategoryName").ToString()))
        Next
        If cboCategory.Items.Count > 0 Then cboCategory.SelectedIndex = 0
    End Sub

    Private Sub LoadExpenses()
        dgv.DataSource = _bal.GetAllExpenses()
    End Sub

    Private Sub SaveExpense()
        Try
            Dim branch = TryCast(cboBranch.SelectedItem, ComboItem)
            Dim category = TryCast(cboCategory.SelectedItem, ComboItem)
            If branch Is Nothing Then Throw New InvalidOperationException("اختر الفرع.")
            If category Is Nothing Then Throw New InvalidOperationException("اختر تصنيف المصروف.")

            Dim amount As Decimal
            If Not Decimal.TryParse(txtAmount.Text, amount) Then Throw New ArgumentException("اكتب مبلغًا صحيحًا.")

            _bal.AddExpense(branch.Id, category.Id, amount, dtpDate.Value.Date, txtDescription.Text)
            MessageBox.Show("تم الحفظ.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtAmount.Clear()
            txtDescription.Clear()
            LoadExpenses()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Class ComboItem
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

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmExpenses
        '
        Me.ClientSize = New System.Drawing.Size(1080, 699)
        Me.Name = "frmExpenses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
End Class
