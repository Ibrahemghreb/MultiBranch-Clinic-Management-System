Public Class frmBranches
    Private ReadOnly branchBal As New BranchBAL()

    Private Sub frmBranches_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        InitializeDefaults()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Branch Management")
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

    Private Sub InitializeDefaults()
        dtpOpenTime.Value = Today.AddHours(9)
        dtpCloseTime.Value = Today.AddHours(21)
        chkIsActive.Checked = True
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = branchBal.GetAllBranches()
        Catch ex As Exception
            ShowError("Error loading branches", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            branchBal.AddBranch(ReadBranchFromForm(requireBranchId:=False))
            MessageBox.Show("Branch saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving branch", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            branchBal.UpdateBranch(ReadBranchFromForm(requireBranchId:=True))
            MessageBox.Show("Branch updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating branch", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim branchId As Integer = ReadRequiredInteger(txtBranchID.Text, "Branch ID")

            If MessageBox.Show("Are you sure you want to delete this branch?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            branchBal.DeleteBranch(branchId)
            MessageBox.Show("Branch deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting branch", ex)
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
            Dim branchId As Integer = ReadRequiredInteger(txtBranchID.Text, "Branch ID")
            Dim branch As Branch = branchBal.GetBranchById(branchId)

            If branch Is Nothing Then
                MessageBox.Show("Branch not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(branch)
        Catch ex As Exception
            ShowError("Error finding branch", ex)
        End Try
    End Sub

    Private Function ReadBranchFromForm(requireBranchId As Boolean) As Branch
        Dim branch As New Branch With {
            .BranchName = txtBranchName.Text,
            .Address = txtaddress.Text,
            .Phone = txtphone.Text,
            .Email = txtEmail.Text,
            .OpenTime = dtpOpenTime.Value,
            .CloseTime = dtpCloseTime.Value,
            .IsActive = chkIsActive.Checked
        }

        If requireBranchId Then
            branch.BranchID = ReadRequiredInteger(txtBranchID.Text, "Branch ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtBranchID.Text) Then
            branch.BranchID = ReadRequiredInteger(txtBranchID.Text, "Branch ID")
        End If

        Return branch
    End Function

    Private Sub FillForm(branch As Branch)
        txtBranchID.Text = branch.BranchID.ToString()
        txtBranchName.Text = branch.BranchName
        txtaddress.Text = branch.Address
        txtphone.Text = branch.Phone
        txtEmail.Text = branch.Email
        dtpOpenTime.Value = branch.OpenTime
        dtpCloseTime.Value = branch.CloseTime
        chkIsActive.Checked = branch.IsActive
    End Sub

    Private Sub ClearForm()
        txtBranchID.Clear()
        txtBranchName.Clear()
        txtaddress.Clear()
        txtphone.Clear()
        txtEmail.Clear()
        dtpOpenTime.Value = Today.AddHours(9)
        dtpCloseTime.Value = Today.AddHours(21)
        chkIsActive.Checked = True
        txtBranchName.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        Try
            Dim branchIdValue = DataGridView1.Rows(e.RowIndex).Cells("BranchID").Value
            If branchIdValue Is Nothing Then
                Return
            End If

            Dim branch = branchBal.GetBranchById(CInt(branchIdValue))
            If branch IsNot Nothing Then
                FillForm(branch)
            End If
        Catch ex As Exception
            ShowError("Error loading selected branch", ex)
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

    Private Sub txtphone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtphone.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBranchID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBranchID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBranchID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBranchID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmBranches_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
