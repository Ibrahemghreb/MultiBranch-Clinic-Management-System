Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBranches
    Inherits ReaLTaiizor.Forms.MaterialForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.panelInputs = New System.Windows.Forms.Panel()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.dtpCloseTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpOpenTime = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtphone = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBranchID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.flowButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnback = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.panelInputs.SuspendLayout()
        Me.panelButtons.SuspendLayout()
        Me.flowButtons.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelInputs
        '
        Me.panelInputs.Controls.Add(Me.chkIsActive)
        Me.panelInputs.Controls.Add(Me.dtpCloseTime)
        Me.panelInputs.Controls.Add(Me.dtpOpenTime)
        Me.panelInputs.Controls.Add(Me.Label7)
        Me.panelInputs.Controls.Add(Me.Label6)
        Me.panelInputs.Controls.Add(Me.txtEmail)
        Me.panelInputs.Controls.Add(Me.Label3)
        Me.panelInputs.Controls.Add(Me.txtphone)
        Me.panelInputs.Controls.Add(Me.Label5)
        Me.panelInputs.Controls.Add(Me.txtaddress)
        Me.panelInputs.Controls.Add(Me.Label4)
        Me.panelInputs.Controls.Add(Me.txtBranchName)
        Me.panelInputs.Controls.Add(Me.Label2)
        Me.panelInputs.Controls.Add(Me.txtBranchID)
        Me.panelInputs.Controls.Add(Me.Label1)
        Me.panelInputs.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelInputs.Location = New System.Drawing.Point(3, 78)
        Me.panelInputs.Name = "panelInputs"
        Me.panelInputs.Size = New System.Drawing.Size(1018, 168)
        Me.panelInputs.TabIndex = 0
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(880, 124)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(78, 27)
        Me.chkIsActive.TabIndex = 8
        Me.chkIsActive.Text = "Active"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'dtpCloseTime
        '
        Me.dtpCloseTime.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dtpCloseTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpCloseTime.Location = New System.Drawing.Point(720, 120)
        Me.dtpCloseTime.Name = "dtpCloseTime"
        Me.dtpCloseTime.ShowUpDown = True
        Me.dtpCloseTime.Size = New System.Drawing.Size(130, 30)
        Me.dtpCloseTime.TabIndex = 7
        '
        'dtpOpenTime
        '
        Me.dtpOpenTime.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dtpOpenTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpOpenTime.Location = New System.Drawing.Point(520, 120)
        Me.dtpOpenTime.Name = "dtpOpenTime"
        Me.dtpOpenTime.ShowUpDown = True
        Me.dtpOpenTime.Size = New System.Drawing.Size(130, 30)
        Me.dtpOpenTime.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(660, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 23)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Close"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(460, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 23)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Open"
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtEmail.Location = New System.Drawing.Point(520, 72)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(330, 30)
        Me.txtEmail.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(460, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 23)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Email"
        '
        'txtphone
        '
        Me.txtphone.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtphone.Location = New System.Drawing.Point(520, 24)
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(330, 30)
        Me.txtphone.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(460, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 19)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Phone"
        '
        'txtaddress
        '
        Me.txtaddress.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtaddress.Location = New System.Drawing.Point(130, 120)
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.Size = New System.Drawing.Size(320, 30)
        Me.txtaddress.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(16, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Address"
        '
        'txtBranchName
        '
        Me.txtBranchName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtBranchName.Location = New System.Drawing.Point(130, 72)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(320, 30)
        Me.txtBranchName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(16, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 23)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Branch Name"
        '
        'txtBranchID
        '
        Me.txtBranchID.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtBranchID.Location = New System.Drawing.Point(130, 24)
        Me.txtBranchID.Name = "txtBranchID"
        Me.txtBranchID.Size = New System.Drawing.Size(110, 30)
        Me.txtBranchID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(16, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Branch ID"
        '
        'panelButtons
        '
        Me.panelButtons.Controls.Add(Me.flowButtons)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelButtons.Location = New System.Drawing.Point(3, 629)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(1018, 88)
        Me.panelButtons.TabIndex = 2
        '
        'flowButtons
        '
        Me.flowButtons.Controls.Add(Me.btnback)
        Me.flowButtons.Controls.Add(Me.btndelete)
        Me.flowButtons.Controls.Add(Me.btnfind)
        Me.flowButtons.Controls.Add(Me.btnedit)
        Me.flowButtons.Controls.Add(Me.btnsave)
        Me.flowButtons.Controls.Add(Me.btnnew)
        Me.flowButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowButtons.Location = New System.Drawing.Point(0, 0)
        Me.flowButtons.Name = "flowButtons"
        Me.flowButtons.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.flowButtons.Size = New System.Drawing.Size(1018, 88)
        Me.flowButtons.TabIndex = 0
        '
        'btnback
        '
        Me.btnback.Location = New System.Drawing.Point(8, 8)
        Me.btnback.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(128, 74)
        Me.btnback.TabIndex = 0
        Me.btnback.Text = "Back"
        Me.btnback.UseVisualStyleBackColor = True
        '
        'btndelete
        '
        Me.btndelete.Location = New System.Drawing.Point(144, 8)
        Me.btndelete.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 74)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnfind
        '
        Me.btnfind.Location = New System.Drawing.Point(280, 8)
        Me.btnfind.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(128, 74)
        Me.btnfind.TabIndex = 2
        Me.btnfind.Text = "Find"
        Me.btnfind.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Location = New System.Drawing.Point(416, 8)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(128, 74)
        Me.btnedit.TabIndex = 3
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.Location = New System.Drawing.Point(552, 8)
        Me.btnsave.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(128, 74)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'btnnew
        '
        Me.btnnew.Location = New System.Drawing.Point(688, 8)
        Me.btnnew.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(128, 74)
        Me.btnnew.TabIndex = 5
        Me.btnnew.Text = "New"
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 246)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1018, 383)
        Me.DataGridView1.TabIndex = 1
        '
        'frmBranches
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 720)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelInputs)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "frmBranches"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Branch Management"
        Me.panelInputs.ResumeLayout(False)
        Me.panelInputs.PerformLayout()
        Me.panelButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelInputs As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBranchID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBranchName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtaddress As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtphone As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpOpenTime As DateTimePicker
    Friend WithEvents dtpCloseTime As DateTimePicker
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents panelButtons As Panel
    Friend WithEvents flowButtons As FlowLayoutPanel
    Friend WithEvents btnback As Button
    Friend WithEvents btndelete As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents btnnew As Button
End Class
