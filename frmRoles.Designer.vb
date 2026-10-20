Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRoles
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
        Me.chkIsSystem = New System.Windows.Forms.CheckBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRoleName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRoleCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRoleID = New System.Windows.Forms.TextBox()
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
        Me.panelInputs.Controls.Add(Me.chkIsSystem)
        Me.panelInputs.Controls.Add(Me.txtDescription)
        Me.panelInputs.Controls.Add(Me.Label4)
        Me.panelInputs.Controls.Add(Me.txtRoleName)
        Me.panelInputs.Controls.Add(Me.Label3)
        Me.panelInputs.Controls.Add(Me.txtRoleCode)
        Me.panelInputs.Controls.Add(Me.Label2)
        Me.panelInputs.Controls.Add(Me.txtRoleID)
        Me.panelInputs.Controls.Add(Me.Label1)
        Me.panelInputs.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelInputs.Location = New System.Drawing.Point(3, 78)
        Me.panelInputs.Name = "panelInputs"
        Me.panelInputs.Size = New System.Drawing.Size(1018, 160)
        Me.panelInputs.TabIndex = 0
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(880, 106)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(78, 27)
        Me.chkIsActive.TabIndex = 4
        Me.chkIsActive.Text = "Active"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'chkIsSystem
        '
        Me.chkIsSystem.AutoSize = True
        Me.chkIsSystem.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.chkIsSystem.Location = New System.Drawing.Point(736, 107)
        Me.chkIsSystem.Name = "chkIsSystem"
        Me.chkIsSystem.Size = New System.Drawing.Size(86, 27)
        Me.chkIsSystem.TabIndex = 3
        Me.chkIsSystem.Text = "System"
        Me.chkIsSystem.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDescription.Location = New System.Drawing.Point(130, 72)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(600, 64)
        Me.txtDescription.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(16, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Description"
        '
        'txtRoleName
        '
        Me.txtRoleName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtRoleName.Location = New System.Drawing.Point(520, 24)
        Me.txtRoleName.Name = "txtRoleName"
        Me.txtRoleName.Size = New System.Drawing.Size(320, 30)
        Me.txtRoleName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(460, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 23)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Name"
        '
        'txtRoleCode
        '
        Me.txtRoleCode.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtRoleCode.Location = New System.Drawing.Point(130, 24)
        Me.txtRoleCode.Name = "txtRoleCode"
        Me.txtRoleCode.Size = New System.Drawing.Size(320, 30)
        Me.txtRoleCode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(16, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Code"
        '
        'txtRoleID
        '
        Me.txtRoleID.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtRoleID.Location = New System.Drawing.Point(880, 24)
        Me.txtRoleID.Name = "txtRoleID"
        Me.txtRoleID.Size = New System.Drawing.Size(110, 30)
        Me.txtRoleID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(800, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Role ID"
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
        Me.DataGridView1.Location = New System.Drawing.Point(3, 238)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1018, 391)
        Me.DataGridView1.TabIndex = 1
        '
        'frmRoles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 720)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelInputs)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "frmRoles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Role Management"
        Me.panelInputs.ResumeLayout(False)
        Me.panelInputs.PerformLayout()
        Me.panelButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelInputs As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRoleID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtRoleCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtRoleName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents chkIsSystem As CheckBox
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
