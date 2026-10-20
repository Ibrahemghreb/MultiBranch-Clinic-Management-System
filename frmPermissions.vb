Public Class frmPermissions
    Private ReadOnly permissionsBal As New PermissionsBAL()

    Private Sub frmPermissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Permission Management")
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
            DataGridView1.DataSource = permissionsBal.GetAllPermissions()
        Catch ex As Exception
            ShowError("Error loading permissions", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            permissionsBal.AddPermission(ReadPermissionFromForm(requireId:=False))
            MessageBox.Show("Permission saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving permission", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            permissionsBal.UpdatePermission(ReadPermissionFromForm(requireId:=True))
            MessageBox.Show("Permission updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating permission", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim id = ReadRequiredInteger(txtPermissionID.Text, "Permission ID")
            If MessageBox.Show("Are you sure you want to delete this permission?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            permissionsBal.DeletePermission(id)
            MessageBox.Show("Permission deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting permission", ex)
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
            Dim id = ReadRequiredInteger(txtPermissionID.Text, "Permission ID")
            Dim p = permissionsBal.GetPermissionById(id)
            If p Is Nothing Then
                MessageBox.Show("Permission not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(p)
        Catch ex As Exception
            ShowError("Error finding permission", ex)
        End Try
    End Sub

    Private Function ReadPermissionFromForm(requireId As Boolean) As Permission
        Dim p As New Permission With {
            .PermissionCode = txtPermissionCode.Text,
            .PermissionName = txtPermissionName.Text,
            .ModuleName = txtModuleName.Text,
            .Description = txtDescription.Text
        }

        If requireId Then
            p.PermissionID = ReadRequiredInteger(txtPermissionID.Text, "Permission ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtPermissionID.Text) Then
            p.PermissionID = ReadRequiredInteger(txtPermissionID.Text, "Permission ID")
        End If

        Return p
    End Function

    Private Sub FillForm(p As Permission)
        txtPermissionID.Text = p.PermissionID.ToString()
        txtPermissionCode.Text = p.PermissionCode
        txtPermissionName.Text = p.PermissionName
        txtModuleName.Text = p.ModuleName
        txtDescription.Text = p.Description
    End Sub

    Private Sub ClearForm()
        txtPermissionID.Clear()
        txtPermissionCode.Clear()
        txtPermissionName.Clear()
        txtModuleName.Clear()
        txtDescription.Clear()
        txtPermissionCode.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return

        Try
            Dim idValue = DataGridView1.Rows(e.RowIndex).Cells("PermissionID").Value
            If idValue Is Nothing Then Return

            Dim p = permissionsBal.GetPermissionById(CInt(idValue))
            If p IsNot Nothing Then FillForm(p)
        Catch ex As Exception
            ShowError("Error loading selected permission", ex)
        End Try
    End Sub

    Private Shared Function ReadRequiredInteger(value As String, fieldName As String) As Integer
        Dim result As Integer
        If Not Integer.TryParse(value, result) OrElse result <= 0 Then
            Throw New ArgumentException($"{fieldName} must be a valid number.")
        End If

        Return result
    End Function

    Private Shared Sub ShowError(title As String, ex As Exception)
        MessageBox.Show($"{title}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub txtPermissionID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPermissionID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPermissionID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPermissionID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmPermissions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub panelInputs_Paint(sender As Object, e As PaintEventArgs) Handles panelInputs.Paint

    End Sub
End Class
