Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpatients
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmpatients))
        Me.txtpatientphone = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtlastname = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtpatientlname = New System.Windows.Forms.TextBox()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.txtphone = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.txtpatientfname = New System.Windows.Forms.TextBox()
        Me.txtPatientID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnback = New System.Windows.Forms.Button()
        Me.txtpatientemail = New System.Windows.Forms.TextBox()
        Me.txtPatientCode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNationalID = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPassportNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPhone2 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCountryCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtBloodType = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtEmergencyContact = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtEmergencyPhone = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dateofbirthpatient = New System.Windows.Forms.DateTimePicker()
        Me.txtgender = New System.Windows.Forms.ComboBox()
        Me.txtdoctorid = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtbranchid = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtpatientphone
        '
        Me.txtpatientphone.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtpatientphone.Location = New System.Drawing.Point(551, 179)
        Me.txtpatientphone.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtpatientphone.Name = "txtpatientphone"
        Me.txtpatientphone.Size = New System.Drawing.Size(263, 35)
        Me.txtpatientphone.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label6.Location = New System.Drawing.Point(13, 225)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 27)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "Date of Birth"
        '
        'txtlastname
        '
        Me.txtlastname.AutoSize = True
        Me.txtlastname.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtlastname.Location = New System.Drawing.Point(428, 135)
        Me.txtlastname.Name = "txtlastname"
        Me.txtlastname.Size = New System.Drawing.Size(111, 27)
        Me.txtlastname.TabIndex = 61
        Me.txtlastname.Text = "LastName"
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
        Me.DataGridView1.Location = New System.Drawing.Point(7, 471)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1285, 161)
        Me.DataGridView1.TabIndex = 60
        '
        'btnnew
        '
        Me.btnnew.BackgroundImage = CType(resources.GetObject("btnnew.BackgroundImage"), System.Drawing.Image)
        Me.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnnew.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnnew.Location = New System.Drawing.Point(677, 640)
        Me.btnnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(128, 74)
        Me.btnnew.TabIndex = 9
        Me.btnnew.Text = "New"
        Me.btnnew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnnew.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.BackgroundImage = CType(resources.GetObject("btnsave.BackgroundImage"), System.Drawing.Image)
        Me.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsave.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnsave.Location = New System.Drawing.Point(545, 640)
        Me.btnsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(128, 74)
        Me.btnsave.TabIndex = 10
        Me.btnsave.Text = "Save"
        Me.btnsave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label5.Location = New System.Drawing.Point(428, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 27)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "Gender"
        '
        'txtpatientlname
        '
        Me.txtpatientlname.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtpatientlname.Location = New System.Drawing.Point(552, 136)
        Me.txtpatientlname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtpatientlname.Name = "txtpatientlname"
        Me.txtpatientlname.Size = New System.Drawing.Size(263, 35)
        Me.txtpatientlname.TabIndex = 6
        '
        'txtaddress
        '
        Me.txtaddress.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtaddress.Location = New System.Drawing.Point(156, 176)
        Me.txtaddress.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.Size = New System.Drawing.Size(263, 35)
        Me.txtaddress.TabIndex = 2
        '
        'txtphone
        '
        Me.txtphone.AutoSize = True
        Me.txtphone.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtphone.Location = New System.Drawing.Point(430, 179)
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(79, 27)
        Me.txtphone.TabIndex = 53
        Me.txtphone.Text = "Phone "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label4.Location = New System.Drawing.Point(11, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 27)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "FirstName "
        '
        'btnedit
        '
        Me.btnedit.BackgroundImage = CType(resources.GetObject("btnedit.BackgroundImage"), System.Drawing.Image)
        Me.btnedit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnedit.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnedit.Location = New System.Drawing.Point(409, 640)
        Me.btnedit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(128, 74)
        Me.btnedit.TabIndex = 11
        Me.btnedit.Text = "Edit"
        Me.btnedit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btnfind
        '
        Me.btnfind.BackgroundImage = CType(resources.GetObject("btnfind.BackgroundImage"), System.Drawing.Image)
        Me.btnfind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnfind.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnfind.Location = New System.Drawing.Point(277, 640)
        Me.btnfind.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(128, 74)
        Me.btnfind.TabIndex = 12
        Me.btnfind.Text = "Find"
        Me.btnfind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnfind.UseVisualStyleBackColor = True
        '
        'txtpatientfname
        '
        Me.txtpatientfname.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtpatientfname.Location = New System.Drawing.Point(152, 131)
        Me.txtpatientfname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtpatientfname.Name = "txtpatientfname"
        Me.txtpatientfname.Size = New System.Drawing.Size(263, 35)
        Me.txtpatientfname.TabIndex = 1
        '
        'txtPatientID
        '
        Me.txtPatientID.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtPatientID.Location = New System.Drawing.Point(152, 87)
        Me.txtPatientID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPatientID.Name = "txtPatientID"
        Me.txtPatientID.Size = New System.Drawing.Size(263, 35)
        Me.txtPatientID.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label2.Location = New System.Drawing.Point(13, 182)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 27)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Address"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label1.Location = New System.Drawing.Point(11, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 27)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "PatientID"
        '
        'btndelete
        '
        Me.btndelete.BackgroundImage = CType(resources.GetObject("btndelete.BackgroundImage"), System.Drawing.Image)
        Me.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btndelete.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btndelete.Location = New System.Drawing.Point(143, 640)
        Me.btndelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 74)
        Me.btndelete.TabIndex = 13
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnback
        '
        Me.btnback.BackgroundImage = CType(resources.GetObject("btnback.BackgroundImage"), System.Drawing.Image)
        Me.btnback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnback.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.btnback.Location = New System.Drawing.Point(9, 640)
        Me.btnback.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(128, 74)
        Me.btnback.TabIndex = 14
        Me.btnback.Text = "Back"
        Me.btnback.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnback.UseVisualStyleBackColor = True
        '
        'txtpatientemail
        '
        Me.txtpatientemail.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtpatientemail.Location = New System.Drawing.Point(551, 223)
        Me.txtpatientemail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtpatientemail.Name = "txtpatientemail"
        Me.txtpatientemail.Size = New System.Drawing.Size(263, 35)
        Me.txtpatientemail.TabIndex = 8
        '
        'txtPatientCode
        '
        Me.txtPatientCode.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtPatientCode.Location = New System.Drawing.Point(152, 264)
        Me.txtPatientCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPatientCode.Name = "txtPatientCode"
        Me.txtPatientCode.Size = New System.Drawing.Size(263, 35)
        Me.txtPatientCode.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label9.Location = New System.Drawing.Point(13, 268)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(137, 27)
        Me.Label9.TabIndex = 71
        Me.Label9.Text = "Patient Code"
        '
        'txtNationalID
        '
        Me.txtNationalID.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtNationalID.Location = New System.Drawing.Point(552, 264)
        Me.txtNationalID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNationalID.Name = "txtNationalID"
        Me.txtNationalID.Size = New System.Drawing.Size(263, 35)
        Me.txtNationalID.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label10.Location = New System.Drawing.Point(429, 268)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 27)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "National ID"
        '
        'txtPassportNo
        '
        Me.txtPassportNo.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtPassportNo.Location = New System.Drawing.Point(152, 309)
        Me.txtPassportNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPassportNo.Name = "txtPassportNo"
        Me.txtPassportNo.Size = New System.Drawing.Size(263, 35)
        Me.txtPassportNo.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label11.Location = New System.Drawing.Point(11, 312)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(129, 27)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "Passport No"
        '
        'txtPhone2
        '
        Me.txtPhone2.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtPhone2.Location = New System.Drawing.Point(552, 309)
        Me.txtPhone2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.Size = New System.Drawing.Size(263, 35)
        Me.txtPhone2.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label12.Location = New System.Drawing.Point(430, 312)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 27)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Phone 2"
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtCity.Location = New System.Drawing.Point(1029, 93)
        Me.txtCity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(263, 35)
        Me.txtCity.TabIndex = 13
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label13.Location = New System.Drawing.Point(823, 97)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 27)
        Me.Label13.TabIndex = 75
        Me.Label13.Text = "City"
        '
        'txtCountryCode
        '
        Me.txtCountryCode.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtCountryCode.Location = New System.Drawing.Point(1029, 232)
        Me.txtCountryCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCountryCode.Name = "txtCountryCode"
        Me.txtCountryCode.Size = New System.Drawing.Size(263, 35)
        Me.txtCountryCode.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label14.Location = New System.Drawing.Point(836, 235)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(148, 27)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "Country Code"
        '
        'txtBloodType
        '
        Me.txtBloodType.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtBloodType.Location = New System.Drawing.Point(1029, 137)
        Me.txtBloodType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtBloodType.Name = "txtBloodType"
        Me.txtBloodType.Size = New System.Drawing.Size(263, 35)
        Me.txtBloodType.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label15.Location = New System.Drawing.Point(823, 141)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 27)
        Me.Label15.TabIndex = 77
        Me.Label15.Text = "BloodType"
        '
        'txtEmergencyContact
        '
        Me.txtEmergencyContact.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtEmergencyContact.Location = New System.Drawing.Point(1029, 182)
        Me.txtEmergencyContact.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEmergencyContact.Name = "txtEmergencyContact"
        Me.txtEmergencyContact.Size = New System.Drawing.Size(263, 35)
        Me.txtEmergencyContact.TabIndex = 17
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label16.Location = New System.Drawing.Point(823, 185)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(203, 27)
        Me.Label16.TabIndex = 79
        Me.Label16.Text = "Emergency Contact"
        '
        'txtEmergencyPhone
        '
        Me.txtEmergencyPhone.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtEmergencyPhone.Location = New System.Drawing.Point(1029, 276)
        Me.txtEmergencyPhone.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEmergencyPhone.Name = "txtEmergencyPhone"
        Me.txtEmergencyPhone.Size = New System.Drawing.Size(263, 35)
        Me.txtEmergencyPhone.TabIndex = 16
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label17.Location = New System.Drawing.Point(836, 279)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(188, 27)
        Me.Label17.TabIndex = 78
        Me.Label17.Text = "Emergency Phone"
        '
        'txtNotes
        '
        Me.txtNotes.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.txtNotes.Location = New System.Drawing.Point(145, 363)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(677, 98)
        Me.txtNotes.TabIndex = 18
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label18.Location = New System.Drawing.Point(13, 298)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(68, 27)
        Me.Label18.TabIndex = 80
        Me.Label18.Text = "Notes"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label3.Location = New System.Drawing.Point(430, 224)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 27)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Email"
        '
        'dateofbirthpatient
        '
        Me.dateofbirthpatient.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateofbirthpatient.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateofbirthpatient.Location = New System.Drawing.Point(156, 220)
        Me.dateofbirthpatient.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dateofbirthpatient.Name = "dateofbirthpatient"
        Me.dateofbirthpatient.Size = New System.Drawing.Size(261, 35)
        Me.dateofbirthpatient.TabIndex = 4
        '
        'txtgender
        '
        Me.txtgender.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgender.FormattingEnabled = True
        Me.txtgender.Items.AddRange(New Object() {"male", "female"})
        Me.txtgender.Location = New System.Drawing.Point(550, 93)
        Me.txtgender.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtgender.Name = "txtgender"
        Me.txtgender.Size = New System.Drawing.Size(263, 35)
        Me.txtgender.TabIndex = 5
        '
        'txtdoctorid
        '
        Me.txtdoctorid.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdoctorid.FormattingEnabled = True
        Me.txtdoctorid.Location = New System.Drawing.Point(1029, 321)
        Me.txtdoctorid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtdoctorid.Name = "txtdoctorid"
        Me.txtdoctorid.Size = New System.Drawing.Size(263, 35)
        Me.txtdoctorid.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label7.Location = New System.Drawing.Point(837, 323)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 27)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "DoctorID"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Times New Roman", 14.25!)
        Me.Label8.Location = New System.Drawing.Point(821, 185)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(107, 27)
        Me.Label8.TabIndex = 68
        Me.Label8.Text = "BranchID"
        '
        'txtbranchid
        '
        Me.txtbranchid.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranchid.FormattingEnabled = True
        Me.txtbranchid.Location = New System.Drawing.Point(1029, 182)
        Me.txtbranchid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtbranchid.Name = "txtbranchid"
        Me.txtbranchid.Size = New System.Drawing.Size(263, 35)
        Me.txtbranchid.TabIndex = 20
        '
        'frmpatients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 752)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtEmergencyContact)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtEmergencyPhone)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtBloodType)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtCountryCode)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtPhone2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtPassportNo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtNationalID)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPatientCode)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtbranchid)
        Me.Controls.Add(Me.txtdoctorid)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtgender)
        Me.Controls.Add(Me.dateofbirthpatient)
        Me.Controls.Add(Me.txtpatientemail)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtpatientphone)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtlastname)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtpatientlname)
        Me.Controls.Add(Me.txtaddress)
        Me.Controls.Add(Me.txtphone)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.txtpatientfname)
        Me.Controls.Add(Me.txtPatientID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnback)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmpatients"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmpatients"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtpatientphone As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtlastname As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnnew As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtpatientlname As TextBox
    Friend WithEvents txtaddress As TextBox
    Friend WithEvents txtphone As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnedit As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents txtpatientfname As TextBox
    Friend WithEvents txtPatientID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btndelete As Button
    Friend WithEvents btnback As Button
    Friend WithEvents txtpatientemail As TextBox
    Friend WithEvents txtPatientCode As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtNationalID As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtPassportNo As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtPhone2 As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtCity As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtCountryCode As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtBloodType As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtEmergencyContact As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtEmergencyPhone As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents dateofbirthpatient As DateTimePicker
    Friend WithEvents txtgender As ComboBox
    Friend WithEvents txtdoctorid As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtbranchid As ComboBox
End Class
