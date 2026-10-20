Imports System.IO

Partial Public Class frmappointments
    Private ReadOnly appointmentBal As New AppointmentBAL()
    Private ReadOnly appointmentErrorProvider As New ErrorProvider()

    Private Sub frmappointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        ConfigureAppointmentErrorProvider()
        AttachAppointmentValidationHandlers()
        LoadStatuses()
        LoadBranches()
        LoadDoctors()
        LoadPatients()
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            DataGridView1.DataSource = appointmentBal.GetAllAppointments()
        Catch ex As Exception
            ShowError("حدث خطأ أثناء تحميل بيانات الحجوزات", ex)
        End Try
    End Sub

    Private Sub LoadStatuses()
        txtstatus.Items.Clear()
        txtstatus.Items.AddRange(appointmentBal.GetAllowedStatuses().ToArray())
        txtstatus.SelectedItem = "Pending"
    End Sub

    Private Sub LoadBranches()
        Try
            txtbranchID.Items.Clear()
            txtbranchID.Items.AddRange(appointmentBal.GetBranches().ToArray())
            txtbranchID.SelectedIndex = If(txtbranchID.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("حدث خطأ أثناء تحميل الفروع", ex)
        End Try
    End Sub

    Private Sub LoadDoctors()
        Try
            Doctorid.Items.Clear()
            Doctorid.Items.AddRange(appointmentBal.GetDoctors().ToArray())
            Doctorid.SelectedIndex = If(Doctorid.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("حدث خطأ أثناء تحميل الأطباء", ex)
        End Try
    End Sub

    Private Sub LoadPatients()
        Try
            txtpatientid.Items.Clear()
            txtpatientid.Items.AddRange(appointmentBal.GetPatients().ToArray())
            txtpatientid.SelectedIndex = If(txtpatientid.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("حدث خطأ أثناء تحميل المرضى", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If Not ValidateAppointmentForm() Then
                Return
            End If

            appointmentBal.AddAppointment(ReadAppointmentFromForm(requireAppointmentId:=False))
            MessageBox.Show("تم حفظ الموعد بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadData()
        Catch ex As Exception
            ShowError("حدث خطأ أثناء حفظ الموعد", ex)
        End Try
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        FindRecord()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            If Not ValidateAppointmentForm() Then
                Return
            End If

            appointmentBal.UpdateAppointment(ReadAppointmentFromForm(requireAppointmentId:=True))
            MessageBox.Show("تم تعديل الموعد بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadData()
        Catch ex As Exception
            ShowError("حدث خطأ أثناء تعديل الموعد", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim appointmentId As Integer = ReadRequiredInteger(txtappointmentID.Text, "رقم الموعد")

            If MessageBox.Show("هل أنت متأكد من حذف هذا الموعد؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            appointmentBal.DeleteAppointment(appointmentId)
            MessageBox.Show("تم حذف الموعد بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
            LoadData()
        Catch ex As Exception
            ShowError("حدث خطأ أثناء حذف الموعد", ex)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Hide()
    End Sub

    Private Sub FindRecord()
        Try
            Dim appointmentId As Integer = ReadRequiredInteger(txtappointmentID.Text, "رقم الموعد")
            Dim appointment As Appointment = appointmentBal.GetAppointmentById(appointmentId)

            If appointment Is Nothing Then
                MessageBox.Show("لم يتم العثور على الموعد.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(appointment)
        Catch ex As Exception
            ShowError("حدث خطأ أثناء البحث عن الموعد", ex)
        End Try
    End Sub

    Private Function ReadAppointmentFromForm(requireAppointmentId As Boolean) As Appointment
        Dim appointment As New Appointment With {
            .BranchID = ReadSelectedId(txtbranchID, "الفرع"),
            .DoctorID = ReadSelectedId(Doctorid, "الطبيب"),
            .PatientID = ReadSelectedId(txtpatientid, "المريض"),
            .AppointmentStart = appointmentdate.Value,
            .AppointmentEnd = appointmentdate.Value.AddMinutes(30),
            .Notes = txtnotes.Text,
            .QueueNumber = ReadRequiredInteger(txtquenuenumber.Text, "رقم الدور"),
            .Status = If(txtstatus.SelectedItem Is Nothing, "Pending", txtstatus.SelectedItem.ToString())
        }

        If requireAppointmentId Then
            appointment.AppointmentID = ReadRequiredInteger(txtappointmentID.Text, "رقم الموعد")
        ElseIf Not String.IsNullOrWhiteSpace(txtappointmentID.Text) Then
            appointment.AppointmentID = ReadRequiredInteger(txtappointmentID.Text, "رقم الموعد")
        End If

        Return appointment
    End Function

    Private Sub FillForm(appointment As Appointment)
        txtappointmentID.Text = appointment.AppointmentID.ToString()
        SelectComboItem(txtbranchID, appointment.BranchID)
        SelectComboItem(Doctorid, appointment.DoctorID)
        SelectComboItem(txtpatientid, appointment.PatientID)
        appointmentdate.Value = appointment.AppointmentStart
        txtnotes.Text = appointment.Notes
        txtquenuenumber.Text = appointment.QueueNumber.ToString()
        txtstatus.SelectedItem = appointment.Status
    End Sub

    Private Sub ClearForm()
        txtappointmentID.Clear()
        txtbranchID.SelectedIndex = If(txtbranchID.Items.Count > 0, 0, -1)
        Doctorid.SelectedIndex = If(Doctorid.Items.Count > 0, 0, -1)
        txtpatientid.SelectedIndex = If(txtpatientid.Items.Count > 0, 0, -1)
        appointmentdate.Value = DateTime.Now
        txtnotes.Clear()
        txtquenuenumber.Clear()
        txtstatus.SelectedItem = "Pending"
        ClearAppointmentErrors()
        txtappointmentID.Focus()
    End Sub

    Private Sub ConfigureAppointmentErrorProvider()
        appointmentErrorProvider.ContainerControl = Me
        appointmentErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink
    End Sub

    Private Sub AttachAppointmentValidationHandlers()
        AddHandler txtbranchID.Validating, AddressOf ValidateRequiredComboBox
        AddHandler Doctorid.Validating, AddressOf ValidateRequiredComboBox
        AddHandler txtpatientid.Validating, AddressOf ValidateRequiredComboBox
        AddHandler txtquenuenumber.Validating, AddressOf ValidateQueueNumberTextBox
        AddHandler txtstatus.Validating, AddressOf ValidateRequiredComboBox
        AddHandler appointmentdate.Validating, AddressOf ValidateAppointmentDatePicker
    End Sub

    Private Function ValidateAppointmentForm() As Boolean
        Dim isValid As Boolean = True
        isValid = SetErrorIfComboNotSelected(txtbranchID, "الرجاء اختيار الفرع.") And isValid
        isValid = SetErrorIfComboNotSelected(Doctorid, "الرجاء اختيار الطبيب.") And isValid
        isValid = SetErrorIfComboNotSelected(txtpatientid, "الرجاء اختيار المريض.") And isValid
        isValid = ValidateQueueNumberTextBox(txtquenuenumber, False) And isValid
        isValid = SetErrorIfComboNotSelected(txtstatus, "الرجاء اختيار حالة الموعد.") And isValid
        isValid = ValidateAppointmentDatePicker(appointmentdate, True) And isValid
        Return isValid
    End Function

    Private Sub ClearAppointmentErrors()
        appointmentErrorProvider.SetError(txtbranchID, String.Empty)
        appointmentErrorProvider.SetError(Doctorid, String.Empty)
        appointmentErrorProvider.SetError(txtpatientid, String.Empty)
        appointmentErrorProvider.SetError(txtquenuenumber, String.Empty)
        appointmentErrorProvider.SetError(txtstatus, String.Empty)
        appointmentErrorProvider.SetError(appointmentdate, String.Empty)
    End Sub

    Private Function SetErrorIfComboNotSelected(comboBox As ComboBox, errorMessage As String) As Boolean
        If comboBox.SelectedItem Is Nothing Then
            appointmentErrorProvider.SetError(comboBox, errorMessage)
            Return False
        End If

        appointmentErrorProvider.SetError(comboBox, String.Empty)
        Return True
    End Function

    Private Function ValidateQueueNumberTextBox(control As TextBox, allowEmpty As Boolean) As Boolean
        Dim text As String = control.Text.Trim()
        If String.IsNullOrEmpty(text) Then
            If allowEmpty Then
                appointmentErrorProvider.SetError(control, String.Empty)
                Return True
            End If

            appointmentErrorProvider.SetError(control, "الرجاء إدخال رقم الدور.")
            Return False
        End If

        Dim result As Integer
        If Not Integer.TryParse(text, result) OrElse result <= 0 Then
            appointmentErrorProvider.SetError(control, "رقم الدور يجب أن يكون رقمًا صحيحًا أكبر من صفر.")
            Return False
        End If

        appointmentErrorProvider.SetError(control, String.Empty)
        Return True
    End Function

    Private Function ValidateAppointmentDatePicker(control As DateTimePicker, allowEmpty As Boolean) As Boolean
        Dim selectedDate As DateTime = control.Value
        If selectedDate < DateTime.Now.AddMinutes(-5) Then
            appointmentErrorProvider.SetError(control, "وقت الموعد يجب أن يكون الآن أو بعده.")
            Return False
        End If

        appointmentErrorProvider.SetError(control, String.Empty)
        Return True
    End Function

    Private Sub ValidateRequiredComboBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim combo As ComboBox = DirectCast(sender, ComboBox)
        SetErrorIfComboNotSelected(combo, "الرجاء اختيار قيمة.")
    End Sub

    Private Sub ValidateQueueNumberTextBox(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateQueueNumberTextBox(DirectCast(sender, TextBox), False)
    End Sub

    Private Sub ValidateAppointmentDatePicker(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateAppointmentDatePicker(DirectCast(sender, DateTimePicker), True)
    End Sub

    ' تم تعديل الدالة لتقبل جلب الـ ID بشكل مرن متوافق مع بنية الكائنات المختلفة
    Private Shared Function ReadSelectedId(comboBox As ComboBox, fieldName As String) As Integer
        If comboBox.SelectedItem Is Nothing Then
            Throw New ArgumentException($"الرجاء اختيار {fieldName}.")
        End If

        ' التحقق بالاعتماد على خصائص الكائن الديناميكية لتفادي كراش الاختلاف بين الكلاسات
        Dim item As Object = comboBox.SelectedItem
        Try
            Return Convert.ToInt32(item.ID)
        Catch ex As Exception
            Throw New ArgumentException($"فشل قراءة الرقم التعريفي لـ {fieldName}.")
        End Try
    End Function

    ' تم تعديل الدالة لتبحث عن المعرف بشكل ديناميكي مرن لكافة القوائم
    Private Shared Sub SelectComboItem(comboBox As ComboBox, id As Integer)
        For Each item As Object In comboBox.Items
            Try
                If Convert.ToInt32(item.ID) = id Then
                    comboBox.SelectedItem = item
                    Exit Sub
                End If
            Catch
                ' تجاهل الأخطاء إذا كان العنصر عبارة عن نص عادي (مثل حقول الحالة المتوقعة)
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

    Private Sub txtappointmentID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtappointmentID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtquenuenumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtquenuenumber.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmAppointments_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        UiTheme.ApplyMaterialForm(Me, "Appointment Management")
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnedit, btnsave, btnnew)
        UiTheme.StyleTextInput(txtappointmentID)
        UiTheme.StyleTextInput(txtquenuenumber)
        UiTheme.StyleTextInput(txtnotes)
    End Sub

    ' ملاحظة: تم حذف التكرار الخاطئ للدالة ApplySettings من هنا ليعمل الـ Build بشكل سليم.
    Private Sub ApplyFontToControls(container As Control, targetFont As Font)
        For Each ctrl As Control In container.Controls
            ctrl.Font = targetFont
            If ctrl.HasChildren Then
                ApplyFontToControls(ctrl, targetFont)
            End If
        Next
    End Sub
End Class