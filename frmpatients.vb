Imports System.IO

Partial Public Class frmpatients
    Private ReadOnly patientBal As New PatientBAL()
    Private ReadOnly patientErrorProvider As New ErrorProvider()

    Private Sub frmpatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        ConfigurePatientErrorProvider()
        AttachPatientValidationHandlers()
        LoadDoctors()
        LoadBranches()
        LoadPatients()
    End Sub

    Private Sub LoadPatients()
        Try
            DataGridView1.DataSource = patientBal.GetAllPatients()
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل المرضى", ex)
        End Try
    End Sub

    Private Sub LoadDoctors()
        Try
            txtdoctorid.Items.Clear()
            txtdoctorid.Items.AddRange(patientBal.GetDoctors().ToArray())
            txtdoctorid.SelectedIndex = If(txtdoctorid.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل الأطباء", ex)
        End Try
    End Sub

    Private Sub LoadBranches()
        Try
            txtbranchid.Items.Clear()
            txtbranchid.Items.AddRange(patientBal.GetBranches().ToArray())
            txtbranchid.SelectedIndex = If(txtbranchid.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل الفروع", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If Not ValidatePatientForm() Then
                Return
            End If

            patientBal.AddPatient(ReadPatientFromForm(requirePatientId:=False))
            MessageBox.Show("تم حفظ بيانات المريض بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadPatients()
        Catch ex As Exception
            ShowError("خطأ أثناء حفظ المريض", ex)
        End Try
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        FindPatient()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            If Not ValidatePatientForm() Then
                Return
            End If

            patientBal.UpdatePatient(ReadPatientFromForm(requirePatientId:=True))
            MessageBox.Show("تم تحديث بيانات المريض.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadPatients()
        Catch ex As Exception
            ShowError("خطأ أثناء تعديل المريض", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim patientId As Integer = ReadRequiredInteger(txtPatientID.Text, "رقم المريض")

            If MessageBox.Show("هل أنت متأكد من حذف هذا المريض؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            patientBal.DeletePatient(patientId)
            MessageBox.Show("تم حذف المريض.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadPatients()
        Catch ex As Exception
            ShowError("خطأ أثناء حذف المريض", ex)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Hide()
    End Sub

    Private Sub FindPatient()
        Try
            Dim patientId As Integer = ReadRequiredInteger(txtPatientID.Text, "رقم المريض")
            Dim patient As Patient = patientBal.GetPatientById(patientId)

            If patient Is Nothing Then
                MessageBox.Show("لم يتم العثور على المريض.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(patient)
        Catch ex As Exception
            ShowError("خطأ أثناء البحث عن المريض", ex)
        End Try
    End Sub

    Private Function ReadPatientFromForm(requirePatientId As Boolean) As Patient
        Dim patient As New Patient With {
            .PatientCode = txtPatientCode.Text,
            .FirstName = txtpatientfname.Text,
            .LastName = txtpatientlname.Text,
            .DateOfBirth = dateofbirthpatient.Value,
            .Gender = If(txtgender.SelectedItem Is Nothing, txtgender.Text, txtgender.SelectedItem.ToString()),
            .NationalID = txtNationalID.Text,
            .PassportNo = txtPassportNo.Text,
            .Phone = txtpatientphone.Text,
            .Phone2 = txtPhone2.Text,
            .Email = txtpatientemail.Text,
            .Address = txtaddress.Text,
            .City = txtCity.Text,
            .CountryCode = txtCountryCode.Text,
            .BloodType = txtBloodType.Text,
            .EmergencyContact = txtEmergencyContact.Text,
            .EmergencyPhone = txtEmergencyPhone.Text,
            .Notes = txtNotes.Text,
            .DoctorID = ReadSelectedId(txtdoctorid, "الطبيب"),
            .BranchID = ReadSelectedId(txtbranchid, "الفرع")
        }

        If requirePatientId Then
            patient.PatientID = ReadRequiredInteger(txtPatientID.Text, "رقم المريض")
        ElseIf Not String.IsNullOrWhiteSpace(txtPatientID.Text) Then
            patient.PatientID = ReadRequiredInteger(txtPatientID.Text, "رقم المريض")
        End If

        Return patient
    End Function

    Private Sub FillForm(patient As Patient)
        txtPatientID.Text = patient.PatientID.ToString()
        txtPatientCode.Text = patient.PatientCode
        txtpatientfname.Text = patient.FirstName
        txtpatientlname.Text = patient.LastName
        dateofbirthpatient.Value = patient.DateOfBirth
        txtgender.Text = patient.Gender
        txtNationalID.Text = patient.NationalID
        txtPassportNo.Text = patient.PassportNo
        txtaddress.Text = patient.Address
        txtpatientphone.Text = patient.Phone
        txtPhone2.Text = patient.Phone2
        txtpatientemail.Text = patient.Email
        txtCity.Text = patient.City
        txtCountryCode.Text = patient.CountryCode
        txtBloodType.Text = patient.BloodType
        txtEmergencyContact.Text = patient.EmergencyContact
        txtEmergencyPhone.Text = patient.EmergencyPhone
        txtNotes.Text = patient.Notes
        SelectComboItem(txtdoctorid, patient.DoctorID)
        SelectComboItem(txtbranchid, patient.BranchID)
    End Sub

    Private Sub ClearForm()
        txtPatientID.Clear()
        txtPatientCode.Clear()
        txtpatientfname.Clear()
        txtpatientlname.Clear()
        txtgender.SelectedIndex = -1
        txtNationalID.Clear()
        txtPassportNo.Clear()
        txtaddress.Clear()
        txtpatientphone.Clear()
        txtPhone2.Clear()
        txtpatientemail.Clear()
        txtCity.Clear()
        txtCountryCode.Clear()
        txtBloodType.Clear()
        txtEmergencyContact.Clear()
        txtEmergencyPhone.Clear()
        txtNotes.Clear()
        dateofbirthpatient.Value = DateTime.Now
        txtdoctorid.SelectedIndex = If(txtdoctorid.Items.Count > 0, 0, -1)
        txtbranchid.SelectedIndex = If(txtbranchid.Items.Count > 0, 0, -1)
        ClearPatientErrors()
        txtPatientID.Focus()
    End Sub

    Private Sub ConfigurePatientErrorProvider()
        patientErrorProvider.ContainerControl = Me
        patientErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
    End Sub

    Private Sub AttachPatientValidationHandlers()
        AddHandler txtpatientfname.Validating, AddressOf ValidateRequiredTextBox
        AddHandler txtpatientlname.Validating, AddressOf ValidateRequiredTextBox
        AddHandler txtgender.Validating, AddressOf ValidateRequiredComboBox
        AddHandler txtpatientphone.Validating, AddressOf ValidatePhoneNumberTextBox
        AddHandler txtPhone2.Validating, AddressOf ValidateOptionalPhoneNumberTextBox
        AddHandler txtEmergencyPhone.Validating, AddressOf ValidateOptionalPhoneNumberTextBox
        AddHandler txtpatientemail.Validating, AddressOf ValidateEmailTextBox
        AddHandler txtPatientCode.Validating, AddressOf ValidateRequiredTextBox
    End Sub

    Private Function ValidatePatientForm() As Boolean
        Dim isValid As Boolean = True
        isValid = SetErrorIfEmpty(txtPatientCode, "الرجاء إدخال رقم المريض.") And isValid
        isValid = SetErrorIfEmpty(txtpatientfname, "الرجاء إدخال الاسم الأول.") And isValid
        isValid = SetErrorIfEmpty(txtpatientlname, "الرجاء إدخال اسم العائلة.") And isValid
        isValid = SetErrorIfComboNotSelected(txtgender, "الرجاء اختيار النوع.") And isValid
        isValid = SetErrorIfComboNotSelected(txtdoctorid, "الرجاء اختيار الطبيب.") And isValid
        isValid = SetErrorIfComboNotSelected(txtbranchid, "الرجاء اختيار الفرع.") And isValid
        isValid = ValidatePhoneNumberTextBox(txtpatientphone, False) And isValid
        isValid = ValidateOptionalPhoneNumberTextBox(txtPhone2, True) And isValid
        isValid = ValidateOptionalPhoneNumberTextBox(txtEmergencyPhone, True) And isValid
        isValid = ValidateEmailTextBox(txtpatientemail, True) And isValid
        Return isValid
    End Function

    Private Sub ClearPatientErrors()
        patientErrorProvider.SetError(txtPatientCode, String.Empty)
        patientErrorProvider.SetError(txtpatientfname, String.Empty)
        patientErrorProvider.SetError(txtpatientlname, String.Empty)
        patientErrorProvider.SetError(txtgender, String.Empty)
        patientErrorProvider.SetError(txtdoctorid, String.Empty)
        patientErrorProvider.SetError(txtbranchid, String.Empty)
        patientErrorProvider.SetError(txtpatientphone, String.Empty)
        patientErrorProvider.SetError(txtPhone2, String.Empty)
        patientErrorProvider.SetError(txtEmergencyPhone, String.Empty)
        patientErrorProvider.SetError(txtpatientemail, String.Empty)
    End Sub

    Private Function SetErrorIfEmpty(control As Control, errorMessage As String) As Boolean
        If String.IsNullOrWhiteSpace(control.Text) Then
            patientErrorProvider.SetError(control, errorMessage)
            Return False
        End If

        patientErrorProvider.SetError(control, String.Empty)
        Return True
    End Function

    Private Function SetErrorIfComboNotSelected(comboBox As ComboBox, errorMessage As String) As Boolean
        If comboBox.SelectedItem Is Nothing Then
            patientErrorProvider.SetError(comboBox, errorMessage)
            Return False
        End If

        patientErrorProvider.SetError(comboBox, String.Empty)
        Return True
    End Function

    Private Function ValidatePhoneNumberTextBox(control As Object, allowEmpty As Boolean) As Boolean
        Dim text As String = DirectCast(control, Control).Text.Trim()
        If String.IsNullOrEmpty(text) Then
            If allowEmpty Then
                patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
                Return True
            End If

            patientErrorProvider.SetError(DirectCast(control, Control), "الرجاء إدخال رقم هاتف.")
            Return False
        End If

        If Not text.All(AddressOf Char.IsDigit) Then
            patientErrorProvider.SetError(DirectCast(control, Control), "رقم الهاتف يجب أن يحتوي على أرقام فقط.")
            Return False
        End If

        patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
        Return True
    End Function

    Private Function ValidateOptionalPhoneNumberTextBox(control As Object, allowEmpty As Boolean) As Boolean
        Dim text As String = DirectCast(control, Control).Text.Trim()
        If String.IsNullOrEmpty(text) Then
            patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
            Return True
        End If

        If Not text.All(AddressOf Char.IsDigit) Then
            patientErrorProvider.SetError(DirectCast(control, Control), "رقم الهاتف يجب أن يحتوي على أرقام فقط.")
            Return False
        End If

        patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
        Return True
    End Function

    Private Function ValidateEmailTextBox(control As Object, allowEmpty As Boolean) As Boolean
        Dim email As String = DirectCast(control, Control).Text.Trim()
        If String.IsNullOrEmpty(email) Then
            patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
            Return allowEmpty
        End If

        Dim isValidEmail As Boolean = email.Contains("@") AndAlso email.Contains(".")
        If Not isValidEmail Then
            patientErrorProvider.SetError(DirectCast(control, Control), "صيغة البريد الإلكتروني غير صحيحة.")
            Return False
        End If

        patientErrorProvider.SetError(DirectCast(control, Control), String.Empty)
        Return True
    End Function

    Private Sub ValidateRequiredTextBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        SetErrorIfEmpty(textBox, "هذا الحقل مطلوب.")
    End Sub

    Private Sub ValidateRequiredComboBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        SetErrorIfComboNotSelected(combo, "الرجاء اختيار قيمة.")
    End Sub

    Private Sub ValidatePhoneNumberTextBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidatePhoneNumberTextBox(DirectCast(sender, TextBox), False)
    End Sub

    Private Sub ValidateOptionalPhoneNumberTextBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateOptionalPhoneNumberTextBox(DirectCast(sender, TextBox), True)
    End Sub

    Private Sub ValidateEmailTextBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateEmailTextBox(DirectCast(sender, TextBox), True)
    End Sub

    ' تم التعديل لمنع الـ Casting الخاطئ وقراءة أي كائن يحتوي خاصية ID برمجياً بمرونة
    Private Shared Function ReadSelectedId(comboBox As ComboBox, fieldName As String) As Integer
        If comboBox.SelectedItem Is Nothing Then
            Return 0
        End If

        Dim item As Object = comboBox.SelectedItem
        Try
            Return Convert.ToInt32(item.ID)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' جعل الاختيار ديناميكي متوافق مع كافة كلاسات الـ ListItems (سواء أطباء أو فروع)
    Private Shared Sub SelectComboItem(comboBox As ComboBox, id As Integer)
        For Each item As Object In comboBox.Items
            Try
                If Convert.ToInt32(item.ID) = id Then
                    comboBox.SelectedItem = item
                    Exit Sub
                End If
            Catch
            End Try
        Next

        comboBox.SelectedIndex = -1
    End Sub

    Private Shared Function ReadRequiredInteger(value As String, fieldName As String) As Integer
        Dim result As Integer
        If Not Integer.TryParse(value, result) OrElse result <= 0 Then
            Throw New ArgumentException($"{fieldName} يجب أن يكون رقمًا صحيحًا.")
        End If

        Return result
    End Function

    Private Shared Sub ShowError(title As String, ex As Exception)
        MessageBox.Show($"{title}: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub txtPatientID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPatientID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtpatientphone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpatientphone.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmpatients_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub

    Public Sub ApplySettings(newFont As Font, newBackColor As Color, backgroundImagePath As String)
        Try
            If newFont IsNot Nothing Then
                Me.Font = newFont
                ApplyFontToControls(Me, newFont)
            End If

            If newBackColor <> Color.Empty Then
                Me.BackColor = newBackColor
            End If

            Me.BackgroundImage = Nothing
            If Not String.IsNullOrEmpty(backgroundImagePath) AndAlso File.Exists(backgroundImagePath) Then
                Try
                    Dim bgImage As Image = Image.FromFile(backgroundImagePath)
                    Me.BackgroundImage = bgImage
                    Me.BackgroundImageLayout = ImageLayout.Stretch
                Catch imgEx As Exception
                    MessageBox.Show($"خطأ في تحميل صورة الخلفية: {imgEx.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تطبيق الإعدادات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyUiTheme()
        UiTheme.ApplyMaterialForm(Me, "Patient Management")
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnedit, btnsave, btnnew)
        UiTheme.StyleTextInput(txtpatientphone)
        UiTheme.StyleTextInput(txtpatientlname)
        UiTheme.StyleTextInput(txtpatientfname)
        UiTheme.StyleTextInput(txtPatientCode)
        UiTheme.StyleTextInput(txtNationalID)
        UiTheme.StyleTextInput(txtPassportNo)
        UiTheme.StyleTextInput(txtaddress)
        UiTheme.StyleTextInput(txtPhone2)
        UiTheme.StyleTextInput(txtpatientemail)
        UiTheme.StyleTextInput(txtCity)
        UiTheme.StyleTextInput(txtCountryCode)
        UiTheme.StyleTextInput(txtBloodType)
        UiTheme.StyleTextInput(txtEmergencyContact)
        UiTheme.StyleTextInput(txtEmergencyPhone)
        UiTheme.StyleTextInput(txtNotes)
    End Sub

    ' إضافة دالة تعديل الخطوط المفقودة لضمان إتمام عملية الـ Build
    Private Sub ApplyFontToControls(container As Control, targetFont As Font)
        For Each ctrl As Control In container.Controls
            ctrl.Font = targetFont
            If ctrl.HasChildren Then
                ApplyFontToControls(ctrl, targetFont)
            End If
        Next
    End Sub
End Class