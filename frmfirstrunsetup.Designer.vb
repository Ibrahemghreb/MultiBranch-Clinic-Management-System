<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFirstRunSetup
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.lblNewPassword = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirmPassword = New System.Windows.Forms.Label()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.btnSetPassword = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.Location = New System.Drawing.Point(12, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(460, 38)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = ""
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInstructions
        '
        Me.lblInstructions.Location = New System.Drawing.Point(12, 60)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(460, 65)
        Me.lblInstructions.TabIndex = 1
        Me.lblInstructions.Text = ""
        '
        'lblNewPassword
        '
        Me.lblNewPassword.Location = New System.Drawing.Point(12, 138)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(160, 25)
        Me.lblNewPassword.TabIndex = 2
        Me.lblNewPassword.Text = "كلمة المرور الجديدة:"
        Me.lblNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(178, 138)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(290, 23)
        Me.txtNewPassword.TabIndex = 3
        '
        'lblConfirmPassword
        '
        Me.lblConfirmPassword.Location = New System.Drawing.Point(12, 178)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Size = New System.Drawing.Size(160, 25)
        Me.lblConfirmPassword.TabIndex = 4
        Me.lblConfirmPassword.Text = "تأكيد كلمة المرور:"
        Me.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Location = New System.Drawing.Point(178, 178)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.Size = New System.Drawing.Size(290, 23)
        Me.txtConfirmPassword.TabIndex = 5
        '
        'btnSetPassword
        '
        Me.btnSetPassword.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
        Me.btnSetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSetPassword.ForeColor = System.Drawing.Color.White
        Me.btnSetPassword.Location = New System.Drawing.Point(140, 225)
        Me.btnSetPassword.Name = "btnSetPassword"
        Me.btnSetPassword.Size = New System.Drawing.Size(200, 38)
        Me.btnSetPassword.TabIndex = 6
        Me.btnSetPassword.Text = "تعيين كلمة المرور والمتابعة"
        Me.btnSetPassword.UseVisualStyleBackColor = False
        '
        'frmFirstRunSetup
        '
        Me.ClientSize = New System.Drawing.Size(484, 285)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblInstructions)
        Me.Controls.Add(Me.lblNewPassword)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.lblConfirmPassword)
        Me.Controls.Add(Me.txtConfirmPassword)
        Me.Controls.Add(Me.btnSetPassword)
        Me.Name = "frmFirstRunSetup"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblInstructions As System.Windows.Forms.Label
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnSetPassword As System.Windows.Forms.Button

End Class
