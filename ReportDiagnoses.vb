Imports System.Data.SqlClient

Public Class ReportDiagnoses
    Private currentSearchType As String = ""
    Private currentSearchValue As String = ""

    Private Sub UpdateInputControls()
        txtDiagnosestD.Enabled = rbdiagnosesid.Checked
        txtAppointmentID.Enabled = rbappointmentid.Checked
        Appointmentdate.Enabled = rbAppointmentDate.Checked
    End Sub

    Private Sub rbCheckedChanged(sender As Object, e As EventArgs) Handles rbdiagnosesid.CheckedChanged, rbappointmentid.CheckedChanged, rbAppointmentDate.CheckedChanged

        UpdateInputControls()
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            Dim baseQuery As String = "SELECT D.* FROM Diagnoses D INNER JOIN Appointments A ON D.AppointmentID = A.AppointmentID WHERE "
            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SqlParameter)

            ' حالة البحث حسب معرّف التشخيص
            If rbdiagnosesid.Checked Then
                If String.IsNullOrEmpty(txtDiagnosestD.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف التشخيص")
                    Return
                End If
                conditions.Add("D.DiagnosisID = @DiagnosisID")
                parameters.Add(New SqlParameter("@DiagnosisID", SqlDbType.Int) With {.Value = Integer.Parse(txtDiagnosestD.Text)})
                currentSearchType = "DiagnosisID"
                currentSearchValue = txtDiagnosestD.Text
            End If

            ' حالة البحث حسب معرّف الموعد
            If rbappointmentid.Checked Then
                If String.IsNullOrEmpty(txtAppointmentID.Text) Then
                    MessageBox.Show("الرجاء إدخال معرّف الموعد")
                    Return
                End If
                conditions.Add("D.AppointmentID = @AppointmentID")
                parameters.Add(New SqlParameter("@AppointmentID", SqlDbType.Int) With {.Value = Integer.Parse(txtAppointmentID.Text)})
                currentSearchType = "AppointmentID"
                currentSearchValue = txtAppointmentID.Text
            End If


            ' حالة البحث حسب تاريخ الموعد
            If rbAppointmentDate.Checked Then
                Dim selectedDate As DateTime = Appointmentdate.Value.Date
                conditions.Add("A.AppointmentStart >= @StartDate AND A.AppointmentStart < @EndDate")
                parameters.Add(New SqlParameter("@StartDate", SqlDbType.DateTime) With {.Value = selectedDate})
                parameters.Add(New SqlParameter("@EndDate", SqlDbType.DateTime) With {.Value = selectedDate.AddDays(1)})
                currentSearchType = "AppointmentStart"
                currentSearchValue = selectedDate.ToString("yyyyMMdd")
            End If

            ' التحقق من وجود شروط بحث
            If conditions.Count = 0 Then
                MessageBox.Show("الرجاء اختيار معيار بحث وإدخال قيمة صحيحة")
                Return
            End If

            Dim fullQuery As String = baseQuery & String.Join(" AND ", conditions)

            Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
                conn.Open()
                Using cmd As New SqlCommand(fullQuery, conn)
                    cmd.Parameters.AddRange(parameters.ToArray())
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using

            btnprintexcel.Enabled = True
            btnprintpdf.Enabled = True

        Catch ex As Exception
            MessageBox.Show("خطأ في البحث: " & ex.Message)
        End Try
    End Sub

    Private Sub btnprintexcel_Click(sender As Object, e As EventArgs) Handles btnprintexcel.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Diagnoses", currentSearchType, currentSearchValue, "xlsx")
        ReportHelper.ExportDataGridViewToExcel(DataGridView1, fileName)
    End Sub

    Private Sub btnprintpdf_Click(sender As Object, e As EventArgs) Handles btnprintpdf.Click
        Dim fileName As String = ReportHelper.BuildExportFileName("Diagnoses", currentSearchType, currentSearchValue, "pdf")
        ReportHelper.ExportDataGridViewToPdf(DataGridView1, fileName)
    End Sub

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        'FrmMain.Show()
        Me.Hide()
    End Sub


    Private Sub ReportDiagnoses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        UiTheme.ApplyReportForm(Me, "تقرير التشخيصات")
        ReportHelper.AddViewReportButton(Me, DataGridView1, "تقرير التشخيصات", Function() currentSearchType, Function() currentSearchValue)
    End Sub

    Private Sub ReportDiagnoses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
End Class