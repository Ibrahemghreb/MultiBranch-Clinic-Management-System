Public Class frmRoles
    Private ReadOnly rolesBal As New RolesBAL()

    Private Sub frmRoles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Role Management")
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
            DataGridView1.DataSource = rolesBal.GetAllRoles()
        Catch ex As Exception
            ShowError("Error loading roles", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            rolesBal.AddRole(ReadRoleFromForm(requireRoleId:=False))
            MessageBox.Show("Role saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving role", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            rolesBal.UpdateRole(ReadRoleFromForm(requireRoleId:=True))
            MessageBox.Show("Role updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating role", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim roleId = ReadRequiredInteger(txtRoleID.Text, "Role ID")
            If MessageBox.Show("Are you sure you want to delete this role?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            rolesBal.DeleteRole(roleId)
            MessageBox.Show("Role deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting role", ex)
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
            Dim roleId = ReadRequiredInteger(txtRoleID.Text, "Role ID")
            Dim role = rolesBal.GetRoleById(roleId)

            If role Is Nothing Then
                MessageBox.Show("Role not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(role)
        Catch ex As Exception
            ShowError("Error finding role", ex)
        End Try
    End Sub

    Private Function ReadRoleFromForm(requireRoleId As Boolean) As Role
        Dim role As New Role With {
            .RoleCode = txtRoleCode.Text,
            .RoleName = txtRoleName.Text,
            .Description = txtDescription.Text,
            .IsSystem = chkIsSystem.Checked,
            .IsActive = chkIsActive.Checked
        }

        If requireRoleId Then
            role.RoleID = ReadRequiredInteger(txtRoleID.Text, "Role ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtRoleID.Text) Then
            role.RoleID = ReadRequiredInteger(txtRoleID.Text, "Role ID")
        End If

        Return role
    End Function

    Private Sub FillForm(role As Role)
        txtRoleID.Text = role.RoleID.ToString()
        txtRoleCode.Text = role.RoleCode
        txtRoleName.Text = role.RoleName
        txtDescription.Text = role.Description
        chkIsSystem.Checked = role.IsSystem
        chkIsActive.Checked = role.IsActive
    End Sub

    Private Sub ClearForm()
        txtRoleID.Clear()
        txtRoleCode.Clear()
        txtRoleName.Clear()
        txtDescription.Clear()
        chkIsSystem.Checked = False
        chkIsActive.Checked = True
        txtRoleCode.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        Try
            Dim roleIdValue = DataGridView1.Rows(e.RowIndex).Cells("RoleID").Value
            If roleIdValue Is Nothing Then
                Return
            End If

            Dim role = rolesBal.GetRoleById(CInt(roleIdValue))
            If role IsNot Nothing Then
                FillForm(role)
            End If
        Catch ex As Exception
            ShowError("Error loading selected role", ex)
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

    Private Sub txtRoleID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRoleID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRoleID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRoleID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmRoles_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub panelInputs_Paint(sender As Object, e As PaintEventArgs) Handles panelInputs.Paint

    End Sub
End Class
