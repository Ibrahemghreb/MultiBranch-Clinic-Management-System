Imports System.Data

Public Class AppointmentBAL
    Private ReadOnly appointmentDal As AppointmentDAL
    Private Shared ReadOnly AllowedStatuses As HashSet(Of String) = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        "Pending",
        "Confirmed",
        "CheckedIn",
        "InProgress",
        "Completed",
        "Cancelled",
        "NoShow"
    }

    Public Sub New()
        Me.New(New AppointmentDAL())
    End Sub

    Public Sub New(repository As AppointmentDAL)
        appointmentDal = repository
    End Sub

    Public Function GetAllAppointments() As DataTable
        Return appointmentDal.GetAll()
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Return appointmentDal.GetBranches()
    End Function

    Public Function GetDoctors() As List(Of PatientListItem)
        Return appointmentDal.GetDoctors()
    End Function

    Public Function GetPatients() As List(Of PatientListItem)
        Return appointmentDal.GetPatients()
    End Function

    Public Function GetAllowedStatuses() As List(Of String)
        Return AllowedStatuses.ToList()
    End Function

    Public Function GetAppointmentCountsByDate(periodStart As DateTime, periodEnd As DateTime, Optional branchId As Integer = 0, Optional doctorId As Integer = 0) As Dictionary(Of DateTime, Integer)
        Return appointmentDal.GetAppointmentCountsByDate(periodStart, periodEnd, branchId, doctorId)
    End Function

    Public Function GetAppointmentsByDate(
    targetDate As DateTime,
    Optional branchId As Integer = 0,
    Optional doctorId As Integer = 0) As List(Of CalendarAppointmentItem)

        Return appointmentDal.GetAppointmentsByDate(
        targetDate,
        branchId,
        doctorId)

    End Function
    Public Function GetAppointmentById(appointmentId As Integer) As Appointment
        If appointmentId <= 0 Then
            Throw New ArgumentException("رقم الموعد غير صحيح.")
        End If

        Return appointmentDal.GetById(appointmentId)
    End Function

    Public Sub AddAppointment(appointment As Appointment)
        ValidateAppointment(appointment, requireAppointmentId:=False)

        If appointmentDal.AppointmentExists(appointment.PatientID, appointment.AppointmentStart) Then
            Throw New InvalidOperationException("الموعد موجود بالفعل لهذا المريض في هذا التاريخ.")
        End If

        appointmentDal.Insert(appointment)
        Try
            Dim notif As New NotificationBAL()
            notif.NotifyAdmins("موعد جديد", $"تم إنشاء موعد للمريض #{appointment.PatientID} في {appointment.AppointmentStart:yyyy-MM-dd HH:mm}", "Info")
        Catch
        End Try
    End Sub

    Public Sub UpdateAppointment(appointment As Appointment)
        ValidateAppointment(appointment, requireAppointmentId:=True)

        If appointmentDal.AppointmentExists(appointment.PatientID, appointment.AppointmentStart, appointment.AppointmentID) Then
            Throw New InvalidOperationException("يوجد موعد آخر لنفس المريض في هذا التاريخ.")
        End If

        appointmentDal.Update(appointment)
        Try
            If String.Equals(appointment.Status, "Cancelled", StringComparison.OrdinalIgnoreCase) Then
                Dim notif As New NotificationBAL()
                notif.NotifyAppointmentCancelled($"المريض #{appointment.PatientID}", appointment.AppointmentStart)
            End If
        Catch
        End Try
    End Sub

    Public Sub DeleteAppointment(appointmentId As Integer)
        If appointmentId <= 0 Then
            Throw New ArgumentException("الرجاء تحديد موعد صالح للحذف.")
        End If

        appointmentDal.Delete(appointmentId)
    End Sub

    Private Shared Sub ValidateAppointment(appointment As Appointment, requireAppointmentId As Boolean)
        If appointment Is Nothing Then
            Throw New ArgumentNullException(NameOf(appointment))
        End If

        If requireAppointmentId AndAlso appointment.AppointmentID <= 0 Then
            Throw New ArgumentException("رقم الموعد غير صحيح.")
        End If

        If appointment.BranchID <= 0 Then
            Throw New ArgumentException("الرجاء اختيار الفرع.")
        End If

        If appointment.DoctorID <= 0 Then
            Throw New ArgumentException("الرجاء اختيار الطبيب.")
        End If

        If appointment.PatientID <= 0 Then
            Throw New ArgumentException("الرجاء اختيار المريض.")
        End If

        If appointment.QueueNumber <= 0 Then
            Throw New ArgumentException("رقم الدور يجب أن يكون أكبر من صفر.")
        End If

        If appointment.AppointmentEnd <= appointment.AppointmentStart Then
            Throw New ArgumentException("وقت انتهاء الموعد يجب أن يكون بعد وقت البدء.")
        End If

        If String.IsNullOrWhiteSpace(appointment.Status) Then
            appointment.Status = "Pending"
        End If

        appointment.Status = NormalizeStatus(appointment.Status)
        appointment.Notes = If(appointment.Notes, String.Empty).Trim()
    End Sub

    Private Shared Function NormalizeStatus(status As String) As String
        Dim cleanStatus As String = status.Trim()

        If Not AllowedStatuses.Contains(cleanStatus) Then
            Throw New ArgumentException("حالة الموعد غير صحيحة.")
        End If

        For Each allowedStatus As String In AllowedStatuses
            If String.Equals(allowedStatus, cleanStatus, StringComparison.OrdinalIgnoreCase) Then
                Return allowedStatus
            End If
        Next

        Return cleanStatus
    End Function
End Class
