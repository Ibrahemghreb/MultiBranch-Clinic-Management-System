Imports ReaLTaiizor

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWarehouse
    Inherits ReaLTaiizor.Forms.MaterialForm

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.panelInputs = New System.Windows.Forms.Panel()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSellingPrice = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPurchasePrice = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbUnitOfMeasure = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtQuantityInStock = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMinStockLevel = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtItemID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.flowButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnback = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnfind = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.panelInputs.SuspendLayout()
        Me.panelButtons.SuspendLayout()
        Me.flowButtons.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        'panelInputs
        Me.panelInputs.Controls.Add(Me.chkIsActive)
        Me.panelInputs.Controls.Add(Me.txtNotes)
        Me.panelInputs.Controls.Add(Me.Label11)
        Me.panelInputs.Controls.Add(Me.txtSupplierName)
        Me.panelInputs.Controls.Add(Me.Label10)
        Me.panelInputs.Controls.Add(Me.txtSellingPrice)
        Me.panelInputs.Controls.Add(Me.Label9)
        Me.panelInputs.Controls.Add(Me.txtPurchasePrice)
        Me.panelInputs.Controls.Add(Me.Label8)
        Me.panelInputs.Controls.Add(Me.cmbUnitOfMeasure)
        Me.panelInputs.Controls.Add(Me.Label7)
        Me.panelInputs.Controls.Add(Me.txtQuantityInStock)
        Me.panelInputs.Controls.Add(Me.Label6)
        Me.panelInputs.Controls.Add(Me.txtMinStockLevel)
        Me.panelInputs.Controls.Add(Me.Label12)
        Me.panelInputs.Controls.Add(Me.cmbCategory)
        Me.panelInputs.Controls.Add(Me.Label5)
        Me.panelInputs.Controls.Add(Me.txtItemName)
        Me.panelInputs.Controls.Add(Me.Label4)
        Me.panelInputs.Controls.Add(Me.txtItemCode)
        Me.panelInputs.Controls.Add(Me.Label3)
        Me.panelInputs.Controls.Add(Me.txtItemID)
        Me.panelInputs.Controls.Add(Me.Label2)
        Me.panelInputs.Dock = DockStyle.Top
        Me.panelInputs.Location = New System.Drawing.Point(3, 78)
        Me.panelInputs.Name = "panelInputs"
        Me.panelInputs.Size = New System.Drawing.Size(1018, 200)
        Me.panelInputs.TabIndex = 0

        'chkIsActive
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = CheckState.Checked
        Me.chkIsActive.Font = New Font("Segoe UI", 10.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(880, 156)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(78, 27)
        Me.chkIsActive.TabIndex = 6
        Me.chkIsActive.Text = "Active"
        Me.chkIsActive.UseVisualStyleBackColor = True

        'txtNotes
        Me.txtNotes.Font = New Font("Segoe UI", 10.0!)
        Me.txtNotes.Location = New System.Drawing.Point(130, 152)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(600, 40)
        Me.txtNotes.TabIndex = 5

        'Label11
        Me.Label11.AutoSize = True
        Me.Label11.Font = New Font("Segoe UI", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(16, 156)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 23)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Notes"

        'txtSupplierName
        Me.txtSupplierName.Font = New Font("Segoe UI", 10.0!)
        Me.txtSupplierName.Location = New System.Drawing.Point(710, 104)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(280, 30)
        Me.txtSupplierName.TabIndex = 4

        'Label10
        Me.Label10.AutoSize = True
        Me.Label10.Font = New Font("Segoe UI", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(632, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 23)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Supplier"

        'cmbCategory
        Me.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New Font("Segoe UI", 10.0!)
        Me.cmbCategory.Items.AddRange(New Object() {"Dental Supplies", "Medications", "Equipment", "Instruments", "Consumables", "Stationery", "Other"})
        Me.cmbCategory.Location = New System.Drawing.Point(756, 24)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(200, 31)
        Me.cmbCategory.TabIndex = 2

        'Label5
        Me.Label5.AutoSize = True
        Me.Label5.Font = New Font("Segoe UI", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(672, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 23)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Category"

        'txtItemName
        Me.txtItemName.Font = New Font("Segoe UI", 10.0!)
        Me.txtItemName.Location = New System.Drawing.Point(430, 24)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(230, 30)
        Me.txtItemName.TabIndex = 1

        'Label4
        Me.Label4.AutoSize = True
        Me.Label4.Font = New Font("Segoe UI", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(336, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Item Name"

        'txtItemCode
        Me.txtItemCode.Font = New Font("Segoe UI", 10.0!)
        Me.txtItemCode.Location = New System.Drawing.Point(130, 24)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(190, 30)
        Me.txtItemCode.TabIndex = 0

        'Label3
        Me.Label3.AutoSize = True
        Me.Label3.Font = New Font("Segoe UI", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(16, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 23)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Item Code"

        'txtItemID
        Me.txtItemID.Font = New Font("Segoe UI", 10.0!)
        Me.txtItemID.Location = New System.Drawing.Point(750, 152)
        Me.txtItemID.Name = "txtItemID"
        Me.txtItemID.Size = New System.Drawing.Size(110, 30)
        Me.txtItemID.TabIndex = 7
        Me.txtItemID.Visible = False

        'Label2
        Me.Label2.AutoSize = True
        Me.Label2.Font = New Font("Segoe UI", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(672, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Item ID"
        Me.Label2.Visible = False

        'txtSellingPrice
        Me.txtSellingPrice.Font = New Font("Segoe UI", 10.0!)
        Me.txtSellingPrice.Location = New System.Drawing.Point(500, 104)
        Me.txtSellingPrice.Name = "txtSellingPrice"
        Me.txtSellingPrice.Size = New System.Drawing.Size(120, 30)
        Me.txtSellingPrice.TabIndex = 3

        'Label9
        Me.Label9.AutoSize = True
        Me.Label9.Font = New Font("Segoe UI", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(392, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 23)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Selling Price"

        'txtPurchasePrice
        Me.txtPurchasePrice.Font = New Font("Segoe UI", 10.0!)
        Me.txtPurchasePrice.Location = New System.Drawing.Point(260, 104)
        Me.txtPurchasePrice.Name = "txtPurchasePrice"
        Me.txtPurchasePrice.Size = New System.Drawing.Size(120, 30)
        Me.txtPurchasePrice.TabIndex = 2

        'Label8
        Me.Label8.AutoSize = True
        Me.Label8.Font = New Font("Segoe UI", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(140, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(111, 23)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Purchase Price"

        'cmbUnitOfMeasure
        Me.cmbUnitOfMeasure.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbUnitOfMeasure.Font = New Font("Segoe UI", 10.0!)
        Me.cmbUnitOfMeasure.Items.AddRange(New Object() {"Piece", "Box", "Carton", "Bottle", "Pack", "Set", "Pair", "Roll", "Bag", "Liter", "Kg", "Meter"})
        Me.cmbUnitOfMeasure.Location = New System.Drawing.Point(130, 104)
        Me.cmbUnitOfMeasure.Name = "cmbUnitOfMeasure"
        Me.cmbUnitOfMeasure.Size = New System.Drawing.Size(120, 31)
        Me.cmbUnitOfMeasure.TabIndex = 1

        'Label7
        Me.Label7.AutoSize = True
        Me.Label7.Font = New Font("Segoe UI", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(16, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 23)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Unit of Meas."

        'txtQuantityInStock
        Me.txtQuantityInStock.Font = New Font("Segoe UI", 10.0!)
        Me.txtQuantityInStock.Location = New System.Drawing.Point(130, 64)
        Me.txtQuantityInStock.Name = "txtQuantityInStock"
        Me.txtQuantityInStock.Size = New System.Drawing.Size(120, 30)
        Me.txtQuantityInStock.TabIndex = 0

        'Label6
        Me.Label6.AutoSize = True
        Me.Label6.Font = New Font("Segoe UI", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(16, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Quantity"

        'txtMinStockLevel
        Me.txtMinStockLevel.Font = New Font("Segoe UI", 10.0!)
        Me.txtMinStockLevel.Location = New System.Drawing.Point(756, 64)
        Me.txtMinStockLevel.Name = "txtMinStockLevel"
        Me.txtMinStockLevel.Size = New System.Drawing.Size(120, 30)
        Me.txtMinStockLevel.TabIndex = 3

        'Label12
        Me.Label12.AutoSize = True
        Me.Label12.Font = New Font("Segoe UI", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(632, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 23)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Min Stock Lvl."

        'panelButtons
        Me.panelButtons.Controls.Add(Me.flowButtons)
        Me.panelButtons.Dock = DockStyle.Bottom
        Me.panelButtons.Location = New System.Drawing.Point(3, 629)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(1018, 88)
        Me.panelButtons.TabIndex = 2

        'flowButtons
        Me.flowButtons.Controls.Add(Me.btnback)
        Me.flowButtons.Controls.Add(Me.btndelete)
        Me.flowButtons.Controls.Add(Me.btnfind)
        Me.flowButtons.Controls.Add(Me.btnedit)
        Me.flowButtons.Controls.Add(Me.btnsave)
        Me.flowButtons.Controls.Add(Me.btnnew)
        Me.flowButtons.Dock = DockStyle.Fill
        Me.flowButtons.Location = New System.Drawing.Point(0, 0)
        Me.flowButtons.Name = "flowButtons"
        Me.flowButtons.Padding = New Padding(4, 0, 0, 0)
        Me.flowButtons.Size = New System.Drawing.Size(1018, 88)
        Me.flowButtons.TabIndex = 0

        'btnback
        Me.btnback.Location = New System.Drawing.Point(8, 8)
        Me.btnback.Margin = New Padding(4, 8, 4, 4)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(128, 74)
        Me.btnback.TabIndex = 0
        Me.btnback.Text = "Back"
        Me.btnback.UseVisualStyleBackColor = True

        'btndelete
        Me.btndelete.Location = New System.Drawing.Point(144, 8)
        Me.btndelete.Margin = New Padding(4, 8, 4, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 74)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = True

        'btnfind
        Me.btnfind.Location = New System.Drawing.Point(280, 8)
        Me.btnfind.Margin = New Padding(4, 8, 4, 4)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(128, 74)
        Me.btnfind.TabIndex = 2
        Me.btnfind.Text = "Find"
        Me.btnfind.UseVisualStyleBackColor = True

        'btnedit
        Me.btnedit.Location = New System.Drawing.Point(416, 8)
        Me.btnedit.Margin = New Padding(4, 8, 4, 4)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(128, 74)
        Me.btnedit.TabIndex = 3
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True

        'btnsave
        Me.btnsave.Location = New System.Drawing.Point(552, 8)
        Me.btnsave.Margin = New Padding(4, 8, 4, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(128, 74)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = True

        'btnnew
        Me.btnnew.Location = New System.Drawing.Point(688, 8)
        Me.btnnew.Margin = New Padding(4, 8, 4, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(128, 74)
        Me.btnnew.TabIndex = 5
        Me.btnnew.Text = "New"
        Me.btnnew.UseVisualStyleBackColor = True

        'DataGridView1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 278)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(1018, 351)
        Me.DataGridView1.TabIndex = 1

        'frmWarehouse
        Me.AutoScaleDimensions = New SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1024, 720)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelInputs)
        Me.MaximizeBox = False
        Me.MinimumSize = New Size(900, 640)
        Me.Name = "frmWarehouse"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Warehouse Management"
        Me.panelInputs.ResumeLayout(False)
        Me.panelInputs.PerformLayout()
        Me.panelButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        CType(Me.DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelInputs As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtItemID As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtItemCode As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtQuantityInStock As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbUnitOfMeasure As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtPurchasePrice As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtSellingPrice As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtSupplierName As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtMinStockLevel As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents panelButtons As Panel
    Friend WithEvents flowButtons As FlowLayoutPanel
    Friend WithEvents btnback As Button
    Friend WithEvents btndelete As Button
    Friend WithEvents btnfind As Button
    Friend WithEvents btnedit As Button
    Friend WithEvents btnsave As Button
    Friend WithEvents btnnew As Button
End Class