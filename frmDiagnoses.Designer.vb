Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiagnoses
    Inherits ReaLTaiizor.Forms.MaterialForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiagnoses))
        Me.Diagnosisdate = New System.Windows.Forms.DateTimePicker()
        Me.txtnotes = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtlastname = New System.Windows.Forms.Label()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.txtdiagnosis = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtreport = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.txtDiagnosisID2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnback = New System.Windows.Forms.Button()
        Me.xrayimage = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.space = New System.Windows.Forms.Label()
        Me.btnResetPoints = New System.Windows.Forms.Button()
        Me.txtappointmentid = New System.Windows.Forms.ComboBox()
        Me.txtPatientID = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.xrayimage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Diagnosisdate
        '
        Me.Diagnosisdate.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Diagnosisdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Diagnosisdate.Location = New System.Drawing.Point(182, 203)
        Me.Diagnosisdate.Name = "Diagnosisdate"
        Me.Diagnosisdate.Size = New System.Drawing.Size(261, 35)
        Me.Diagnosisdate.TabIndex = 109
        '
        'txtnotes
        '
        Me.txtnotes.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtnotes.Location = New System.Drawing.Point(571, 361)
        Me.txtnotes.Multiline = True
        Me.txtnotes.Name = "txtnotes"
        Me.txtnotes.Size = New System.Drawing.Size(758, 30)
        Me.txtnotes.TabIndex = 108
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label3.Location = New System.Drawing.Point(571, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 27)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "Report "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label6.Location = New System.Drawing.Point(14, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(151, 27)
        Me.Label6.TabIndex = 106
        Me.Label6.Text = "DiagnosisDate"
        '
        'txtlastname
        '
        Me.txtlastname.AutoSize = True
        Me.txtlastname.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtlastname.Location = New System.Drawing.Point(14, 269)
        Me.txtlastname.Name = "txtlastname"
        Me.txtlastname.Size = New System.Drawing.Size(127, 27)
        Me.txtlastname.TabIndex = 105
        Me.txtlastname.Text = "XRayImage"
        '
        'btnnew
        '
        Me.btnnew.BackgroundImage = CType(resources.GetObject("btnnew.BackgroundImage"), System.Drawing.Image)
        Me.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnnew.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnnew.Location = New System.Drawing.Point(940, 707)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(128, 74)
        Me.btnnew.TabIndex = 103
        Me.btnnew.Text = "New"
        Me.btnnew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.BackgroundImage = CType(resources.GetObject("btnsave.BackgroundImage"), System.Drawing.Image)
        Me.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsave.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnsave.Location = New System.Drawing.Point(807, 707)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(128, 74)
        Me.btnsave.TabIndex = 102
        Me.btnsave.Text = "Save"
        Me.btnsave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'txtdiagnosis
        '
        Me.txtdiagnosis.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtdiagnosis.Location = New System.Drawing.Point(14, 505)
        Me.txtdiagnosis.Multiline = True
        Me.txtdiagnosis.Name = "txtdiagnosis"
        Me.txtdiagnosis.Size = New System.Drawing.Size(551, 184)
        Me.txtdiagnosis.TabIndex = 101
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label5.Location = New System.Drawing.Point(14, 475)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 27)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "Diagnosis"
        '
        'txtreport
        '
        Me.txtreport.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtreport.Location = New System.Drawing.Point(571, 158)
        Me.txtreport.Multiline = True
        Me.txtreport.Name = "txtreport"
        Me.txtreport.Size = New System.Drawing.Size(752, 160)
        Me.txtreport.TabIndex = 99
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label4.Location = New System.Drawing.Point(14, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 27)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "DiagnosisID "
        '
        'btnedit
        '
        Me.btnedit.BackgroundImage = CType(resources.GetObject("btnedit.BackgroundImage"), System.Drawing.Image)
        Me.btnedit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnedit.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnedit.Location = New System.Drawing.Point(672, 707)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(128, 74)
        Me.btnedit.TabIndex = 97
        Me.btnedit.Text = "Edit"
        Me.btnedit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnfind
        '
        Me.btnfind.BackgroundImage = CType(resources.GetObject("btnfind.BackgroundImage"), System.Drawing.Image)
        Me.btnfind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnfind.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnfind.Location = New System.Drawing.Point(540, 707)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(128, 74)
        Me.btnfind.TabIndex = 96
        Me.btnfind.Text = "Find"
        Me.btnfind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnfind.UseVisualStyleBackColor = True
        '
        'txtDiagnosisID2
        '
        Me.txtDiagnosisID2.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtDiagnosisID2.Location = New System.Drawing.Point(182, 95)
        Me.txtDiagnosisID2.Name = "txtDiagnosisID2"
        Me.txtDiagnosisID2.Size = New System.Drawing.Size(262, 35)
        Me.txtDiagnosisID2.TabIndex = 94
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label1.Location = New System.Drawing.Point(14, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 27)
        Me.Label1.TabIndex = 93
        Me.Label1.Text = "AppointmentID"
        '
        'btndelete
        '
        Me.btndelete.BackgroundImage = CType(resources.GetObject("btndelete.BackgroundImage"), System.Drawing.Image)
        Me.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btndelete.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btndelete.Location = New System.Drawing.Point(405, 707)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 74)
        Me.btndelete.TabIndex = 92
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnback
        '
        Me.btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), System.Drawing.Image)
        Me.btnback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnback.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnback.Location = New System.Drawing.Point(272, 707)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(128, 74)
        Me.btnback.TabIndex = 91
        Me.btnback.Text = "Back"
        Me.btnback.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnback.UseVisualStyleBackColor = True
        '
        'xrayimage
        '
        Me.xrayimage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.xrayimage.Location = New System.Drawing.Point(14, 312)
        Me.xrayimage.Name = "xrayimage"
        Me.xrayimage.Size = New System.Drawing.Size(551, 155)
        Me.xrayimage.TabIndex = 110
        Me.xrayimage.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label2.Location = New System.Drawing.Point(572, 327)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 27)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "Notes"
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Button1.Location = New System.Drawing.Point(177, 260)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(158, 31)
        Me.Button1.TabIndex = 112
        Me.Button1.Text = "UPload Photo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'space
        '
        Me.space.AutoSize = True
        Me.space.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.space.Location = New System.Drawing.Point(475, 265)
        Me.space.Name = "space"
        Me.space.Size = New System.Drawing.Size(69, 27)
        Me.space.TabIndex = 113
        Me.space.Text = "Space"
        '
        'btnResetPoints
        '
        Me.btnResetPoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetPoints.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnResetPoints.Location = New System.Drawing.Point(342, 260)
        Me.btnResetPoints.Name = "btnResetPoints"
        Me.btnResetPoints.Size = New System.Drawing.Size(120, 31)
        Me.btnResetPoints.TabIndex = 114
        Me.btnResetPoints.Text = "Re-mesure"
        Me.btnResetPoints.UseVisualStyleBackColor = True
        '
        'txtappointmentid
        '
        Me.txtappointmentid.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtappointmentid.FormattingEnabled = True
        Me.txtappointmentid.Location = New System.Drawing.Point(182, 131)
        Me.txtappointmentid.Name = "txtappointmentid"
        Me.txtappointmentid.Size = New System.Drawing.Size(262, 35)
        Me.txtappointmentid.TabIndex = 115
        '
        'txtPatientID
        '
        Me.txtPatientID.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientID.FormattingEnabled = True
        Me.txtPatientID.Location = New System.Drawing.Point(182, 167)
        Me.txtPatientID.Name = "txtPatientID"
        Me.txtPatientID.Size = New System.Drawing.Size(262, 35)
        Me.txtPatientID.TabIndex = 117
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label7.Location = New System.Drawing.Point(14, 172)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 27)
        Me.Label7.TabIndex = 116
        Me.Label7.Text = "Patient ID"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(576, 411)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(752, 278)
        Me.DataGridView1.TabIndex = 118
        '
        'frmDiagnoses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1341, 780)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtPatientID)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtappointmentid)
        Me.Controls.Add(Me.btnResetPoints)
        Me.Controls.Add(Me.space)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.xrayimage)
        Me.Controls.Add(Me.Diagnosisdate)
        Me.Controls.Add(Me.txtnotes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtlastname)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.txtdiagnosis)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtreport)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.txtDiagnosisID2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnback)
        Me.MaximizeBox = False
        Me.Name = "frmDiagnoses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diagnoses"
        CType(Me.xrayimage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Diagnosisdate As DateTimePicker
    Friend WithEvents txtnotes As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtlastname As Label
    Friend WithEvents btnnew As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents txtdiagnosis As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtreport As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnedit As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents txtDiagnosisID2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btndelete As Button
    Friend WithEvents btnback As Button
    Friend WithEvents xrayimage As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents space As Label
    Friend WithEvents btnResetPoints As Button
    Friend WithEvents txtappointmentid As ComboBox
    Friend WithEvents txtPatientID As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents DataGridView1 As DataGridView
End Class
