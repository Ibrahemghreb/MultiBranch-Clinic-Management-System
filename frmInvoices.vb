Imports ReaLTaiizor.Forms
Imports System.Data

Public Class frmInvoices
    Inherits MaterialForm

    Private ReadOnly _bal As New InvoiceBAL()

    Private panelInputs As Panel
    Private panelButtons As Panel
    Private DataGridView1 As DataGridView

    Private txtInvoiceID As TextBox
    Private txtInvoiceNo As TextBox
    Private dtpInvoiceDate As DateTimePicker
    Private cboPatient As ComboBox
    Private cboBranch As ComboBox
    Private cboAppointment As ComboBox
    Private cboStatus As ComboBox
    Private txtSubTotal As TextBox
    Private txtDiscount As TextBox
    Private txtTaxRate As TextBox
    Private lblTaxAmount As Label
    Private lblNetTotal As Label
    Private txtNotes As TextBox

    Private flowButtons As FlowLayoutPanel
    Private btnsave As Button
    Private btnedit As Button
    Private btndelete As Button
    Private btnnew As Button
    Private btnfind As Button
    Private btnback As Button

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ClientSize = New Size(1030, 650)
        Me.Name = "frmInvoices"
        Me.ResumeLayout(False)
    End Sub

    Private Sub frmInvoices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        BuildUi()
        ApplyUiTheme()
        WireEvents()
        LoadLookups()
        LoadData()
        ClearForm()
    End Sub

    Private Sub WireEvents()
        AddHandler btnsave.Click, AddressOf btnsave_Click
        AddHandler btnedit.Click, AddressOf btnedit_Click
        AddHandler btndelete.Click, AddressOf btndelete_Click
        AddHandler btnnew.Click, AddressOf btnnew_Click
        AddHandler btnfind.Click, AddressOf btnfind_Click
        AddHandler btnback.Click, AddressOf btnback_Click
        AddHandler DataGridView1.CellClick, AddressOf DataGridView1_CellClick
        AddHandler cboPatient.SelectedIndexChanged, AddressOf cboPatient_SelectedIndexChanged
        AddHandler txtSubTotal.TextChanged, AddressOf Recalculate
        AddHandler txtDiscount.TextChanged, AddressOf Recalculate
        AddHandler txtTaxRate.TextChanged, AddressOf Recalculate
        AddHandler txtSubTotal.KeyPress, AddressOf txtAmount_KeyPress
        AddHandler txtDiscount.KeyPress, AddressOf txtAmount_KeyPress
        AddHandler txtTaxRate.KeyPress, AddressOf txtAmount_KeyPress
        AddHandler txtInvoiceID.KeyDown, AddressOf txtInvoiceID_KeyDown
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "إدارة الفواتير")
        UiTheme.StyleInputPanel(panelInputs)
        UiTheme.StyleButtonPanel(panelButtons)
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnedit, btnsave, btnnew)

        For Each ctrl As Control In panelInputs.Controls
            If TypeOf ctrl Is Label Then
                UiTheme.StyleFieldLabel(DirectCast(ctrl, Label))
            ElseIf TypeOf ctrl Is TextBox Then
                UiTheme.StyleTextInput(DirectCast(ctrl, TextBox))
            End If
        Next
        UiTheme.AlignInputLabels(panelInputs)
    End Sub

    Private Sub LoadLookups()
        cboPatient.DataSource = _bal.GetPatients()
        cboPatient.DisplayMember = "DisplayText"
        cboPatient.ValueMember = "ID"
        cboPatient.SelectedIndex = If(cboPatient.Items.Count > 0, 0, -1)

        cboBranch.DataSource = _bal.GetBranches()
        cboBranch.DisplayMember = "DisplayText"
        cboBranch.ValueMember = "ID"
        cboBranch.SelectedIndex = If(cboBranch.Items.Count > 0, 0, -1)

        cboStatus.Items.AddRange(New Object() {"Open", "Paid", "PartiallyPaid", "Cancelled"})
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = _bal.GetAllInvoices()
        Catch ex As Exception
            ShowError("خطأ في تحميل الفواتير", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            _bal.AddInvoice(ReadInvoiceFromForm(False))
            MessageBox.Show("تم حفظ الفاتورة.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ في حفظ الفاتورة", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs)
        Try
            _bal.UpdateInvoice(ReadInvoiceFromForm(True))
            MessageBox.Show("تم تحديث الفاتورة.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ في تحديث الفاتورة", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs)
        Try
            Dim invId = ReadRequiredInteger(txtInvoiceID.Text, "رقم الفاتورة")
            If MessageBox.Show("هل أنت متأكد من إلغاء الفاتورة؟", "تأكيد الإلغاء",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If
            _bal.CancelInvoice(invId)
            MessageBox.Show("تم إلغاء الفاتورة.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ في إلغاء الفاتورة", ex)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs)
        FindData()
    End Sub

    Private Sub FindData()
        Try
            Dim invId = ReadRequiredInteger(txtInvoiceID.Text, "رقم الفاتورة")
            Dim dt = _bal.GetAllInvoices()
            Dim found As DataRow = Nothing
            For Each row As DataRow In dt.Rows
                If CInt(row("InvoiceID")) = invId Then
                    found = row
                    Exit For
                End If
            Next
            If found Is Nothing Then
                MessageBox.Show("لم يتم العثور على الفاتورة.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If
            FillForm(found)
        Catch ex As Exception
            ShowError("خطأ في البحث عن الفاتورة", ex)
        End Try
    End Sub

    Private Function ReadInvoiceFromForm(requireId As Boolean) As Invoice
        Dim inv As New Invoice()
        If requireId Then
            inv.InvoiceID = ReadRequiredInteger(txtInvoiceID.Text, "رقم الفاتورة")
        ElseIf Not String.IsNullOrWhiteSpace(txtInvoiceID.Text) Then
            Integer.TryParse(txtInvoiceID.Text, inv.InvoiceID)
        End If
        inv.InvoiceNo = txtInvoiceNo.Text.Trim()
        If cboPatient.SelectedValue IsNot Nothing AndAlso TypeOf cboPatient.SelectedValue Is Integer Then
            inv.PatientID = CInt(cboPatient.SelectedValue)
        End If
        If cboBranch.SelectedValue IsNot Nothing AndAlso TypeOf cboBranch.SelectedValue Is Integer Then
            inv.BranchID = CInt(cboBranch.SelectedValue)
        End If
        inv.InvoiceDate = dtpInvoiceDate.Value
        Decimal.TryParse(txtSubTotal.Text, inv.SubTotal)
        Decimal.TryParse(txtDiscount.Text, inv.DiscountAmount)
        Decimal.TryParse(txtTaxRate.Text, inv.TaxRate)
        inv.Status = cboStatus.SelectedItem.ToString()
        inv.Notes = txtNotes.Text.Trim()
        If cboAppointment.SelectedValue IsNot Nothing AndAlso TypeOf cboAppointment.SelectedValue Is Integer AndAlso CInt(cboAppointment.SelectedValue) > 0 Then
            inv.AppointmentID = CInt(cboAppointment.SelectedValue)
        End If
        Return inv
    End Function

    Private Sub FillForm(row As DataRow)
        txtInvoiceID.Text = row("InvoiceID").ToString()
        txtInvoiceNo.Text = row("InvoiceNo").ToString()
        dtpInvoiceDate.Value = Convert.ToDateTime(row("InvoiceDate"))
        txtSubTotal.Text = Convert.ToDecimal(row("SubTotal")).ToString("F2")
        txtDiscount.Text = Convert.ToDecimal(row("DiscountAmount")).ToString("F2")
        txtTaxRate.Text = Convert.ToDecimal(row("TaxRate")).ToString("F2")
        lblTaxAmount.Text = Convert.ToDecimal(row("TaxAmount")).ToString("F2")
        lblNetTotal.Text = Convert.ToDecimal(row("NetAmount")).ToString("F2") & " ج.م"
        txtNotes.Text = If(row.Table.Columns.Contains("Notes"), row("Notes").ToString(), "")
        If row.Table.Columns.Contains("Status") Then
            Dim st = row("Status").ToString()
            For i As Integer = 0 To cboStatus.Items.Count - 1
                If cboStatus.Items(i).ToString() = st Then
                    cboStatus.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        If row.Table.Columns.Contains("PatientID") Then
            SelectComboByValue(cboPatient, CInt(row("PatientID")))
        End If
        If row.Table.Columns.Contains("BranchID") Then
            SelectComboByValue(cboBranch, CInt(row("BranchID")))
        End If
        LoadAppointments()
        If row.Table.Columns.Contains("AppointmentID") AndAlso Not IsDBNull(row("AppointmentID")) Then
            SelectComboByValue(cboAppointment, CInt(row("AppointmentID")))
        End If
    End Sub

    Private Sub ClearForm()
        txtInvoiceID.Clear()
        txtInvoiceNo.Text = "INV-" & DateTime.Now.ToString("yyyyMMddHHmmss")
        dtpInvoiceDate.Value = DateTime.Now
        txtSubTotal.Text = "0"
        txtDiscount.Text = "0"
        txtTaxRate.Text = "14"
        lblTaxAmount.Text = "0.00"
        lblNetTotal.Text = "0.00 ج.م"
        txtNotes.Clear()
        If cboPatient.Items.Count > 0 Then cboPatient.SelectedIndex = 0
        If cboBranch.Items.Count > 0 Then cboBranch.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        cboAppointment.Items.Clear()
        txtSubTotal.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            Dim idVal = DataGridView1.Rows(e.RowIndex).Cells("InvoiceID").Value
            If idVal Is Nothing Then Return
            Dim dt = _bal.GetAllInvoices()
            Dim invId = CInt(idVal)
            For Each row As DataRow In dt.Rows
                If CInt(row("InvoiceID")) = invId Then
                    FillForm(row)
                    Exit For
                End If
            Next
        Catch ex As Exception
            ShowError("خطأ في تحميل الفاتورة", ex)
        End Try
    End Sub

    Private Sub cboPatient_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadAppointments()
    End Sub

    Private Sub LoadAppointments()
        cboAppointment.Items.Clear()
        cboAppointment.Items.Add(New PatientListItem(0, "(بدون موعد)"))
        If cboPatient.SelectedValue IsNot Nothing AndAlso TypeOf cboPatient.SelectedValue Is Integer Then
            Dim patId = CInt(cboPatient.SelectedValue)
            If patId > 0 Then
                For Each item As PatientListItem In _bal.GetAppointmentsByPatient(patId)
                    cboAppointment.Items.Add(item)
                Next
            End If
        End If
        cboAppointment.SelectedIndex = 0
    End Sub

    Private Sub Recalculate(sender As Object, e As EventArgs)
        Dim subTotal As Decimal = 0
        Dim discount As Decimal = 0
        Dim taxRate As Decimal = 14
        Decimal.TryParse(txtSubTotal.Text, subTotal)
        Decimal.TryParse(txtDiscount.Text, discount)
        Decimal.TryParse(txtTaxRate.Text, taxRate)
        If taxRate <= 0 Then taxRate = 14
        Dim taxable = Math.Max(0D, subTotal - discount)
        Dim taxAmt = Math.Round(taxable * taxRate / 100D, 2)
        Dim net = Math.Round(taxable + taxAmt, 2)
        lblTaxAmount.Text = taxAmt.ToString("F2")
        lblNetTotal.Text = net.ToString("F2") & " ج.م"
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInvoiceID_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmInvoices_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Shared Sub SelectComboByValue(cbo As ComboBox, value As Integer)
        For i As Integer = 0 To cbo.Items.Count - 1
            Dim item = cbo.Items(i)
            Dim itemVal As Integer = 0
            If TypeOf item Is PatientListItem Then
                itemVal = DirectCast(item, PatientListItem).ID
            ElseIf TypeOf item Is DataRowView Then
                itemVal = Convert.ToInt32(DirectCast(item, DataRowView).Row(0))
            End If
            If itemVal = value Then
                cbo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Shared Function ReadRequiredInteger(value As String, fieldName As String) As Integer
        Dim result As Integer
        If Not Integer.TryParse(value, result) OrElse result <= 0 Then
            Throw New ArgumentException(fieldName & " يجب أن يكون رقماً صحيحاً.")
        End If
        Return result
    End Function

    Private Shared Sub ShowError(title As String, ex As Exception)
        MessageBox.Show(title & ": " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Shared Function NewLabel(text As String, x As Integer, y As Integer, Optional width As Integer = 0, Optional height As Integer = 0) As Label
        Dim lbl As New Label()
        lbl.Text = text
        lbl.Location = New Point(x, y + 4)
        lbl.AutoSize = True
        If width > 0 Then
            lbl.AutoSize = False
            lbl.Width = width
            lbl.Height = If(height > 0, height, 24)
        End If
        Return lbl
    End Function

    Private Sub BuildUi()
        panelInputs = New Panel With {.Dock = DockStyle.Top, .Height = 260, .BackColor = Color.White, .Padding = New Padding(16, 12, 16, 8)}
        panelButtons = New Panel With {.Dock = DockStyle.Bottom, .Height = 88, .BackColor = Color.FromArgb(245, 245, 245), .Padding = New Padding(8, 8, 8, 8)}
        DataGridView1 = New DataGridView With {.Dock = DockStyle.Fill, .BackColor = Color.White}

        ' Left column inputs
        Dim leftX = 16, rightX = 400, labelW = 80, inputW = 200, rowH = 32

        txtInvoiceID = New TextBox With {.Location = New Point(2, 2), .Width = 60, .Visible = False}
        panelInputs.Controls.Add(txtInvoiceID)

        Dim y = 8
        panelInputs.Controls.Add(NewLabel("رقم الفاتورة", leftX, y))
        txtInvoiceNo = New TextBox With {.Location = New Point(leftX + labelW, y), .Width = inputW, .ReadOnly = True, .BackColor = Color.FromArgb(245, 245, 245)}
        panelInputs.Controls.Add(txtInvoiceNo)

        panelInputs.Controls.Add(NewLabel("التاريخ", rightX, y))
        dtpInvoiceDate = New DateTimePicker With {.Location = New Point(rightX + 60, y), .Width = 160, .Format = DateTimePickerFormat.Short}
        panelInputs.Controls.Add(dtpInvoiceDate)

        panelInputs.Controls.Add(NewLabel("الحالة", rightX + 290, y))
        cboStatus = New ComboBox With {.Location = New Point(rightX + 340, y), .Width = 120, .DropDownStyle = ComboBoxStyle.DropDownList}
        panelInputs.Controls.Add(cboStatus)

        y += rowH
        panelInputs.Controls.Add(NewLabel("المريض", leftX, y))
        cboPatient = New ComboBox With {.Location = New Point(leftX + labelW, y), .Width = 260, .DropDownStyle = ComboBoxStyle.DropDown, .AutoCompleteMode = AutoCompleteMode.SuggestAppend, .AutoCompleteSource = AutoCompleteSource.ListItems}
        panelInputs.Controls.Add(cboPatient)

        panelInputs.Controls.Add(NewLabel("الفرع", rightX, y))
        cboBranch = New ComboBox With {.Location = New Point(rightX + 60, y), .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList}
        panelInputs.Controls.Add(cboBranch)

        y += rowH
        panelInputs.Controls.Add(NewLabel("الموعد", leftX, y))
        cboAppointment = New ComboBox With {.Location = New Point(leftX + labelW, y), .Width = 260, .DropDownStyle = ComboBoxStyle.DropDownList}
        panelInputs.Controls.Add(cboAppointment)

        y += rowH + 6
        panelInputs.Controls.Add(NewLabel("المبلغ", leftX, y))
        txtSubTotal = New TextBox With {.Location = New Point(leftX + labelW, y), .Width = 100, .Text = "0"}
        panelInputs.Controls.Add(txtSubTotal)

        panelInputs.Controls.Add(NewLabel("الخصم", rightX, y))
        txtDiscount = New TextBox With {.Location = New Point(rightX + 60, y), .Width = 100, .Text = "0"}
        panelInputs.Controls.Add(txtDiscount)

        panelInputs.Controls.Add(NewLabel("الضريبة %", rightX + 220, y))
        txtTaxRate = New TextBox With {.Location = New Point(rightX + 300, y), .Width = 60, .Text = "14"}
        panelInputs.Controls.Add(txtTaxRate)

        y += rowH
        panelInputs.Controls.Add(NewLabel("قيمة الضريبة", leftX, y))
        lblTaxAmount = New Label With {.Location = New Point(leftX + labelW, y + 4), .Width = 100, .Text = "0.00", .Font = New Font("Segoe UI", 10, FontStyle.Bold)}
        panelInputs.Controls.Add(lblTaxAmount)

        panelInputs.Controls.Add(NewLabel("الإجمالي", rightX, y))
        lblNetTotal = New Label With {.Location = New Point(rightX + 60, y + 4), .Width = 200, .Text = "0.00 ج.م", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .ForeColor = Color.FromArgb(0, 168, 150)}
        panelInputs.Controls.Add(lblNetTotal)

        y += rowH
        panelInputs.Controls.Add(NewLabel("ملاحظات", leftX, y))
        txtNotes = New TextBox With {.Location = New Point(leftX + labelW, y), .Width = 500, .Height = 50, .Multiline = True, .AcceptsReturn = True, .ScrollBars = ScrollBars.Vertical}
        panelInputs.Controls.Add(txtNotes)

        ' DataGridView
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Size = New Size(100, 100)

        ' FlowLayoutPanel for buttons (matches frmDoctors, frmUsers, etc.)
        flowButtons = New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False
        }
        flowButtons.Padding = New Padding(0)

        ' Buttons
        btnback = New Button With {.Name = "btnback", .Text = "رجوع", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}
        btndelete = New Button With {.Name = "btndelete", .Text = "حذف", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}
        btnfind = New Button With {.Name = "btnfind", .Text = "بحث", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}
        btnedit = New Button With {.Name = "btnedit", .Text = "تعديل", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}
        btnsave = New Button With {.Name = "btnsave", .Text = "حفظ", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}
        btnnew = New Button With {.Name = "btnnew", .Text = "جديد", .Size = New Size(128, 74), .Margin = New Padding(4, 8, 4, 4)}

        flowButtons.Controls.AddRange(New Control() {btnback, btndelete, btnfind, btnedit, btnsave, btnnew})
        panelButtons.Controls.Add(flowButtons)

        Me.Controls.Add(DataGridView1)
        Me.Controls.Add(panelInputs)
        Me.Controls.Add(panelButtons)
    End Sub
End Class
