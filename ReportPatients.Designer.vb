<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportPatients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportPatients))
        rbphone = New RadioButton()
        rbfirstname = New RadioButton()
        rbPatientD = New RadioButton()
        DataGridView1 = New DataGridView()
        btnsearch = New Button()
        txtphonedoctor = New TextBox()
        txtfirstname = New TextBox()
        btnback = New Button()
        txtPatientD = New TextBox()
        dateofbirthpatient = New DateTimePicker()
        rbDateofBirth = New RadioButton()
        btnprintexcel = New Button()
        btnprintpdf = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rbphone
        ' 
        rbphone.AutoSize = True
        rbphone.Font = New Font("Times New Roman", 14.25F)
        rbphone.Location = New Point(25, 102)
        rbphone.Name = "rbphone"
        rbphone.Size = New Size(80, 25)
        rbphone.TabIndex = 62
        rbphone.TabStop = True
        rbphone.Text = "Phone "
        rbphone.UseVisualStyleBackColor = True
        ' 
        ' rbfirstname
        ' 
        rbfirstname.AutoSize = True
        rbfirstname.Font = New Font("Times New Roman", 14.25F)
        rbfirstname.Location = New Point(25, 57)
        rbfirstname.Name = "rbfirstname"
        rbfirstname.Size = New Size(110, 25)
        rbfirstname.TabIndex = 61
        rbfirstname.TabStop = True
        rbfirstname.Text = "FirstName "
        rbfirstname.UseVisualStyleBackColor = True
        ' 
        ' rbPatientD
        ' 
        rbPatientD.AutoSize = True
        rbPatientD.Font = New Font("Times New Roman", 14.25F)
        rbPatientD.Location = New Point(25, 12)
        rbPatientD.Name = "rbPatientD"
        rbPatientD.Size = New Size(91, 25)
        rbPatientD.TabIndex = 60
        rbPatientD.TabStop = True
        rbPatientD.Text = "PatientD"
        rbPatientD.UseVisualStyleBackColor = True
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
        DataGridView1.Location = New Point(14, 150)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(874, 398)
        DataGridView1.TabIndex = 59
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(742, 12)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 58
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' txtphonedoctor
        ' 
        txtphonedoctor.Font = New Font("Times New Roman", 14.25F)
        txtphonedoctor.Location = New Point(134, 103)
        txtphonedoctor.Name = "txtphonedoctor"
        txtphonedoctor.Size = New Size(230, 29)
        txtphonedoctor.TabIndex = 56
        ' 
        ' txtfirstname
        ' 
        txtfirstname.Font = New Font("Times New Roman", 14.25F)
        txtfirstname.Location = New Point(134, 57)
        txtfirstname.Name = "txtfirstname"
        txtfirstname.Size = New Size(230, 29)
        txtfirstname.TabIndex = 55
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(386, 12)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 54
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtPatientD
        ' 
        txtPatientD.Font = New Font("Times New Roman", 14.25F)
        txtPatientD.Location = New Point(134, 11)
        txtPatientD.Name = "txtPatientD"
        txtPatientD.Size = New Size(230, 29)
        txtPatientD.TabIndex = 53
        ' 
        ' dateofbirthpatient
        ' 
        dateofbirthpatient.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dateofbirthpatient.Format = DateTimePickerFormat.Short
        dateofbirthpatient.Location = New Point(516, 100)
        dateofbirthpatient.Name = "dateofbirthpatient"
        dateofbirthpatient.Size = New Size(229, 29)
        dateofbirthpatient.TabIndex = 69
        ' 
        ' rbDateofBirth
        ' 
        rbDateofBirth.AutoSize = True
        rbDateofBirth.Font = New Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        rbDateofBirth.Location = New Point(386, 104)
        rbDateofBirth.Name = "rbDateofBirth"
        rbDateofBirth.Size = New Size(124, 25)
        rbDateofBirth.TabIndex = 70
        rbDateofBirth.TabStop = True
        rbDateofBirth.Text = "Date of Birth"
        rbDateofBirth.UseVisualStyleBackColor = True
        ' 
        ' btnprintexcel
        ' 
        btnprintexcel.BackgroundImage = CType(resources.GetObject("btnprintexcel.BackgroundImage"), Image)
        btnprintexcel.BackgroundImageLayout = ImageLayout.Stretch
        btnprintexcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintexcel.Location = New Point(624, 12)
        btnprintexcel.Name = "btnprintexcel"
        btnprintexcel.Size = New Size(112, 69)
        btnprintexcel.TabIndex = 84
        btnprintexcel.Text = "Print Excel"
        btnprintexcel.TextAlign = ContentAlignment.BottomCenter
        btnprintexcel.UseVisualStyleBackColor = True
        ' 
        ' btnprintpdf
        ' 
        btnprintpdf.BackgroundImage = CType(resources.GetObject("btnprintpdf.BackgroundImage"), Image)
        btnprintpdf.BackgroundImageLayout = ImageLayout.Stretch
        btnprintpdf.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintpdf.Location = New Point(506, 12)
        btnprintpdf.Name = "btnprintpdf"
        btnprintpdf.Size = New Size(112, 69)
        btnprintpdf.TabIndex = 83
        btnprintpdf.Text = "Print PDF"
        btnprintpdf.TextAlign = ContentAlignment.BottomCenter
        btnprintpdf.UseVisualStyleBackColor = True
        ' 
        ' ReportPatients
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(897, 561)
        Controls.Add(btnprintexcel)
        Controls.Add(btnprintpdf)
        Controls.Add(rbDateofBirth)
        Controls.Add(dateofbirthpatient)
        Controls.Add(rbphone)
        Controls.Add(rbfirstname)
        Controls.Add(rbPatientD)
        Controls.Add(DataGridView1)
        Controls.Add(btnsearch)
        Controls.Add(txtphonedoctor)
        Controls.Add(txtfirstname)
        Controls.Add(btnback)
        Controls.Add(txtPatientD)
        MaximizeBox = False
        Name = "ReportPatients"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ReportPatients"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rbphone As RadioButton
    Friend WithEvents rbfirstname As RadioButton
    Friend WithEvents rbPatientD As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents txtphonedoctor As TextBox
    Friend WithEvents txtfirstname As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtPatientD As TextBox
    Friend WithEvents dateofbirthpatient As DateTimePicker
    Friend WithEvents rbDateofBirth As RadioButton
    Friend WithEvents btnprintexcel As Button
    Friend WithEvents btnprintpdf As Button
End Class
