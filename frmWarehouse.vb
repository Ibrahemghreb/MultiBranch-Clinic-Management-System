Public Class frmWarehouse
    Private ReadOnly _bal As New InventoryBAL()

    Private Sub frmWarehouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Warehouse Management")
        UiTheme.StyleInputPanel(panelInputs)
        UiTheme.StyleButtonPanel(panelButtons)
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnedit, btnsave, btnnew)

        For Each ctrl As Control In panelInputs.Controls
            If TypeOf ctrl Is Label Then
                UiTheme.StyleFieldLabel(DirectCast(ctrl, Label))
            ElseIf TypeOf ctrl Is TextBox Then
                UiTheme.StyleTextInput(DirectCast(ctrl, TextBox))
            End If
        Next
        UiTheme.AlignInputLabels(panelInputs)
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = _bal.GetAllItems()
        Catch ex As Exception
            ShowError("Error loading inventory", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            _bal.AddItem(ReadItemFromForm(requireId:=False))
            MessageBox.Show("Item saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving item", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            _bal.UpdateItem(ReadItemFromForm(requireId:=True))
            MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating item", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim id = ReadRequiredInteger(txtItemID.Text, "Item ID")
            If MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            _bal.DeleteItem(id)
            MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting item", ex)
        End Try
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        FindData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Close()
    End Sub

    Private Sub FindData()
        Try
            Dim id = ReadRequiredInteger(txtItemID.Text, "Item ID")
            Dim item = _bal.GetItemById(id)

            If item Is Nothing Then
                MessageBox.Show("Item not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(item)
        Catch ex As Exception
            ShowError("Error finding item", ex)
        End Try
    End Sub

    Private Function ReadItemFromForm(requireId As Boolean) As InventoryItem
        Dim item As New InventoryItem With {
            .ItemCode = txtItemCode.Text,
            .ItemName = txtItemName.Text,
            .Category = If(cmbCategory.SelectedIndex >= 0, cmbCategory.SelectedItem.ToString(), ""),
            .QuantityInStock = ParseDecimal(txtQuantityInStock.Text, 0),
            .UnitOfMeasure = If(cmbUnitOfMeasure.SelectedIndex >= 0, cmbUnitOfMeasure.SelectedItem.ToString(), ""),
            .PurchasePrice = ParseDecimal(txtPurchasePrice.Text, 0),
            .SellingPrice = ParseDecimal(txtSellingPrice.Text, 0),
            .MinStockLevel = ParseDecimal(txtMinStockLevel.Text, 0),
            .SupplierName = txtSupplierName.Text,
            .Notes = txtNotes.Text,
            .IsActive = chkIsActive.Checked
        }

        If requireId Then
            item.InventoryID = ReadRequiredInteger(txtItemID.Text, "Item ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtItemID.Text) Then
            item.InventoryID = ReadRequiredInteger(txtItemID.Text, "Item ID")
        End If

        Return item
    End Function

    Private Sub FillForm(item As InventoryItem)
        txtItemID.Text = item.InventoryID.ToString()
        txtItemCode.Text = item.ItemCode
        txtItemName.Text = item.ItemName

        If item.Category IsNot Nothing AndAlso cmbCategory.Items.Contains(item.Category) Then
            cmbCategory.SelectedItem = item.Category
        ElseIf Not String.IsNullOrWhiteSpace(item.Category) Then
            cmbCategory.Text = item.Category
        End If

        txtQuantityInStock.Text = item.QuantityInStock.ToString("F2")
        txtMinStockLevel.Text = item.MinStockLevel.ToString("F2")

        If item.UnitOfMeasure IsNot Nothing AndAlso cmbUnitOfMeasure.Items.Contains(item.UnitOfMeasure) Then
            cmbUnitOfMeasure.SelectedItem = item.UnitOfMeasure
        ElseIf Not String.IsNullOrWhiteSpace(item.UnitOfMeasure) Then
            cmbUnitOfMeasure.Text = item.UnitOfMeasure
        End If

        txtPurchasePrice.Text = item.PurchasePrice.ToString("F2")
        txtSellingPrice.Text = item.SellingPrice.ToString("F2")
        txtSupplierName.Text = item.SupplierName
        txtNotes.Text = item.Notes
        chkIsActive.Checked = item.IsActive
    End Sub

    Private Sub ClearForm()
        txtItemID.Clear()
        txtItemCode.Clear()
        txtItemName.Clear()
        cmbCategory.SelectedIndex = -1
        txtQuantityInStock.Text = "0.00"
        cmbUnitOfMeasure.SelectedIndex = -1
        txtPurchasePrice.Text = "0.00"
        txtSellingPrice.Text = "0.00"
        txtMinStockLevel.Text = "0.00"
        txtSupplierName.Clear()
        txtNotes.Clear()
        chkIsActive.Checked = True
        txtItemCode.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return

        Try
            Dim idValue = DataGridView1.Rows(e.RowIndex).Cells("InventoryID").Value
            If idValue Is Nothing Then Return

            Dim item = _bal.GetItemById(CInt(idValue))
            If item IsNot Nothing Then FillForm(item)
        Catch ex As Exception
            ShowError("Error loading selected item", ex)
        End Try
    End Sub

    Private Shared Function ReadRequiredInteger(value As String, fieldName As String) As Integer
        Dim result As Integer
        If Not Integer.TryParse(value, result) OrElse result <= 0 Then
            Throw New ArgumentException($"{fieldName} must be a valid number.")
        End If
        Return result
    End Function

    Private Shared Function ParseDecimal(value As String, defaultValue As Decimal) As Decimal
        Dim result As Decimal
        If Decimal.TryParse(value, result) Then Return result
        Return defaultValue
    End Function

    Private Shared Sub ShowError(title As String, ex As Exception)
        MessageBox.Show($"{title}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub txtItemID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQuantityInStock_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantityInStock.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPurchasePrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPurchasePrice.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtSellingPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSellingPrice.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMinStockLevel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMinStockLevel.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtItemID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmWarehouse_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class