<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reportappointments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportAppointments))
        appointmentdate = New DateTimePicker()
        rbdoctorsid = New RadioButton()
        rbAppointmentID = New RadioButton()
        rbpatientid = New RadioButton()
        dgvResults = New DataGridView()
        btnsearch = New Button()
        btnexportpdf = New Button()
        txtdoctorid = New TextBox()
        txtappointmentid = New TextBox()
        btnback = New Button()
        txtPatientiD = New TextBox()
        btnExportExcel = New Button()
        rbAppointmentDate = New RadioButton()
        CType(dgvResults, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' appointmentdate
        ' 
        appointmentdate.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        appointmentdate.Format = DateTimePickerFormat.Short
        appointmentdate.Location = New Point(585, 100)
        appointmentdate.Name = "appointmentdate"
        appointmentdate.Size = New Size(229, 29)
        appointmentdate.TabIndex = 81
        ' 
        ' rbdoctorsid
        ' 
        rbdoctorsid.AutoSize = True
        rbdoctorsid.Font = New Font("Times New Roman", 14.25F)
        rbdoctorsid.Location = New Point(22, 102)
        rbdoctorsid.Name = "rbdoctorsid"
        rbdoctorsid.Size = New Size(100, 25)
        rbdoctorsid.TabIndex = 79
        rbdoctorsid.TabStop = True
        rbdoctorsid.Text = "DoctorID"
        rbdoctorsid.UseVisualStyleBackColor = True
        ' 
        ' rbAppointmentID
        ' 
        rbAppointmentID.AutoSize = True
        rbAppointmentID.Font = New Font("Times New Roman", 14.25F)
        rbAppointmentID.Location = New Point(22, 57)
        rbAppointmentID.Name = "rbAppointmentID"
        rbAppointmentID.Size = New Size(144, 25)
        rbAppointmentID.TabIndex = 78
        rbAppointmentID.TabStop = True
        rbAppointmentID.Text = "AppointmentID"
        rbAppointmentID.UseVisualStyleBackColor = True
        ' 
        ' rbpatientid
        ' 
        rbpatientid.AutoSize = True
        rbpatientid.Font = New Font("Times New Roman", 14.25F)
        rbpatientid.Location = New Point(22, 12)
        rbpatientid.Name = "rbpatientid"
        rbpatientid.Size = New Size(91, 25)
        rbpatientid.TabIndex = 77
        rbpatientid.TabStop = True
        rbpatientid.Text = "PatientD"
        rbpatientid.UseVisualStyleBackColor = True
        ' 
        ' dgvResults
        ' 
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvResults.DefaultCellStyle = DataGridViewCellStyle1
        dgvResults.Location = New Point(11, 150)
        dgvResults.Name = "dgvResults"
        dgvResults.Size = New Size(874, 398)
        dgvResults.TabIndex = 76
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(776, 10)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 75
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' btnexportpdf
        ' 
        btnexportpdf.BackgroundImage = CType(resources.GetObject("btnexportpdf.BackgroundImage"), Image)
        btnexportpdf.BackgroundImageLayout = ImageLayout.Stretch
        btnexportpdf.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnexportpdf.Location = New Point(538, 12)
        btnexportpdf.Name = "btnexportpdf"
        btnexportpdf.Size = New Size(112, 69)
        btnexportpdf.TabIndex = 74
        btnexportpdf.Text = "Print PDF"
        btnexportpdf.TextAlign = ContentAlignment.BottomCenter
        btnexportpdf.UseVisualStyleBackColor = True
        ' 
        ' txtdoctorid
        ' 
        txtdoctorid.Font = New Font("Times New Roman", 14.25F)
        txtdoctorid.Location = New Point(173, 103)
        txtdoctorid.Name = "txtdoctorid"
        txtdoctorid.Size = New Size(230, 29)
        txtdoctorid.TabIndex = 73
        ' 
        ' txtappointmentid
        ' 
        txtappointmentid.Font = New Font("Times New Roman", 14.25F)
        txtappointmentid.Location = New Point(173, 57)
        txtappointmentid.Name = "txtappointmentid"
        txtappointmentid.Size = New Size(230, 29)
        txtappointmentid.TabIndex = 72
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(420, 12)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 71
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtPatientiD
        ' 
        txtPatientiD.Font = New Font("Times New Roman", 14.25F)
        txtPatientiD.Location = New Point(173, 11)
        txtPatientiD.Name = "txtPatientiD"
        txtPatientiD.Size = New Size(230, 29)
        txtPatientiD.TabIndex = 70
        ' 
        ' btnExportExcel
        ' 
        btnExportExcel.BackgroundImage = CType(resources.GetObject("btnExportExcel.BackgroundImage"), Image)
        btnExportExcel.BackgroundImageLayout = ImageLayout.Stretch
        btnExportExcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnExportExcel.Location = New Point(656, 12)
        btnExportExcel.Name = "btnExportExcel"
        btnExportExcel.Size = New Size(112, 69)
        btnExportExcel.TabIndex = 82
        btnExportExcel.Text = "Print Excel"
        btnExportExcel.TextAlign = ContentAlignment.BottomCenter
        btnExportExcel.UseVisualStyleBackColor = True
        ' 
        ' rbAppointmentDate
        ' 
        rbAppointmentDate.AutoSize = True
        rbAppointmentDate.Font = New Font("Times New Roman", 14.25F)
        rbAppointmentDate.Location = New Point(420, 104)
        rbAppointmentDate.Name = "rbAppointmentDate"
        rbAppointmentDate.Size = New Size(159, 25)
        rbAppointmentDate.TabIndex = 83
        rbAppointmentDate.TabStop = True
        rbAppointmentDate.Text = "AppointmentDate"
        rbAppointmentDate.UseVisualStyleBackColor = True
        ' 
        ' ReportAppointments
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(893, 556)
        Controls.Add(rbAppointmentDate)
        Controls.Add(btnExportExcel)
        Controls.Add(appointmentdate)
        Controls.Add(rbdoctorsid)
        Controls.Add(rbAppointmentID)
        Controls.Add(rbpatientid)
        Controls.Add(dgvResults)
        Controls.Add(btnsearch)
        Controls.Add(btnexportpdf)
        Controls.Add(txtdoctorid)
        Controls.Add(txtappointmentid)
        Controls.Add(btnback)
        Controls.Add(txtPatientiD)
        MaximizeBox = False
        Name = "ReportAppointments"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ReportAppointments"
        CType(dgvResults, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents appointmentdate As DateTimePicker
    Friend WithEvents rbdoctorsid As RadioButton
    Friend WithEvents rbAppointmentID As RadioButton
    Friend WithEvents rbpatientid As RadioButton
    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents btnexportpdf As Button
    Friend WithEvents txtdoctorid As TextBox
    Friend WithEvents txtappointmentid As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtPatientiD As TextBox
    Friend WithEvents btnExportExcel As Button
    Friend WithEvents rbAppointmentDate As RadioButton
End Class
