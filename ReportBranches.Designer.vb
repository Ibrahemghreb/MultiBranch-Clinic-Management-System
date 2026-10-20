<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportBranches
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportBranches))
        rbcity = New RadioButton()
        rbbranchname = New RadioButton()
        rbBranchID = New RadioButton()
        DataGridView1 = New DataGridView()
        btnsearch = New Button()
        txtcity = New TextBox()
        txtbranchname = New TextBox()
        btnback = New Button()
        txtBranchid = New TextBox()
        btnprintexcel = New Button()
        btnprintpdf = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rbcity
        ' 
        rbcity.AutoSize = True
        rbcity.Font = New Font("Times New Roman", 14.25F)
        rbcity.Location = New Point(19, 102)
        rbcity.Name = "rbcity"
        rbcity.Size = New Size(59, 25)
        rbcity.TabIndex = 62
        rbcity.TabStop = True
        rbcity.Text = "City"
        rbcity.UseVisualStyleBackColor = True
        ' 
        ' rbbranchname
        ' 
        rbbranchname.AutoSize = True
        rbbranchname.Font = New Font("Times New Roman", 14.25F)
        rbbranchname.Location = New Point(19, 57)
        rbbranchname.Name = "rbbranchname"
        rbbranchname.Size = New Size(129, 25)
        rbbranchname.TabIndex = 61
        rbbranchname.TabStop = True
        rbbranchname.Text = "BranchName "
        rbbranchname.UseVisualStyleBackColor = True
        ' 
        ' rbBranchID
        ' 
        rbBranchID.AutoSize = True
        rbBranchID.Font = New Font("Times New Roman", 14.25F)
        rbBranchID.Location = New Point(19, 12)
        rbBranchID.Name = "rbBranchID"
        rbBranchID.Size = New Size(100, 25)
        rbBranchID.TabIndex = 60
        rbBranchID.TabStop = True
        rbBranchID.Text = "BranchID"
        rbBranchID.UseVisualStyleBackColor = True
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
        DataGridView1.Location = New Point(15, 150)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(874, 398)
        DataGridView1.TabIndex = 59
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(741, 12)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 58
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' txtcity
        ' 
        txtcity.Font = New Font("Times New Roman", 14.25F)
        txtcity.Location = New Point(146, 103)
        txtcity.Name = "txtcity"
        txtcity.Size = New Size(230, 29)
        txtcity.TabIndex = 56
        ' 
        ' txtbranchname
        ' 
        txtbranchname.Font = New Font("Times New Roman", 14.25F)
        txtbranchname.Location = New Point(146, 57)
        txtbranchname.Name = "txtbranchname"
        txtbranchname.Size = New Size(230, 29)
        txtbranchname.TabIndex = 55
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(387, 12)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 54
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtBranchid
        ' 
        txtBranchid.Font = New Font("Times New Roman", 14.25F)
        txtBranchid.Location = New Point(146, 11)
        txtBranchid.Name = "txtBranchid"
        txtBranchid.Size = New Size(230, 29)
        txtBranchid.TabIndex = 53
        ' 
        ' btnprintexcel
        ' 
        btnprintexcel.BackgroundImage = CType(resources.GetObject("btnprintexcel.BackgroundImage"), Image)
        btnprintexcel.BackgroundImageLayout = ImageLayout.Stretch
        btnprintexcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintexcel.Location = New Point(623, 12)
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
        btnprintpdf.Location = New Point(505, 12)
        btnprintpdf.Name = "btnprintpdf"
        btnprintpdf.Size = New Size(112, 69)
        btnprintpdf.TabIndex = 83
        btnprintpdf.Text = "Print PDF"
        btnprintpdf.TextAlign = ContentAlignment.BottomCenter
        btnprintpdf.UseVisualStyleBackColor = True
        ' 
        ' ReportBranches
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 557)
        Controls.Add(btnprintexcel)
        Controls.Add(btnprintpdf)
        Controls.Add(rbcity)
        Controls.Add(rbbranchname)
        Controls.Add(rbBranchID)
        Controls.Add(DataGridView1)
        Controls.Add(btnsearch)
        Controls.Add(txtcity)
        Controls.Add(txtbranchname)
        Controls.Add(btnback)
        Controls.Add(txtBranchid)
        MaximizeBox = False
        Name = "ReportBranches"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ReportBranches"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rbcity As RadioButton
    Friend WithEvents rbbranchname As RadioButton
    Friend WithEvents rbBranchID As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents txtcity As TextBox
    Friend WithEvents txtbranchname As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtBranchid As TextBox
    Friend WithEvents btnprintexcel As Button
    Friend WithEvents btnprintpdf As Button
End Class
