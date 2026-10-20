Imports System.Data.SqlClient

Public Class reportappointments
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtPatientiD.Enabled = rbpatientid.Checked
        txtappointmentid.Enabled = rbAppointmentID.Checked
        txtdoctorid.Enabled = rbdoctorsid.Checked
        appointmentdate.Enabled = rbAppointmentDate.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles _
        rbpatientid.CheckedChanged,
        rbAppointmentID.CheckedChanged,
        rbdoctorsid.CheckedChanged,
        rbAppointmentDate.CheckedChanged
        UpdateInputControls()
    End Sub

    Private Sub reportappointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UpdateInputControls()
        btnexportpdf.Enabled = False
        btnExportExcel.Enabled = False
        UiTheme.ApplyReportForm(Me, "تقرير المواعيد")
        ReportHelper.AddViewReportButton(Me, dgvResults, "تقرير المواعيد", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String =
                "SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.BranchID, " &
                "a.AppointmentStart, a.AppointmentEnd, a.Status, a.Notes, " &
                "p.FirstName + ' ' + p.LastName AS PatientName, " &
                "d.FirstName + ' ' + d.LastName AS DoctorName, " &
                "b.BranchName " &
                "FROM dbo.Appointments a " &
                "JOIN dbo.Patients p ON p.PatientID = a.PatientID " &
                "JOIN dbo.Doctors d ON d.DoctorID = a.DoctorID " &
                "JOIN dbo.Branches b ON b.BranchID = a.BranchID " &
                "WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            If rbpatientid.Checked Then
                If String.IsNullOrWhiteSpace(txtPatientiD.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف المريض.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                conditions.Add("a.PatientID = @PatientID")
                parameters.Add(New SqlParameter("@PatientID", SqlDbType.Int) With {.Value = Integer.Parse(txtPatientiD.Text.Trim())})
                currentSearchType = "PatientID"
                currentSearchValue = txtPatientiD.Text.Trim()
            ElseIf rbAppointmentID.Checked Then
                If String.IsNullOrWhiteSpace(txtappointmentid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الموعد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                conditions.Add("a.AppointmentID = @AppointmentID")
                parameters.Add(New SqlParameter("@AppointmentID", SqlDbType.Int) With {.Value = Integer.Parse(txtappointmentid.Text.Trim())})
                currentSearchType = "AppointmentID"
                currentSearchValue = txtappointmentid.Text.Trim()
            ElseIf rbdoctorsid.Checked Then
                If String.IsNullOrWhiteSpace(txtdoctorid.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الطبيب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                conditions.Add("a.DoctorID = @DoctorID")
                parameters.Add(New SqlParameter("@DoctorID", SqlDbType.Int) With {.Value = Integer.Parse(txtdoctorid.Text.Trim())})
                currentSearchType = "DoctorID"
                currentSearchValue = txtdoctorid.Text.Trim()
            ElseIf rbAppointmentDate.Checked Then
                Dim selectedDate As Date = appointmentdate.Value.Date
                conditions.Add("CAST(a.AppointmentStart AS DATE) = @AppointmentDate")
                parameters.Add(New SqlParameter("@AppointmentDate", SqlDbType.Date) With {.Value = selectedDate})
                currentSearchType = "AppointmentDate"
                currentSearchValue = selectedDate.ToString("yyyyMMdd")
            Else
                MessageBox.Show("الرجاء اختيار معيار بحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim fullQuery As String = baseQuery & String.Join(" AND ", conditions) & " ORDER BY a.AppointmentStart"

            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(fullQuery, conn)
                    cmd.Parameters.AddRange(parameters.ToArray())
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        dgvResults.DataSource = dt
                    End Using
                End Using
            End Using

            btnexportpdf.Enabled = dgvResults.Rows.Count > 0
            btnExportExcel.Enabled = dgvResults.Rows.Count > 0
        Catch ex As Exception
            Logger.LogError($"reportappointments.btnsearch: {ex.Message}")
            MessageBox.Show("خطأ في البحث: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnexportpdf_Click(sender As Object, e As EventArgs) Handles btnexportpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Appointments", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(dgvResults, fileName)
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Appointments", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(dgvResults, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Close()
    End Sub

    Private Sub reportappointments_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
