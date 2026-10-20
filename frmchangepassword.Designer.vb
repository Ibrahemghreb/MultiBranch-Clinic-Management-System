<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmChangePassword
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.lblOld = New System.Windows.Forms.Label()
        Me.txtOldPassword = New System.Windows.Forms.TextBox()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirm = New System.Windows.Forms.Label()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNote
        '
        Me.lblNote.Location = New System.Drawing.Point(12, 12)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(460, 45)
        Me.lblNote.TabIndex = 0
        Me.lblNote.Text = ""
        '
        'lblOld
        '
        Me.lblOld.Location = New System.Drawing.Point(12, 68)
        Me.lblOld.Name = "lblOld"
        Me.lblOld.Size = New System.Drawing.Size(160, 25)
        Me.lblOld.TabIndex = 1
        Me.lblOld.Text = "كلمة المرور الحالية:"
        Me.lblOld.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOldPassword
        '
        Me.txtOldPassword.Location = New System.Drawing.Point(178, 68)
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.Size = New System.Drawing.Size(290, 23)
        Me.txtOldPassword.TabIndex = 2
        '
        'lblNew
        '
        Me.lblNew.Location = New System.Drawing.Point(12, 108)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(160, 25)
        Me.lblNew.TabIndex = 3
        Me.lblNew.Text = "كلمة المرور الجديدة:"
        Me.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(178, 108)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(290, 23)
        Me.txtNewPassword.TabIndex = 4
        '
        'lblConfirm
        '
        Me.lblConfirm.Location = New System.Drawing.Point(12, 148)
        Me.lblConfirm.Name = "lblConfirm"
        Me.lblConfirm.Size = New System.Drawing.Size(160, 25)
        Me.lblConfirm.TabIndex = 5
        Me.lblConfirm.Text = "تأكيد كلمة المرور:"
        Me.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Location = New System.Drawing.Point(178, 148)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.Size = New System.Drawing.Size(290, 23)
        Me.txtConfirmPassword.TabIndex = 6
        '
        'btnChange
        '
        Me.btnChange.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
        Me.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChange.ForeColor = System.Drawing.Color.White
        Me.btnChange.Location = New System.Drawing.Point(178, 198)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(170, 35)
        Me.btnChange.TabIndex = 7
        Me.btnChange.Text = "تغيير كلمة المرور"
        Me.btnChange.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(358, 198)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 35)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmChangePassword
        '
        Me.ClientSize = New System.Drawing.Size(484, 261)
        Me.Controls.Add(Me.lblNote)
        Me.Controls.Add(Me.lblOld)
        Me.Controls.Add(Me.txtOldPassword)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.lblConfirm)
        Me.Controls.Add(Me.txtConfirmPassword)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmChangePassword"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents lblOld As System.Windows.Forms.Label
    Friend WithEvents txtOldPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirm As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button

End Class
