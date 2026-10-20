Imports ReaLTaiizor.Forms

Public Class frmPayments
    Inherits MaterialForm

    Private ReadOnly _bal As New PaymentBAL()
    Private dgv As DataGridView
    Private cboInvoice As ComboBox
    Private txtAmount As TextBox
    Private cboMethod As ComboBox
    Private txtReference As TextBox
    Private lblBalance As Label

    Public Sub New()
        dgv = New DataGridView With {.Location = New Point(12, 185), .Size = New Size(900, 360), .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right}
        cboInvoice = New ComboBox With {.Location = New Point(120, 82), .Width = 360, .DropDownStyle = ComboBoxStyle.DropDownList}
        txtAmount = New TextBox With {.Location = New Point(585, 82), .Width = 120}
        cboMethod = New ComboBox With {.Location = New Point(120, 122), .Width = 160, .DropDownStyle = ComboBoxStyle.DropDownList}
        cboMethod.Items.AddRange(New Object() {"Cash", "Card", "InstaPay", "VodafoneCash", "BankTransfer", "Insurance", "Other"})
        cboMethod.SelectedIndex = 0
        txtReference = New TextBox With {.Location = New Point(390, 122), .Width = 315}
        lblBalance = New Label With {.Text = "المتبقي: 0.00", .Location = New Point(720, 86), .AutoSize = True, .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)}

        Dim btnSave As New Button With {.Text = "حفظ", .Location = New Point(720, 118), .Size = New Size(90, 32)}
        Dim btnRefresh As New Button With {.Text = "تحديث", .Location = New Point(820, 118), .Size = New Size(90, 32)}

        Controls.AddRange(New Control() {
            New Label With {.Text = "الفاتورة", .Location = New Point(12, 86), .AutoSize = True}, cboInvoice,
            New Label With {.Text = "المبلغ", .Location = New Point(520, 86), .AutoSize = True}, txtAmount, lblBalance,
            New Label With {.Text = "طريقة الدفع", .Location = New Point(12, 126), .AutoSize = True}, cboMethod,
            New Label With {.Text = "مرجع الدفع", .Location = New Point(305, 126), .AutoSize = True}, txtReference,
            btnSave, btnRefresh, dgv
        })

        ClientSize = New Size(924, 560)
        Text = "المدفوعات"
        StartPosition = FormStartPosition.CenterScreen

        AddHandler Me.Load, Sub(s, e)
                                UiTheme.ApplyMaterialForm(Me, "المدفوعات")
                                UiTheme.StyleDataGridView(dgv)
                                LoadInvoices()
                                LoadPayments()
                            End Sub
        AddHandler cboInvoice.SelectedIndexChanged, Sub(s, e) UpdateSelectedInvoiceBalance()
        AddHandler btnSave.Click, Sub(s, e) SavePayment()
        AddHandler btnRefresh.Click, Sub(s, e)
                                         LoadInvoices()
                                         LoadPayments()
                                     End Sub
    End Sub

    Private Sub LoadInvoices()
        cboInvoice.Items.Clear()
        For Each row As DataRow In _bal.GetPayableInvoices().Rows
            cboInvoice.Items.Add(New InvoiceItem(CInt(row("InvoiceID")), row("InvoiceNo").ToString(), row("PatientName").ToString(), CDec(row("Balance"))))
        Next
        If cboInvoice.Items.Count > 0 Then cboInvoice.SelectedIndex = 0
        UpdateSelectedInvoiceBalance()
    End Sub

    Private Sub LoadPayments()
        dgv.DataSource = _bal.GetAllPayments()
    End Sub

    Private Sub UpdateSelectedInvoiceBalance()
        Dim invoice = TryCast(cboInvoice.SelectedItem, InvoiceItem)
        lblBalance.Text = "المتبقي: " & If(invoice Is Nothing, "0.00", invoice.Balance.ToString("0.00"))
        If invoice IsNot Nothing Then txtAmount.Text = invoice.Balance.ToString("0.00")
    End Sub

    Private Sub SavePayment()
        Try
            Dim invoice = TryCast(cboInvoice.SelectedItem, InvoiceItem)
            If invoice Is Nothing Then Throw New InvalidOperationException("اختر الفاتورة.")

            Dim amount As Decimal
            If Not Decimal.TryParse(txtAmount.Text, amount) Then Throw New ArgumentException("اكتب مبلغًا صحيحًا.")
            If amount > invoice.Balance Then Throw New ArgumentException("المبلغ أكبر من المتبقي على الفاتورة.")

            _bal.AddPayment(invoice.Id, amount, cboMethod.SelectedItem.ToString(), txtReference.Text)
            MessageBox.Show("تم الحفظ.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtReference.Clear()
            LoadInvoices()
            LoadPayments()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Class InvoiceItem
        Public ReadOnly Id As Integer
        Public ReadOnly InvoiceNo As String
        Public ReadOnly PatientName As String
        Public ReadOnly Balance As Decimal

        Public Sub New(id As Integer, invoiceNo As String, patientName As String, balance As Decimal)
            Me.Id = id
            Me.InvoiceNo = invoiceNo
            Me.PatientName = patientName
            Me.Balance = balance
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("{0} - {1} - متبقي {2:0.00}", InvoiceNo, PatientName, Balance)
        End Function
    End Class

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmPayments
        '
        Me.ClientSize = New System.Drawing.Size(1017, 698)
        Me.Name = "frmPayments"
        Me.ResumeLayout(False)

    End Sub
End Class
