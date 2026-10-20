Public Class frmDoctors
    Private ReadOnly doctorBal As New DoctorBAL()

    Private Sub frmdoctors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        LoadLookups()
        LoadData()
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Doctor Management")
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

    Private Sub LoadLookups()
        cmbBranch.DataSource = doctorBal.GetBranches()
        cmbBranch.DisplayMember = "DisplayText"
        cmbBranch.ValueMember = "BranchID"
        cmbBranch.SelectedIndex = If(cmbBranch.Items.Count > 0, 0, -1)

        Dim specialties = doctorBal.GetSpecialties()
        specialties.Insert(0, New SpecialtyListItem With {.SpecialtyID = 0, .SpecialtyName = "(None)"})
        cmbSpecialty.DataSource = specialties
        cmbSpecialty.DisplayMember = "DisplayText"
        cmbSpecialty.ValueMember = "SpecialtyID"
        cmbSpecialty.SelectedIndex = 0
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = doctorBal.GetAllDoctors()
        Catch ex As Exception
            ShowError("Error loading doctors", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            doctorBal.AddDoctor(ReadDoctorFromForm(requireDoctorId:=False))
            MessageBox.Show("Doctor saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error saving doctor", ex)
        End Try
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            doctorBal.UpdateDoctor(ReadDoctorFromForm(requireDoctorId:=True))
            MessageBox.Show("Doctor updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error updating doctor", ex)
        End Try
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        FindData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim doctorId = ReadRequiredInteger(txtDoctorid.Text, "Doctor ID")
            If MessageBox.Show("Are you sure you want to delete this doctor?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            doctorBal.DeleteDoctor(doctorId)
            MessageBox.Show("Doctor deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadData()
            ClearForm()
        Catch ex As Exception
            ShowError("Error deleting doctor", ex)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Close()
    End Sub

    Private Sub FindData()
        Try
            Dim doctorId = ReadRequiredInteger(txtDoctorid.Text, "Doctor ID")
            Dim doctor = doctorBal.GetDoctorById(doctorId)

            If doctor Is Nothing Then
                MessageBox.Show("Doctor not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(doctor)
        Catch ex As Exception
            ShowError("Error finding doctor", ex)
        End Try
    End Sub

    Private Function ReadDoctorFromForm(requireDoctorId As Boolean) As Doctor
        Dim doctor As New Doctor With {
            .UserID = ReadRequiredInteger(txtuserid.Text, "User ID"),
            .BranchID = ReadSelectedBranchId(),
            .SpecialtyID = ReadSelectedSpecialtyId(),
            .FirstName = txtfirstname.Text,
            .LastName = txtlastnamedoctor.Text,
            .Phone = txtphonedoctor.Text,
            .Email = txtemail.Text,
            .LicenseNo = txtLicenseNo.Text,
            .IsActive = chkIsActive.Checked
        }

        If requireDoctorId Then
            doctor.DoctorID = ReadRequiredInteger(txtDoctorid.Text, "Doctor ID")
        ElseIf Not String.IsNullOrWhiteSpace(txtDoctorid.Text) Then
            doctor.DoctorID = ReadRequiredInteger(txtDoctorid.Text, "Doctor ID")
        End If

        Return doctor
    End Function

    Private Sub FillForm(doctor As Doctor)
        txtDoctorid.Text = doctor.DoctorID.ToString()
        txtuserid.Text = doctor.UserID.ToString()
        txtfirstname.Text = doctor.FirstName
        txtlastnamedoctor.Text = doctor.LastName
        txtphonedoctor.Text = doctor.Phone
        txtemail.Text = doctor.Email
        txtLicenseNo.Text = doctor.LicenseNo
        chkIsActive.Checked = doctor.IsActive

        If doctor.BranchID > 0 Then
            cmbBranch.SelectedValue = doctor.BranchID
        End If

        If doctor.SpecialtyID > 0 Then
            cmbSpecialty.SelectedValue = doctor.SpecialtyID
        Else
            cmbSpecialty.SelectedIndex = 0
        End If
    End Sub

    Private Sub ClearForm()
        txtDoctorid.Clear()
        txtuserid.Clear()
        txtfirstname.Clear()
        txtlastnamedoctor.Clear()
        txtphonedoctor.Clear()
        txtemail.Clear()
        txtLicenseNo.Clear()
        chkIsActive.Checked = True

        If cmbBranch.Items.Count > 0 Then
            cmbBranch.SelectedIndex = 0
        End If

        cmbSpecialty.SelectedIndex = 0
        txtfirstname.Focus()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        Try
            Dim doctorIdValue = DataGridView1.Rows(e.RowIndex).Cells("DoctorID").Value
            If doctorIdValue Is Nothing Then
                Return
            End If

            Dim doctor = doctorBal.GetDoctorById(CInt(doctorIdValue))
            If doctor IsNot Nothing Then
                FillForm(doctor)
            End If
        Catch ex As Exception
            ShowError("Error loading selected doctor", ex)
        End Try
    End Sub

    Private Function ReadSelectedBranchId() As Integer
        If cmbBranch.SelectedValue Is Nothing Then
            Throw New ArgumentException("Please select a branch.")
        End If

        Return Convert.ToInt32(cmbBranch.SelectedValue)
    End Function

    Private Function ReadSelectedSpecialtyId() As Integer
        If cmbSpecialty.SelectedValue Is Nothing Then
            Return 0
        End If

        Return Convert.ToInt32(cmbSpecialty.SelectedValue)
    End Function

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

    Private Sub txtDoctorid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDoctorid.KeyPress, txtuserid.KeyPress, txtphonedoctor.KeyPress
        If sender Is txtphonedoctor Then
            If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
                e.Handled = True
            End If
            Return
        End If

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDoctorid_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDoctorid.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            FindData()
        End If
    End Sub

    Private Sub frmdoctors_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
