Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmappointments
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmappointments))
        Me.appointmentdate = New System.Windows.Forms.DateTimePicker()
        Me.txtnotes = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDoctorID = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.txtappointmentID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnback = New System.Windows.Forms.Button()
        Me.txtpatientid = New System.Windows.Forms.ComboBox()
        Me.Doctorid = New System.Windows.Forms.ComboBox()
        Me.txtbranchID = New System.Windows.Forms.ComboBox()
        Me.txtquenuenumber = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtstatus = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'appointmentdate
        '
        Me.appointmentdate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.appointmentdate.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.appointmentdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.appointmentdate.Location = New System.Drawing.Point(209, 180)
        Me.appointmentdate.Name = "appointmentdate"
        Me.appointmentdate.ShowUpDown = True
        Me.appointmentdate.Size = New System.Drawing.Size(241, 35)
        Me.appointmentdate.TabIndex = 90
        '
        'txtnotes
        '
        Me.txtnotes.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtnotes.Location = New System.Drawing.Point(597, 180)
        Me.txtnotes.Name = "txtnotes"
        Me.txtnotes.Size = New System.Drawing.Size(243, 35)
        Me.txtnotes.TabIndex = 89
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label3.Location = New System.Drawing.Point(461, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 27)
        Me.Label3.TabIndex = 88
        Me.Label3.Text = "Notes"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label6.Location = New System.Drawing.Point(14, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(189, 27)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Appointment Start"
        '
        'txtDoctorID
        '
        Me.txtDoctorID.AutoSize = True
        Me.txtDoctorID.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtDoctorID.Location = New System.Drawing.Point(461, 144)
        Me.txtDoctorID.Name = "txtDoctorID"
        Me.txtDoctorID.Size = New System.Drawing.Size(104, 27)
        Me.txtDoctorID.TabIndex = 85
        Me.txtDoctorID.Text = "DoctorID"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(26, 286)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(814, 324)
        Me.DataGridView1.TabIndex = 84
        '
        'btnnew
        '
        Me.btnnew.BackgroundImage = CType(resources.GetObject("btnnew.BackgroundImage"), System.Drawing.Image)
        Me.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnnew.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnnew.Location = New System.Drawing.Point(701, 622)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(128, 74)
        Me.btnnew.TabIndex = 83
        Me.btnnew.Text = "New"
        Me.btnnew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.BackgroundImage = CType(resources.GetObject("btnsave.BackgroundImage"), System.Drawing.Image)
        Me.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsave.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnsave.Location = New System.Drawing.Point(568, 622)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(128, 74)
        Me.btnsave.TabIndex = 82
        Me.btnsave.Text = "Save"
        Me.btnsave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label5.Location = New System.Drawing.Point(458, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 27)
        Me.Label5.TabIndex = 80
        Me.Label5.Text = "BranchID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label4.Location = New System.Drawing.Point(14, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 27)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "AppointmentID"
        '
        'btnedit
        '
        Me.btnedit.BackgroundImage = CType(resources.GetObject("btnedit.BackgroundImage"), System.Drawing.Image)
        Me.btnedit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnedit.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnedit.Location = New System.Drawing.Point(433, 622)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(128, 74)
        Me.btnedit.TabIndex = 75
        Me.btnedit.Text = "Edit"
        Me.btnedit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnfind
        '
        Me.btnfind.BackgroundImage = CType(resources.GetObject("btnfind.BackgroundImage"), System.Drawing.Image)
        Me.btnfind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnfind.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnfind.Location = New System.Drawing.Point(301, 622)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(128, 74)
        Me.btnfind.TabIndex = 74
        Me.btnfind.Text = "Find"
        Me.btnfind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnfind.UseVisualStyleBackColor = True
        '
        'txtappointmentID
        '
        Me.txtappointmentID.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtappointmentID.Location = New System.Drawing.Point(208, 93)
        Me.txtappointmentID.Name = "txtappointmentID"
        Me.txtappointmentID.Size = New System.Drawing.Size(242, 35)
        Me.txtappointmentID.TabIndex = 72
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label1.Location = New System.Drawing.Point(14, 144)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 27)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "PatientID"
        '
        'btndelete
        '
        Me.btndelete.BackgroundImage = CType(resources.GetObject("btndelete.BackgroundImage"), System.Drawing.Image)
        Me.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btndelete.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btndelete.Location = New System.Drawing.Point(166, 622)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 74)
        Me.btndelete.TabIndex = 69
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnback
        '
        Me.btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), System.Drawing.Image)
        Me.btnback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnback.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnback.Location = New System.Drawing.Point(33, 622)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(128, 74)
        Me.btnback.TabIndex = 68
        Me.btnback.Text = "Back"
        Me.btnback.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnback.UseVisualStyleBackColor = True
        '
        'txtpatientid
        '
        Me.txtpatientid.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtpatientid.Location = New System.Drawing.Point(208, 137)
        Me.txtpatientid.Name = "txtpatientid"
        Me.txtpatientid.Size = New System.Drawing.Size(242, 35)
        Me.txtpatientid.TabIndex = 73
        '
        'Doctorid
        '
        Me.Doctorid.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Doctorid.Location = New System.Drawing.Point(597, 137)
        Me.Doctorid.Name = "Doctorid"
        Me.Doctorid.Size = New System.Drawing.Size(242, 35)
        Me.Doctorid.TabIndex = 79
        '
        'txtbranchID
        '
        Me.txtbranchID.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtbranchID.Location = New System.Drawing.Point(597, 93)
        Me.txtbranchID.Name = "txtbranchID"
        Me.txtbranchID.Size = New System.Drawing.Size(242, 35)
        Me.txtbranchID.TabIndex = 91
        '
        'txtquenuenumber
        '
        Me.txtquenuenumber.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtquenuenumber.Location = New System.Drawing.Point(209, 227)
        Me.txtquenuenumber.Name = "txtquenuenumber"
        Me.txtquenuenumber.Size = New System.Drawing.Size(243, 35)
        Me.txtquenuenumber.TabIndex = 93
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label2.Location = New System.Drawing.Point(14, 227)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 27)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "QueueNumber"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label7.Location = New System.Drawing.Point(458, 231)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 27)
        Me.Label7.TabIndex = 94
        Me.Label7.Text = "Status"
        '
        'txtstatus
        '
        Me.txtstatus.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtstatus.Location = New System.Drawing.Point(597, 225)
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.Size = New System.Drawing.Size(242, 35)
        Me.txtstatus.TabIndex = 95
        '
        'frmappointments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 728)
        Me.Controls.Add(Me.txtstatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtquenuenumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.appointmentdate)
        Me.Controls.Add(Me.txtnotes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDoctorID)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.txtbranchID)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Doctorid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.txtpatientid)
        Me.Controls.Add(Me.txtappointmentID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnback)
        Me.MaximizeBox = False
        Me.Name = "frmappointments"
        Me.Padding = New System.Windows.Forms.Padding(3, 78, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Appointments"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents appointmentdate As DateTimePicker
    Friend WithEvents txtnotes As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtDoctorID As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnnew As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnedit As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents txtappointmentID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btndelete As Button
    Friend WithEvents btnback As Button
    Friend WithEvents txtpatientid As ComboBox
    Friend WithEvents Doctorid As ComboBox
    Friend WithEvents txtbranchID As ComboBox
    Friend WithEvents txtquenuenumber As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtstatus As ComboBox
End Class
