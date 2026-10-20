Public Class frmUsers
    Private ReadOnly userService As New UserBAL()

    Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        LoadRoles()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "User Management")
        UiTheme.StyleInputPanel(panelInputs)
        UiTheme.StyleButtonPanel(panelButtons)
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnupdate, btnsave, btnnew)

        For Each ctrl As Control In panelInputs.Controls
            If TypeOf ctrl Is Label Then
                UiTheme.StyleFieldLabel(DirectCast(ctrl, Label))
            ElseIf TypeOf ctrl Is TextBox Then
                UiTheme.StyleTextInput(DirectCast(ctrl, TextBox))
            End If
        Next
        UiTheme.AlignInputLabels(panelInputs)
    End Sub

    Private Sub LoadRoles()
        cmbRole.DataSource = userService.GetActiveRoles()
        cmbRole.DisplayMember = "RoleName"
        cmbRole.ValueMember = "RoleID"
        cmbRole.SelectedIndex = If(cmbRole.Items.Count > 0, 0, -1)
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = userService.GetAllUsers()
        Catch ex As Exception
            ShowError("Error loading users", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            userService.AddUser(ReadUserFromForm(requireUserId:=False, requirePassword:=True))
            MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving user", ex)
        End Try
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            userService.UpdateUser(ReadUserFromForm(requireUserId:=True, requirePassword:=False))
            MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating user", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim userId = ReadRequiredInteger(txtuserid.Text, "User ID")
            If MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            userService.DeleteUser(userId)
            MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting user", ex)
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
            Dim userId = ReadRequiredInteger(txtuserid.Text, "User ID")
            Dim user = userService.GetUserById(userId)

            If user Is Nothing Then
                MessageBox.Show("User not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(user)
        Catch ex As Exception
            ShowError("Error finding user", ex)
        End Try
    End Sub

    Private Function ReadUserFromForm(requireUserId As Boolean, requirePassword As Boolean) As ClinicUser
        If cmbRole.SelectedValue Is Nothing Then
            Throw New ArgumentException("Please select a role.")
        End If

        Dim user As New ClinicUser With {
            .Username = txtusername.Text,
            .Password = txtpassword.Text,
            .FullName = txtFullName.Text,
            .RoleID = CInt(cmbRole.SelectedValue),
            .Role = cmbRole.Text,
            .IsActive = chkIsActive.Checked
        }

        If requireUserId Then
            user.UserID = ReadRequiredInteger(txtuserid.Text, "User ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtuserid.Text) Then
            user.UserID = ReadRequiredInteger(txtuserid.Text, "User ID")
        End If

        If requirePassword AndAlso String.IsNullOrWhiteSpace(user.Password) Then
            Throw New ArgumentException("Please enter a password.")
        End If

        Return user
    End Function

    Private Sub FillForm(user As ClinicUser)
        txtuserid.Text = user.UserID.ToString()
        txtusername.Text = user.Username
        txtpassword.Clear()
        txtFullName.Text = user.FullName
        chkIsActive.Checked = user.IsActive

        If user.RoleID > 0 Then
            cmbRole.SelectedValue = user.RoleID
        Else
            cmbRole.Text = user.Role
        End If
    End Sub

    Private Sub ClearForm()
        txtuserid.Clear()
        txtusername.Clear()
        txtpassword.Clear()
        txtFullName.Clear()
        chkIsActive.Checked = True
        If cmbRole.Items.Count > 0 Then
            cmbRole.SelectedIndex = 0
        End If
        txtusername.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        Try
            Dim userIdValue = DataGridView1.Rows(e.RowIndex).Cells("UserID").Value
            If userIdValue Is Nothing Then
                Return
            End If

            Dim user = userService.GetUserById(CInt(userIdValue))
            If user IsNot Nothing Then
                FillForm(user)
            End If
        Catch ex As Exception
            ShowError("Error loading selected user", ex)
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

    Private Sub txtuserid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtuserid.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtuserid_KeyDown(sender As Object, e As KeyEventArgs) Handles txtuserid.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmUsers_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
