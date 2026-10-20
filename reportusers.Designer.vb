<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reportusers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reportusers))
        rbbranchid = New RadioButton()
        rbuserid = New RadioButton()
        Rbusername = New RadioButton()
        DataGridView1 = New DataGridView()
        btnsearch = New Button()
        txtbranchid = New TextBox()
        txtuserid = New TextBox()
        btnback = New Button()
        txtusername = New TextBox()
        btnprintexcel = New Button()
        btnprintpdf = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' rbbranchid
        ' 
        rbbranchid.AutoSize = True
        rbbranchid.Font = New Font("Times New Roman", 14.25F)
        rbbranchid.Location = New Point(22, 102)
        rbbranchid.Name = "rbbranchid"
        rbbranchid.Size = New Size(100, 25)
        rbbranchid.TabIndex = 62
        rbbranchid.TabStop = True
        rbbranchid.Text = "BranchID"
        rbbranchid.UseVisualStyleBackColor = True
        ' 
        ' rbuserid
        ' 
        rbuserid.AutoSize = True
        rbuserid.Font = New Font("Times New Roman", 14.25F)
        rbuserid.Location = New Point(22, 57)
        rbuserid.Name = "rbuserid"
        rbuserid.Size = New Size(82, 25)
        rbuserid.TabIndex = 61
        rbuserid.TabStop = True
        rbuserid.Text = "UserID"
        rbuserid.UseVisualStyleBackColor = True
        ' 
        ' Rbusername
        ' 
        Rbusername.AutoSize = True
        Rbusername.Font = New Font("Times New Roman", 14.25F)
        Rbusername.Location = New Point(22, 12)
        Rbusername.Name = "Rbusername"
        Rbusername.Size = New Size(106, 25)
        Rbusername.TabIndex = 60
        Rbusername.TabStop = True
        Rbusername.Text = "UserName"
        Rbusername.UseVisualStyleBackColor = True
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
        DataGridView1.Location = New Point(11, 150)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(874, 398)
        DataGridView1.TabIndex = 59
        ' 
        ' btnsearch
        ' 
        btnsearch.BackgroundImage = CType(resources.GetObject("btnsearch.BackgroundImage"), Image)
        btnsearch.BackgroundImageLayout = ImageLayout.Stretch
        btnsearch.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnsearch.Location = New Point(739, 11)
        btnsearch.Name = "btnsearch"
        btnsearch.Size = New Size(112, 69)
        btnsearch.TabIndex = 58
        btnsearch.Text = "Search"
        btnsearch.TextAlign = ContentAlignment.BottomCenter
        btnsearch.UseVisualStyleBackColor = True
        ' 
        ' txtbranchid
        ' 
        txtbranchid.Font = New Font("Times New Roman", 14.25F)
        txtbranchid.Location = New Point(131, 103)
        txtbranchid.Name = "txtbranchid"
        txtbranchid.Size = New Size(230, 29)
        txtbranchid.TabIndex = 56
        ' 
        ' txtuserid
        ' 
        txtuserid.Font = New Font("Times New Roman", 14.25F)
        txtuserid.Location = New Point(131, 57)
        txtuserid.Name = "txtuserid"
        txtuserid.Size = New Size(230, 29)
        txtuserid.TabIndex = 55
        ' 
        ' btnback
        ' 
        btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), Image)
        btnback.BackgroundImageLayout = ImageLayout.Stretch
        btnback.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnback.Location = New Point(383, 12)
        btnback.Name = "btnback"
        btnback.Size = New Size(112, 69)
        btnback.TabIndex = 54
        btnback.Text = "Back"
        btnback.TextAlign = ContentAlignment.BottomCenter
        btnback.UseVisualStyleBackColor = True
        ' 
        ' txtusername
        ' 
        txtusername.Font = New Font("Times New Roman", 14.25F)
        txtusername.Location = New Point(131, 11)
        txtusername.Name = "txtusername"
        txtusername.Size = New Size(230, 29)
        txtusername.TabIndex = 53
        ' 
        ' btnprintexcel
        ' 
        btnprintexcel.BackgroundImage = CType(resources.GetObject("btnprintexcel.BackgroundImage"), Image)
        btnprintexcel.BackgroundImageLayout = ImageLayout.Stretch
        btnprintexcel.Font = New Font("Times New Roman", 14.25F, FontStyle.Bold)
        btnprintexcel.Location = New Point(621, 12)
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
        btnprintpdf.Location = New Point(503, 12)
        btnprintpdf.Name = "btnprintpdf"
        btnprintpdf.Size = New Size(112, 69)
        btnprintpdf.TabIndex = 83
        btnprintpdf.Text = "Print PDF"
        btnprintpdf.TextAlign = ContentAlignment.BottomCenter
        btnprintpdf.UseVisualStyleBackColor = True
        ' 
        ' reportusers
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(891, 560)
        Controls.Add(btnprintexcel)
        Controls.Add(btnprintpdf)
        Controls.Add(rbbranchid)
        Controls.Add(rbuserid)
        Controls.Add(Rbusername)
        Controls.Add(DataGridView1)
        Controls.Add(btnsearch)
        Controls.Add(txtbranchid)
        Controls.Add(txtuserid)
        Controls.Add(btnback)
        Controls.Add(txtusername)
        MaximizeBox = False
        Name = "reportusers"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Reportusers"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents rbbranchid As RadioButton
    Friend WithEvents rbuserid As RadioButton
    Friend WithEvents Rbusername As RadioButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnsearch As Button
    Friend WithEvents txtbranchid As TextBox
    Friend WithEvents txtuserid As TextBox
    Friend WithEvents btnback As Button
    Friend WithEvents txtusername As TextBox
    Friend WithEvents btnprintexcel As Button
    Friend WithEvents btnprintpdf As Button
End Class
