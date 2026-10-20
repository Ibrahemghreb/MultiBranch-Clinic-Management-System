Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUsers
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
        Me.cmbRole = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtuserid = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.flowButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnback = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
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
        Me.panelInputs.Controls.Add(Me.cmbRole)
        Me.panelInputs.Controls.Add(Me.Label3)
        Me.panelInputs.Controls.Add(Me.txtFullName)
        Me.panelInputs.Controls.Add(Me.Label5)
        Me.panelInputs.Controls.Add(Me.txtpassword)
        Me.panelInputs.Controls.Add(Me.lblPassword)
        Me.panelInputs.Controls.Add(Me.txtusername)
        Me.panelInputs.Controls.Add(Me.Label1)
        Me.panelInputs.Controls.Add(Me.txtuserid)
        Me.panelInputs.Controls.Add(Me.Label4)
        Me.panelInputs.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelInputs.Location = New System.Drawing.Point(3, 78)
        Me.panelInputs.Name = "panelInputs"
        Me.panelInputs.Size = New System.Drawing.Size(1018, 140)
        Me.panelInputs.TabIndex = 0
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(791, 88)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(78, 27)
        Me.chkIsActive.TabIndex = 6
        Me.chkIsActive.Text = "Active"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'cmbRole
        '
        Me.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRole.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbRole.FormattingEnabled = True
        Me.cmbRole.Location = New System.Drawing.Point(551, 84)
        Me.cmbRole.Name = "cmbRole"
        Me.cmbRole.Size = New System.Drawing.Size(220, 31)
        Me.cmbRole.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(460, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 23)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Role"
        '
        'txtFullName
        '
        Me.txtFullName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtFullName.Location = New System.Drawing.Point(120, 97)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(330, 30)
        Me.txtFullName.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(16, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 23)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Full Name"
        '
        'txtpassword
        '
        Me.txtpassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtpassword.Location = New System.Drawing.Point(547, 36)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.Size = New System.Drawing.Size(330, 30)
        Me.txtpassword.TabIndex = 3
        Me.txtpassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblPassword.Location = New System.Drawing.Point(460, 40)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(80, 23)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Password"
        '
        'txtusername
        '
        Me.txtusername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtusername.Location = New System.Drawing.Point(120, 48)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(330, 30)
        Me.txtusername.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(16, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Username"
        '
        'txtuserid
        '
        Me.txtuserid.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtuserid.Location = New System.Drawing.Point(120, 12)
        Me.txtuserid.Name = "txtuserid"
        Me.txtuserid.Size = New System.Drawing.Size(120, 30)
        Me.txtuserid.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(16, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "User ID"
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
        Me.flowButtons.Controls.Add(Me.btnupdate)
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
        'btnupdate
        '
        Me.btnupdate.Location = New System.Drawing.Point(416, 8)
        Me.btnupdate.Margin = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(128, 74)
        Me.btnupdate.TabIndex = 3
        Me.btnupdate.Text = "Edit"
        Me.btnupdate.UseVisualStyleBackColor = True
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
        Me.DataGridView1.Location = New System.Drawing.Point(3, 218)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1018, 411)
        Me.DataGridView1.TabIndex = 1
        '
        'frmUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 720)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelInputs)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "frmUsers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Management"
        Me.panelInputs.ResumeLayout(False)
        Me.panelInputs.PerformLayout()
        Me.panelButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelInputs As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents txtuserid As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtusername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtpassword As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbRole As ComboBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents panelButtons As Panel
    Friend WithEvents flowButtons As FlowLayoutPanel
    Friend WithEvents btnback As Button
    Friend WithEvents btndelete As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents btnupdate As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents btnnew As Button
End Class
