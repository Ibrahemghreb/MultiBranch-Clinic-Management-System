<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportDiagnoses
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportDiagnoses))
        Appointmentdate = New DateTimePicker()
        rbappointmentid = New RadioButton()
        rbdiagnosesid = New RadioButton()
        DataGridView1 = New DataGridView()
        btnsearch = New Button()
        txtAppointmentID = New TextBox()
        btnback = New Button()
        txtDiagnosestD = New TextBox()
        rbAppointmentDate = New RadioButton()
        btnprintexcel = New Button()
        btnprintpdf = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Appointmentdate
        ' 
        Appointmentdate.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Appointmentdate.Format = DateTimePickerFormat.Short
        Appointmentdate.Location = New Point(184, 102)
        Appointmentdate.Name = "Appointmentdate"
        Appointmentdate.Size = New Size(229, 29)
        Appointmentdate.TabIndex = 93
        ' 
        ' rbappointmentid
        ' 
        rbappointmentid.AutoSize = True
        rbappointmentid.Font = New Font("Times New Roman", 14.25F)
        rbappointmentid.Location = New Point(22, 59)
        rbappointmentid.Name = "rbappointmentid"
        rbappointmentid.Size = New Size(144, 25)
        rbappointmentid.TabIndex = 90
        rbappointmentid.TabStop = True
        rbappointmentid.Text = "AppointmentID"
        rbappointmentid.UseVisualStyleBackColor = True
        ' 
        ' rbdiagnosesid
        ' 
        rbdiagnosesid.AutoSize = True
        rbdiagnosesid.Font = New Font("Times New Roman", 14.25F)
        rbdiagnosesid.Location = New Point(22, 12)
        rbdiagnosesid.Name = "rbdiagnosesid"
        rbdiagnosesid.Size = New Size(123, 25)
        rbdiagnosesid.TabIndex = 89
        rbdiagnosesid.TabStop = True
        rbdiagnosesid.Text = "DiagnosestD"
        rbdiagnosesid.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.Location = New Point(12, 151)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(874, 398)
        DataGridView1.TabIndex = 88
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(770, 12)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 87
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' txtAppointmentID
        ' 
        txtAppointmentID.Font = New Font("Times New Roman", 14.25F)
        txtAppointmentID.Location = New Point(184, 55)
        txtAppointmentID.Name = "txtAppointmentID"
        txtAppointmentID.Size = New Size(230, 29)
        txtAppointmentID.TabIndex = 84
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(420, 12)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 83
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtDiagnosestD
        ' 
        txtDiagnosestD.Font = New Font("Times New Roman", 14.25F)
        txtDiagnosestD.Location = New Point(184, 11)
        txtDiagnosestD.Name = "txtDiagnosestD"
        txtDiagnosestD.Size = New Size(230, 29)
        txtDiagnosestD.TabIndex = 82
        ' 
        ' rbAppointmentDate
        ' 
        rbAppointmentDate.AutoSize = True
        rbAppointmentDate.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rbAppointmentDate.Location = New Point(22, 106)
        rbAppointmentDate.Name = "rbAppointmentDate"
        rbAppointmentDate.Size = New Size(164, 25)
        rbAppointmentDate.TabIndex = 94
        rbAppointmentDate.TabStop = True
        rbAppointmentDate.Text = "AppointmentDate "
        rbAppointmentDate.UseVisualStyleBackColor = True
        ' 
        ' btnprintexcel
        ' 
        btnprintexcel.BackgroundImage = CType(resources.GetObject("btnprintexcel.BackgroundImage"), Image)
        btnprintexcel.BackgroundImageLayout = ImageLayout.Stretch
        btnprintexcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintexcel.Location = New Point(656, 12)
        btnprintexcel.Name = "btnprintexcel"
        btnprintexcel.Size = New Size(112, 69)
        btnprintexcel.TabIndex = 96
        btnprintexcel.Text = "Print Excel"
        btnprintexcel.TextAlign = ContentAlignment.BottomCenter
        btnprintexcel.UseVisualStyleBackColor = True
        ' 
        ' btnprintpdf
        ' 
        btnprintpdf.BackgroundImage = CType(resources.GetObject("btnprintpdf.BackgroundImage"), Image)
        btnprintpdf.BackgroundImageLayout = ImageLayout.Stretch
        btnprintpdf.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintpdf.Location = New Point(538, 12)
        btnprintpdf.Name = "btnprintpdf"
        btnprintpdf.Size = New Size(112, 69)
        btnprintpdf.TabIndex = 95
        btnprintpdf.Text = "Print PDF"
        btnprintpdf.TextAlign = ContentAlignment.BottomCenter
        btnprintpdf.UseVisualStyleBackColor = True
        ' 
        ' ReportDiagnoses
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(894, 561)
        Controls.Add(btnprintexcel)
        Controls.Add(btnprintpdf)
        Controls.Add(rbAppointmentDate)
        Controls.Add(Appointmentdate)
        Controls.Add(rbappointmentid)
        Controls.Add(rbdiagnosesid)
        Controls.Add(DataGridView1)
        Controls.Add(btnsearch)
        Controls.Add(txtAppointmentID)
        Controls.Add(btnback)
        Controls.Add(txtDiagnosestD)
        MaximizeBox = False
        Name = "ReportDiagnoses"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ReportDiagnoses"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Appointmentdate As DateTimePicker
    Friend WithEvents rbappointmentid As RadioButton
    Friend WithEvents rbdiagnosesid As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents txtAppointmentID As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtDiagnosestD As TextBox
    Friend WithEvents rbAppointmentDate As RadioButton
    Friend WithEvents btnprintexcel As Button
    Friend WithEvents btnprintpdf As Button
End Class
