Imports System.IO
Imports System.Drawing

Partial Public Class frmDiagnoses
    Private ReadOnly diagnosisBal As New DiagnosisBAL()
    Private ReadOnly appointmentBal As New AppointmentBAL()

    Private point1 As Point
    Private point2 As Point
    Private isPoint1Set As Boolean = False
    Private isPoint2Set As Boolean = False
    Private scaleFactor As Double = 1.0

    Private Sub frmDiagnoses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ApplyUiTheme()
        ApplySettings(My.Settings.appfont, My.Settings.appbackcolor, My.Settings.AppBackgroundImagePath)
        xrayimage.SizeMode = PictureBoxSizeMode.Zoom
        LoadPatients()
        ClearForm()
    End Sub

    Private Sub LoadPatients()
        Try
            txtPatientID.Items.Clear()
            txtPatientID.Items.AddRange(diagnosisBal.GetPatients().ToArray())
            txtPatientID.SelectedIndex = If(txtPatientID.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل المرضى", ex)
        End Try
    End Sub

    Private Sub LoadAppointments()
        Try
            txtappointmentid.Items.Clear()

            If txtPatientID.SelectedItem Is Nothing Then
                Return
            End If

            Dim patientId As Integer = DirectCast(txtPatientID.SelectedItem, PatientListItem).ID
            txtappointmentid.Items.AddRange(diagnosisBal.GetAppointmentsByPatient(patientId).ToArray())
            txtappointmentid.SelectedIndex = If(txtappointmentid.Items.Count > 0, 0, -1)
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل المواعيد", ex)
        End Try
    End Sub

    Private Sub LoadDiagnoses()
        Try
            DataGridView1.DataSource = diagnosisBal.GetAllDiagnoses()
        Catch ex As Exception
            ShowError("خطأ أثناء تحميل التشخيصات", ex)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            Dim newId As Integer = diagnosisBal.AddDiagnosis(ReadDiagnosisFromForm(requireDiagnosisId:=False))
            txtDiagnosisID2.Text = newId.ToString()
            MessageBox.Show("تم حفظ التشخيص بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ أثناء حفظ التشخيص", ex)
        End Try
    End Sub

    Private Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        FindData()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Try
            diagnosisBal.UpdateDiagnosis(ReadDiagnosisFromForm(requireDiagnosisId:=True))
            MessageBox.Show("تم تعديل التشخيص بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ أثناء تعديل التشخيص", ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            Dim diagnosisId As Integer = ReadRequiredInteger(txtDiagnosisID2.Text, "رقم التشخيص")

            If MessageBox.Show("هل أنت متأكد من حذف هذا التشخيص؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            diagnosisBal.DeleteDiagnosis(diagnosisId)
            MessageBox.Show("تم حذف التشخيص بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
        Catch ex As Exception
            ShowError("خطأ أثناء حذف التشخيص", ex)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ClearForm()
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Hide()
    End Sub

    Private Sub FindData()
        Try
            Dim diagnosisId As Integer = ReadRequiredInteger(txtDiagnosisID2.Text, "رقم التشخيص")
            Dim diagnosis As Diagnosis = diagnosisBal.GetDiagnosisById(diagnosisId)

            If diagnosis Is Nothing Then
                MessageBox.Show("لم يتم العثور على التشخيص.", "ملاحظة", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                Return
            End If

            FillForm(diagnosis)
        Catch ex As Exception
            ShowError("خطأ أثناء البحث عن التشخيص", ex)
        End Try
    End Sub

    Private Function ReadDiagnosisFromForm(requireDiagnosisId As Boolean) As Diagnosis
        Dim diagnosis As New Diagnosis With {
            .AppointmentID = ReadSelectedId(txtappointmentid, "الموعد"),
            .DoctorID = GetSelectedAppointmentDoctorId(),
            .DiagnosisDate = Diagnosisdate.Value,
            .DiagnosisText = txtdiagnosis.Text,
            .XRayImage = GetImageBytes(),
            .Report = txtreport.Text,
            .Notes = txtnotes.Text
        }

        If requireDiagnosisId Then
            diagnosis.DiagnosisID = ReadRequiredInteger(txtDiagnosisID2.Text, "رقم التشخيص")
        ElseIf Not String.IsNullOrWhiteSpace(txtDiagnosisID2.Text) Then
            diagnosis.DiagnosisID = ReadRequiredInteger(txtDiagnosisID2.Text, "رقم التشخيص")
        End If

        Return diagnosis
    End Function

    Private Function GetSelectedAppointmentDoctorId() As Integer
        Dim appointmentId As Integer = ReadSelectedId(txtappointmentid, "الموعد")
        Dim appointment As Appointment = appointmentBal.GetAppointmentById(appointmentId)
        If appointment Is Nothing Then
            Throw New ArgumentException("الموعد المحدد غير صالح.")
        End If

        Return appointment.DoctorID
    End Function

    Private Sub FillForm(diagnosis As Diagnosis)
        txtDiagnosisID2.Text = diagnosis.DiagnosisID.ToString()
        SelectComboItem(txtPatientID, diagnosis.PatientID)
        LoadAppointments()
        SelectComboItem(txtappointmentid, diagnosis.AppointmentID)
        Diagnosisdate.Value = diagnosis.DiagnosisDate
        txtdiagnosis.Text = diagnosis.DiagnosisText
        txtreport.Text = diagnosis.Report
        txtnotes.Text = diagnosis.Notes
        xrayimage.Image = BytesToImage(diagnosis.XRayImage)
        ResetPoints()
    End Sub

    Private Sub ClearForm()
        txtDiagnosisID2.Clear()
        txtPatientID.SelectedIndex = If(txtPatientID.Items.Count > 0, 0, -1)
        LoadAppointments()
        Diagnosisdate.Value = DateTime.Now
        txtdiagnosis.Clear()
        txtreport.Clear()
        txtnotes.Clear()
        xrayimage.Image = Nothing
        space.Text = ""
        ResetPoints()
        LoadDiagnoses()
        txtDiagnosisID2.Focus()
    End Sub

    Private Function GetImageBytes() As Byte()
        If xrayimage.Image Is Nothing Then
            Return Nothing
        End If

        Using ms As New MemoryStream()
            xrayimage.Image.Save(ms, Imaging.ImageFormat.Png)
            Return ms.ToArray()
        End Using
    End Function

    Private Shared Function BytesToImage(imageBytes As Byte()) As Image
        If imageBytes Is Nothing OrElse imageBytes.Length = 0 Then
            Return Nothing
        End If

        Using ms As New MemoryStream(imageBytes)
            Return Image.FromStream(ms)
        End Using
    End Function

    Private Shared Function ReadSelectedId(comboBox As ComboBox, fieldName As String) As Integer
        If comboBox.SelectedItem Is Nothing Then
            Throw New ArgumentException($"الرجاء اختيار {fieldName}.")
        End If

        Return DirectCast(comboBox.SelectedItem, PatientListItem).ID
    End Function

    Private Shared Sub SelectComboItem(comboBox As ComboBox, id As Integer)
        For Each item As PatientListItem In comboBox.Items
            If item.ID = id Then
                comboBox.SelectedItem = item
                Exit Sub
            End If
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

    Private Sub txtDiagnosisID2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiagnosisID2.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPatientID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtPatientID.SelectedIndexChanged
        LoadAppointments()
    End Sub

    Private Sub xrayimage_MouseClick(sender As Object, e As MouseEventArgs) Handles xrayimage.MouseClick
        If Not isPoint1Set Then
            point1 = e.Location
            isPoint1Set = True
        ElseIf Not isPoint2Set Then
            point2 = e.Location
            isPoint2Set = True
            CalculateDistance()
        End If

        xrayimage.Invalidate()
    End Sub

    Private Sub xrayimage_Paint(sender As Object, e As PaintEventArgs) Handles xrayimage.Paint
        If isPoint1Set Then e.Graphics.FillEllipse(Brushes.Red, point1.X - 5, point1.Y - 5, 10, 10)
        If isPoint2Set Then e.Graphics.FillEllipse(Brushes.Blue, point2.X - 5, point2.Y - 5, 10, 10)
        If isPoint1Set AndAlso isPoint2Set Then e.Graphics.DrawLine(Pens.Green, point1, point2)
    End Sub

    Private Sub CalculateDistance()
        Dim dx As Integer = point2.X - point1.X
        Dim dy As Integer = point2.Y - point1.Y
        Dim distanceInPixels As Double = Math.Sqrt(dx * dx + dy * dy)
        Dim distanceInMM As Double = distanceInPixels / scaleFactor
        space.Text = distanceInMM.ToString("F2") & " ملم"
        ResetPoints()
    End Sub

    Private Sub ResetPoints()
        isPoint1Set = False
        isPoint2Set = False
        xrayimage.Invalidate()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            If ofd.ShowDialog() = DialogResult.OK Then
                xrayimage.Image = Image.FromFile(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub btnResetPoints_Click(sender As Object, e As EventArgs) Handles btnResetPoints.Click
        ResetPoints()
        space.Text = ""
    End Sub

    Private Sub SetScaleFactor(pixelsPerMM As Double)
        scaleFactor = pixelsPerMM
    End Sub

    Private Sub frmDiagnoses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        UiTheme.ApplyMaterialForm(Me, "Diagnosis Management")
        UiTheme.StyleDataGridView(DataGridView1)
        UiTheme.ApplyIconButtons(Me, btnback, btndelete, btnfind, btnedit, btnsave, btnnew)
        UiTheme.StyleTextInput(txtDiagnosisID2)
        UiTheme.StyleTextInput(txtreport)
        UiTheme.StyleTextInput(txtdiagnosis)
        UiTheme.StyleTextInput(txtnotes)
        UiTheme.StyleActionButton(Button1)
        UiTheme.StyleActionButton(btnResetPoints)
    End Sub

    Private Sub ApplyFontToControls(parent As Control, fontToApply As Font)
        For Each ctrl As Control In parent.Controls
            Try
                ctrl.Font = fontToApply
                If ctrl.HasChildren Then
                    ApplyFontToControls(ctrl, fontToApply)
                End If
            Catch
            End Try
        Next
    End Sub
End Class
