<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Activation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnActivate = New Button()
        Label1 = New Label()
        txtProcessorID = New TextBox()
        lblStatus = New Label()
        btnend = New Button()
        txtActivationKey = New TextBox()
        Label9 = New Label()
        lblMessage = New Label()
        btncopy = New Button()
        rdbTrial = New RadioButton()
        rdbLicense = New RadioButton()
        SuspendLayout()
        ' 
        ' btnActivate
        ' 
        btnActivate.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        btnActivate.Location = New Point(168, 314)
        btnActivate.Margin = New Padding(3, 2, 3, 2)
        btnActivate.Name = "btnActivate"
        btnActivate.Size = New Size(147, 71)
        btnActivate.TabIndex = 0
        btnActivate.Text = "Activation"
        btnActivate.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(15, 170)
        Label1.Name = "Label1"
        Label1.Size = New Size(200, 25)
        Label1.TabIndex = 1
        Label1.Text = "Customer Number"
        ' 
        ' txtProcessorID
        ' 
        txtProcessorID.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        txtProcessorID.Location = New Point(242, 167)
        txtProcessorID.Margin = New Padding(3, 2, 3, 2)
        txtProcessorID.Name = "txtProcessorID"
        txtProcessorID.ReadOnly = True
        txtProcessorID.Size = New Size(366, 34)
        txtProcessorID.TabIndex = 2
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        lblStatus.Location = New Point(117, 275)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(0, 25)
        lblStatus.TabIndex = 5
        ' 
        ' btnend
        ' 
        btnend.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        btnend.Location = New Point(15, 314)
        btnend.Margin = New Padding(3, 2, 3, 2)
        btnend.Name = "btnend"
        btnend.Size = New Size(147, 71)
        btnend.TabIndex = 17
        btnend.Text = "Exit"
        btnend.UseVisualStyleBackColor = True
        ' 
        ' txtActivationKey
        ' 
        txtActivationKey.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        txtActivationKey.Location = New Point(242, 221)
        txtActivationKey.Margin = New Padding(3, 2, 3, 2)
        txtActivationKey.Name = "txtActivationKey"
        txtActivationKey.Size = New Size(366, 34)
        txtActivationKey.TabIndex = 19
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        Label9.Location = New Point(15, 224)
        Label9.Name = "Label9"
        Label9.Size = New Size(172, 25)
        Label9.TabIndex = 18
        Label9.Text = "Activation Code"
        ' 
        ' lblMessage
        ' 
        lblMessage.AutoSize = True
        lblMessage.Font = New Font("Times New Roman", 20F, FontStyle.Bold)
        lblMessage.Location = New Point(282, 456)
        lblMessage.Name = "lblMessage"
        lblMessage.Size = New Size(0, 38)
        lblMessage.TabIndex = 20
        ' 
        ' btncopy
        ' 
        btncopy.FlatStyle = FlatStyle.Flat
        btncopy.Font = New Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btncopy.Location = New Point(637, 161)
        btncopy.Margin = New Padding(3, 2, 3, 2)
        btncopy.Name = "btncopy"
        btncopy.Size = New Size(61, 44)
        btncopy.TabIndex = 21
        btncopy.Text = "copy"
        btncopy.UseVisualStyleBackColor = True
        ' 
        ' rdbTrial
        ' 
        rdbTrial.AutoSize = True
        rdbTrial.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        rdbTrial.Location = New Point(24, 32)
        rdbTrial.Name = "rdbTrial"
        rdbTrial.RightToLeft = RightToLeft.No
        rdbTrial.Size = New Size(155, 29)
        rdbTrial.TabIndex = 22
        rdbTrial.TabStop = True
        rdbTrial.Text = "Trial version"
        rdbTrial.TextAlign = ContentAlignment.MiddleCenter
        rdbTrial.UseVisualStyleBackColor = True
        ' 
        ' rdbLicense
        ' 
        rdbLicense.AutoSize = True
        rdbLicense.Font = New Font("Times New Roman", 13.8F, FontStyle.Bold)
        rdbLicense.Location = New Point(24, 76)
        rdbLicense.Name = "rdbLicense"
        rdbLicense.RightToLeft = RightToLeft.No
        rdbLicense.Size = New Size(259, 29)
        rdbLicense.TabIndex = 23
        rdbLicense.TabStop = True
        rdbLicense.Text = "I Have code Activation"
        rdbLicense.TextAlign = ContentAlignment.MiddleCenter
        rdbLicense.UseVisualStyleBackColor = True
        ' 
        ' Activation
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(729, 430)
        Controls.Add(rdbLicense)
        Controls.Add(rdbTrial)
        Controls.Add(btncopy)
        Controls.Add(lblMessage)
        Controls.Add(txtActivationKey)
        Controls.Add(Label9)
        Controls.Add(btnend)
        Controls.Add(lblStatus)
        Controls.Add(txtProcessorID)
        Controls.Add(Label1)
        Controls.Add(btnActivate)
        Margin = New Padding(3, 2, 3, 2)
        MaximizeBox = False
        Name = "Activation"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Activation Form"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btnActivate As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtProcessorID As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnend As Button
    Friend WithEvents txtActivationKey As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblMessage As Label
    Friend WithEvents btncopy As Button
    Friend WithEvents rdbTrial As RadioButton
    Friend WithEvents rdbLicense As RadioButton
End Class
