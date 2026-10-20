Imports System.Data.SqlClient
Imports System.IO

Public Class frmDoctorDashboard

    Public Sub ApplySettings(newFont As Font, newBackColor As Color, backgroundImagePath As String)
        ' نفس الكود الموجود (لم يتغير)
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

    Private Sub ApplyFontToControls(parent As Control, fontToApply As Font)
        For Each ctrl As Control In parent.Controls
            Try
                ctrl.Font = fontToApply
                If ctrl.HasChildren Then ApplyFontToControls(ctrl, fontToApply)
            Catch
            End Try
        Next
    End Sub

    Private Sub frmDoctorDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UiTheme.ApplyMaterialForm(Me, "لوحة تحكم الطبيب")
        Try
            If SessionManager.UserID = 0 Then
                MessageBox.Show("يرجى تسجيل الدخول أولاً.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Dim loginForm As New frmLogin()
                Me.Hide()
                loginForm.ShowDialog()
                Me.Close()
                Return
            End If

            LoadPatients()
            LoadAppointments()
            Me.KeyPreview = True
            ApplySettings(My.Settings.appfont, My.Settings.appbackcolor, My.Settings.appbackgroundimagepath)
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحميل لوحة التحكم: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPatients()
        Try
            If SessionManager.DoctorID = 0 OrElse SessionManager.BranchID = 0 Then
                MessageBox.Show("بيانات الدكتور أو الفرع غير مكتملة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim patientQuery As String = "SELECT PatientID, FirstName, LastName, DateOfBirth, Gender, Phone " &
                                             "FROM Patients " &
                                             "WHERE DoctorID = @DoctorID AND BranchID = @BranchID"
                Using command As New SqlCommand(patientQuery, connection)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)

                    Using adapter As New SqlDataAdapter(command)
                        Dim table As New DataTable()
                        adapter.Fill(table)
                        DataGridView1.DataSource = table
                        ConfigureDataGridView()

                        If table.Rows.Count = 0 Then
                            MessageBox.Show("لا يوجد مرضى مرتبطون بك في هذا الفرع حاليًا.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحميل بيانات المرضى: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadAppointments()
        Try
            If SessionManager.DoctorID = 0 OrElse SessionManager.BranchID = 0 Then
                MessageBox.Show("بيانات الدكتور أو الفرع غير مكتملة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim appointmentQuery As String = "SELECT a.AppointmentID, a.QueueNumber, p.FirstName, p.LastName, a.AppointmentStart AS AppointmentDate, a.Status, a.Notes " &
                                                "FROM Appointments a " &
                                                "INNER JOIN Patients p ON a.PatientID = p.PatientID " &
                                                "WHERE a.DoctorID = @DoctorID AND a.BranchID = @BranchID AND CAST(a.AppointmentStart AS DATE) = @Today AND a.Status = 'Pending' " &
                                                "ORDER BY a.QueueNumber ASC"
                Using command As New SqlCommand(appointmentQuery, connection)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)
                    command.Parameters.AddWithValue("@Today", Date.Today)

                    Using adapter As New SqlDataAdapter(command)
                        Dim table As New DataTable()
                        adapter.Fill(table)
                        DataGridView2.DataSource = table
                        ConfigureQueueDataGridView()

                        If table.Rows.Count = 0 Then
                            ' يمكن إظهار رسالة اختيارية
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحميل قائمة المواعيد: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' تكوين DataGridView1 (نفس الكود القديم)
    Private Sub ConfigureDataGridView()
        With DataGridView1
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "PatientID", .HeaderText = "رقم المريض", .Name = "PatientID", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "FirstName", .HeaderText = "الاسم الأول", .Name = "FirstName", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "LastName", .HeaderText = "اسم العائلة", .Name = "LastName", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "DateOfBirth", .HeaderText = "تاريخ الميلاد", .Name = "DateOfBirth", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "Gender", .HeaderText = "الجنس", .Name = "Gender", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "Phone", .HeaderText = "رقم الهاتف", .Name = "Phone", .ReadOnly = True})
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
    End Sub

    Private Sub ConfigureQueueDataGridView()
        With DataGridView2
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "AppointmentID", .HeaderText = "رقم الموعد", .Name = "AppointmentID", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "QueueNumber", .HeaderText = "رقم الدور", .Name = "QueueNumber", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "FirstName", .HeaderText = "الاسم الأول", .Name = "FirstName", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "LastName", .HeaderText = "اسم العائلة", .Name = "LastName", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "AppointmentDate", .HeaderText = "وقت الموعد", .Name = "AppointmentDate", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "Status", .HeaderText = "الحالة", .Name = "Status", .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "Notes", .HeaderText = "ملاحظات", .Name = "Notes", .ReadOnly = True})
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
    End Sub

    ' دوال الأزرار (بنفس الشكل، ولكن باستخدام SessionManager بدلاً من الاستعلامات الإضافية)
    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("يرجى تحديد موعد لتغيير ترتيبه.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim appointmentID As Integer = Convert.ToInt32(selectedRow.Cells("AppointmentID").Value)
            Dim currentQueueNumber As Integer = Convert.ToInt32(selectedRow.Cells("QueueNumber").Value)

            If currentQueueNumber <= 1 Then
                MessageBox.Show("هذا الموعد في أعلى القائمة بالفعل.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                connection.Open()
                Dim updateQuery As String = "UPDATE Appointments SET QueueNumber = QueueNumber + 1 " &
                                           "WHERE QueueNumber = @PreviousQueueNumber AND AppointmentID != @AppointmentID AND CAST(AppointmentStart AS DATE) = @Today AND Status = 'Pending' AND DoctorID = @DoctorID AND BranchID = @BranchID;" &
                                           "UPDATE Appointments SET QueueNumber = @NewQueueNumber WHERE AppointmentID = @AppointmentID"
                Using command As New SqlCommand(updateQuery, connection)
                    command.Parameters.AddWithValue("@PreviousQueueNumber", currentQueueNumber - 1)
                    command.Parameters.AddWithValue("@AppointmentID", appointmentID)
                    command.Parameters.AddWithValue("@NewQueueNumber", currentQueueNumber - 1)
                    command.Parameters.AddWithValue("@Today", Date.Today)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)
                    command.ExecuteNonQuery()
                End Using
            End Using

            LoadAppointments()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تغيير ترتيب الدور: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        ' مشابه لما سبق، مع تعديل معاملات SQL لتشمل DoctorID و BranchID
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("يرجى تحديد موعد لتغيير ترتيبه.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim appointmentID As Integer = Convert.ToInt32(selectedRow.Cells("AppointmentID").Value)
            Dim currentQueueNumber As Integer = Convert.ToInt32(selectedRow.Cells("QueueNumber").Value)

            Dim maxQueueNumber As Integer
            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim query As String = "SELECT MAX(QueueNumber) FROM Appointments WHERE CAST(AppointmentStart AS DATE) = @Today AND Status = 'Pending' AND DoctorID = @DoctorID AND BranchID = @BranchID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Today", Date.Today)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)
                    connection.Open()
                    maxQueueNumber = If(IsDBNull(command.ExecuteScalar()), 0, Convert.ToInt32(command.ExecuteScalar()))
                End Using
            End Using

            If currentQueueNumber >= maxQueueNumber Then
                MessageBox.Show("هذا الموعد في أسفل القائمة بالفعل.", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                connection.Open()
                Dim updateQuery As String = "UPDATE Appointments SET QueueNumber = QueueNumber - 1 " &
                                           "WHERE QueueNumber = @NextQueueNumber AND AppointmentID != @AppointmentID AND CAST(AppointmentStart AS DATE) = @Today AND Status = 'Pending' AND DoctorID = @DoctorID AND BranchID = @BranchID;" &
                                           "UPDATE Appointments SET QueueNumber = @NewQueueNumber WHERE AppointmentID = @AppointmentID"
                Using command As New SqlCommand(updateQuery, connection)
                    command.Parameters.AddWithValue("@NextQueueNumber", currentQueueNumber + 1)
                    command.Parameters.AddWithValue("@AppointmentID", appointmentID)
                    command.Parameters.AddWithValue("@NewQueueNumber", currentQueueNumber + 1)
                    command.Parameters.AddWithValue("@Today", Date.Today)
                    command.Parameters.AddWithValue("@DoctorID", SessionManager.DoctorID)
                    command.Parameters.AddWithValue("@BranchID", SessionManager.BranchID)
                    command.ExecuteNonQuery()
                End Using
            End Using

            LoadAppointments()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تغيير ترتيب الدور: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnComplete_Click(sender As Object, e As EventArgs) Handles btnComplete.Click
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("يرجى تحديد موعد لتغيير حالته.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim appointmentID As Integer = Convert.ToInt32(selectedRow.Cells("AppointmentID").Value)

            Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
                Dim query As String = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentID = @AppointmentID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@AppointmentID", appointmentID)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using

            LoadAppointments()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحديث حالة الموعد: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Try
            SessionManager.ClearSession()
            For Each form As Form In Application.OpenForms
                If TypeOf form Is FrmMain Then
                    form.Close()
                End If
            Next
            Dim frmmain As New FrmMain()
            Me.Hide()
            frmmain.ShowDialog()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تسجيل الخروج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDoctorDashboard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
            Managementclinic.FrmMain.ShowDialog()
        End If
    End Sub
End Class