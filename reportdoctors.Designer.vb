<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reportdoctors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reportdoctors))
        DataGridView1 = New DataGridView()
        btnsearch = New Button()
        txtphonedoctor = New TextBox()
        txtfirstname = New TextBox()
        btnback = New Button()
        txtDoctorid = New TextBox()
        rbdoctorid = New RadioButton()
        rbfirstname = New RadioButton()
        rbphone = New RadioButton()
        btnprintexcel = New Button()
        btnprintpdf = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
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
        DataGridView1.Location = New Point(12, 163)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(874, 398)
        DataGridView1.TabIndex = 49
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(738, 25)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 48
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' txtphonedoctor
        ' 
        txtphonedoctor.Font = New Font("Times New Roman", 14.25F)
        txtphonedoctor.Location = New Point(132, 116)
        txtphonedoctor.Name = "txtphonedoctor"
        txtphonedoctor.Size = New Size(230, 29)
        txtphonedoctor.TabIndex = 46
        ' 
        ' txtfirstname
        ' 
        txtfirstname.Font = New Font("Times New Roman", 14.25F)
        txtfirstname.Location = New Point(132, 70)
        txtfirstname.Name = "txtfirstname"
        txtfirstname.Size = New Size(230, 29)
        txtfirstname.TabIndex = 45
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(384, 25)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 42
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtDoctorid
        ' 
        txtDoctorid.Font = New Font("Times New Roman", 14.25F)
        txtDoctorid.Location = New Point(132, 24)
        txtDoctorid.Name = "txtDoctorid"
        txtDoctorid.Size = New Size(230, 29)
        txtDoctorid.TabIndex = 41
        ' 
        ' rbdoctorid
        ' 
        rbdoctorid.AutoSize = True
        rbdoctorid.Font = New Font("Times New Roman", 14.25F)
        rbdoctorid.Location = New Point(23, 25)
        rbdoctorid.Name = "rbdoctorid"
        rbdoctorid.Size = New Size(100, 25)
        rbdoctorid.TabIndex = 50
        rbdoctorid.TabStop = True
        rbdoctorid.Text = "DoctorID"
        rbdoctorid.UseVisualStyleBackColor = True
        ' 
        ' rbfirstname
        ' 
        rbfirstname.AutoSize = True
        rbfirstname.Font = New Font("Times New Roman", 14.25F)
        rbfirstname.Location = New Point(23, 70)
        rbfirstname.Name = "rbfirstname"
        rbfirstname.Size = New Size(110, 25)
        rbfirstname.TabIndex = 51
        rbfirstname.TabStop = True
        rbfirstname.Text = "FirstName "
        rbfirstname.UseVisualStyleBackColor = True
        ' 
        ' rbphone
        ' 
        rbphone.AutoSize = True
        rbphone.Font = New Font("Times New Roman", 14.25F)
        rbphone.Location = New Point(23, 115)
        rbphone.Name = "rbphone"
        rbphone.Size = New Size(80, 25)
        rbphone.TabIndex = 52
        rbphone.TabStop = True
        rbphone.Text = "Phone "
        rbphone.UseVisualStyleBackColor = True
        ' 
        ' btnprintexcel
        ' 
        btnprintexcel.BackgroundImage = CType(resources.GetObject("btnprintexcel.BackgroundImage"), Image)
        btnprintexcel.BackgroundImageLayout = ImageLayout.Stretch
        btnprintexcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintexcel.Location = New Point(620, 24)
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
        btnprintpdf.Location = New Point(502, 24)
        btnprintpdf.Name = "btnprintpdf"
        btnprintpdf.Size = New Size(112, 69)
        btnprintpdf.TabIndex = 83
        btnprintpdf.Text = "Print PDF"
        btnprintpdf.TextAlign = ContentAlignment.BottomCenter
        btnprintpdf.UseVisualStyleBackColor = True
        ' 
        ' reportdoctors
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(898, 573)
        Controls.Add(btnprintexcel)
        Controls.Add(btnprintpdf)
        Controls.Add(rbphone)
        Controls.Add(rbfirstname)
        Controls.Add(rbdoctorid)
        Controls.Add(DataGridView1)
        Controls.Add(btnsearch)
        Controls.Add(txtphonedoctor)
        Controls.Add(txtfirstname)
        Controls.Add(btnback)
        Controls.Add(txtDoctorid)
        MaximizeBox = False
        Name = "reportdoctors"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Reportdoctors"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents txtphonedoctor As TextBox
    Friend WithEvents txtfirstname As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtDoctorid As TextBox
    Friend WithEvents rbdoctorid As RadioButton
    Friend WithEvents rbfirstname As RadioButton
    Friend WithEvents rbphone As RadioButton
    Friend WithEvents btnprintexcel As Button
    Friend WithEvents btnprintpdf As Button
End Class
