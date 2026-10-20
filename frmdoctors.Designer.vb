Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDoctors
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
        Me.cmbSpecialty = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLicenseNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtuserid = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtphonedoctor = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.txtlastnamedoctor = New System.Windows.Forms.TextBox()
        Me.lblLastName = New System.Windows.Forms.Label()
        Me.txtfirstname = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbBranch = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDoctorid = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.panelInputs.Controls.Add(Me.cmbSpecialty)
        Me.panelInputs.Controls.Add(Me.Label7)
        Me.panelInputs.Controls.Add(Me.txtLicenseNo)
        Me.panelInputs.Controls.Add(Me.Label6)
        Me.panelInputs.Controls.Add(Me.txtuserid)
        Me.panelInputs.Controls.Add(Me.Label3)
        Me.panelInputs.Controls.Add(Me.txtemail)
        Me.panelInputs.Controls.Add(Me.lblEmail)
        Me.panelInputs.Controls.Add(Me.txtphonedoctor)
        Me.panelInputs.Controls.Add(Me.lblPhone)
        Me.panelInputs.Controls.Add(Me.txtlastnamedoctor)
        Me.panelInputs.Controls.Add(Me.lblLastName)
        Me.panelInputs.Controls.Add(Me.txtfirstname)
        Me.panelInputs.Controls.Add(Me.Label4)
        Me.panelInputs.Controls.Add(Me.cmbBranch)
        Me.panelInputs.Controls.Add(Me.Label1)
        Me.panelInputs.Controls.Add(Me.txtDoctorid)
        Me.panelInputs.Controls.Add(Me.Label2)
        Me.panelInputs.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelInputs.Location = New System.Drawing.Point(3, 78)
        Me.panelInputs.Name = "panelInputs"
        Me.panelInputs.Size = New System.Drawing.Size(1018, 196)
        Me.panelInputs.TabIndex = 0
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(924, 156)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(78, 27)
        Me.chkIsActive.TabIndex = 11
        Me.chkIsActive.Text = "Active"
        '
        'cmbSpecialty
        '
        Me.cmbSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialty.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbSpecialty.FormattingEnabled = True
        Me.cmbSpecialty.Location = New System.Drawing.Point(564, 152)
        Me.cmbSpecialty.Name = "cmbSpecialty"
        Me.cmbSpecialty.Size = New System.Drawing.Size(330, 31)
        Me.cmbSpecialty.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(460, 156)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 23)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Specialty"
        '
        'txtLicenseNo
        '
        Me.txtLicenseNo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtLicenseNo.Location = New System.Drawing.Point(120, 152)
        Me.txtLicenseNo.Name = "txtLicenseNo"
        Me.txtLicenseNo.Size = New System.Drawing.Size(330, 30)
        Me.txtLicenseNo.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(16, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 23)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "License No"
        '
        'txtuserid
        '
        Me.txtuserid.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtuserid.Location = New System.Drawing.Point(564, 116)
        Me.txtuserid.Name = "txtuserid"
        Me.txtuserid.Size = New System.Drawing.Size(120, 30)
        Me.txtuserid.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(460, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 23)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "User ID"
        '
        'txtemail
        '
        Me.txtemail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtemail.Location = New System.Drawing.Point(120, 116)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(330, 30)
        Me.txtemail.TabIndex = 7
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblEmail.Location = New System.Drawing.Point(16, 120)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(51, 23)
        Me.lblEmail.TabIndex = 10
        Me.lblEmail.Text = "Email"
        '
        'txtphonedoctor
        '
        Me.txtphonedoctor.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtphonedoctor.Location = New System.Drawing.Point(564, 80)
        Me.txtphonedoctor.Name = "txtphonedoctor"
        Me.txtphonedoctor.Size = New System.Drawing.Size(330, 30)
        Me.txtphonedoctor.TabIndex = 6
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblPhone.Location = New System.Drawing.Point(460, 84)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(59, 23)
        Me.lblPhone.TabIndex = 8
        Me.lblPhone.Text = "Phone"
        '
        'txtlastnamedoctor
        '
        Me.txtlastnamedoctor.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtlastnamedoctor.Location = New System.Drawing.Point(120, 80)
        Me.txtlastnamedoctor.Name = "txtlastnamedoctor"
        Me.txtlastnamedoctor.Size = New System.Drawing.Size(330, 30)
        Me.txtlastnamedoctor.TabIndex = 5
        '
        'lblLastName
        '
        Me.lblLastName.AutoSize = True
        Me.lblLastName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblLastName.Location = New System.Drawing.Point(16, 84)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(91, 23)
        Me.lblLastName.TabIndex = 6
        Me.lblLastName.Text = "Last Name"
        '
        'txtfirstname
        '
        Me.txtfirstname.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtfirstname.Location = New System.Drawing.Point(564, 44)
        Me.txtfirstname.Name = "txtfirstname"
        Me.txtfirstname.Size = New System.Drawing.Size(330, 30)
        Me.txtfirstname.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(460, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 23)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "First Name"
        '
        'cmbBranch
        '
        Me.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBranch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbBranch.FormattingEnabled = True
        Me.cmbBranch.Location = New System.Drawing.Point(120, 44)
        Me.cmbBranch.Name = "cmbBranch"
        Me.cmbBranch.Size = New System.Drawing.Size(330, 31)
        Me.cmbBranch.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(16, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Branch"
        '
        'txtDoctorid
        '
        Me.txtDoctorid.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDoctorid.Location = New System.Drawing.Point(120, 10)
        Me.txtDoctorid.Name = "txtDoctorid"
        Me.txtDoctorid.Size = New System.Drawing.Size(120, 30)
        Me.txtDoctorid.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(16, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Doctor ID"
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
        Me.DataGridView1.Location = New System.Drawing.Point(3, 274)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1018, 355)
        Me.DataGridView1.TabIndex = 1
        '
        'frmDoctors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 720)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelInputs)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "frmDoctors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Doctor Management"
        Me.panelInputs.ResumeLayout(False)
        Me.panelInputs.PerformLayout()
        Me.panelButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelInputs As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDoctorid As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbBranch As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtfirstname As TextBox
    Friend WithEvents lblLastName As Label
    Friend WithEvents txtlastnamedoctor As TextBox
    Friend WithEvents lblPhone As Label
    Friend WithEvents txtphonedoctor As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtemail As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtuserid As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtLicenseNo As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbSpecialty As ComboBox
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
