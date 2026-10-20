Imports System.Data

Public Class DiagnosisBAL
    Private ReadOnly diagnosisDal As DiagnosisDAL

    Public Sub New()
        Me.New(New DiagnosisDAL())
    End Sub

    Public Sub New(repository As DiagnosisDAL)
        diagnosisDal = repository
    End Sub

    Public Function GetAllDiagnoses() As DataTable
        Return diagnosisDal.GetAll()
    End Function

    Public Function GetPatients() As List(Of PatientListItem)
        Return diagnosisDal.GetPatients()
    End Function

    Public Function GetAppointmentsByPatient(patientId As Integer) As List(Of PatientListItem)
        If patientId <= 0 Then
            Return New List(Of PatientListItem)()
        End If

        Return diagnosisDal.GetAppointmentsByPatient(patientId)
    End Function

    Public Function GetDiagnosisById(diagnosisId As Integer) As Diagnosis
        If diagnosisId <= 0 Then
            Throw New ArgumentException("رقم التشخيص غير صحيح.")
        End If

        Return diagnosisDal.GetById(diagnosisId)
    End Function

    Public Function AddDiagnosis(diagnosis As Diagnosis) As Integer
        ValidateDiagnosis(diagnosis, requireDiagnosisId:=False)

        If diagnosisDal.DiagnosisExistsForAppointment(diagnosis.AppointmentID) Then
            Throw New InvalidOperationException("التشخيص موجود بالفعل لهذا الموعد.")
        End If

        Return diagnosisDal.Insert(diagnosis)
    End Function

    Public Sub UpdateDiagnosis(diagnosis As Diagnosis)
        ValidateDiagnosis(diagnosis, requireDiagnosisId:=True)

        If diagnosisDal.DiagnosisExistsForAppointment(diagnosis.AppointmentID, diagnosis.DiagnosisID) Then
            Throw New InvalidOperationException("يوجد تشخيص آخر لهذا الموعد.")
        End If

        diagnosisDal.Update(diagnosis)
    End Sub

    Public Sub DeleteDiagnosis(diagnosisId As Integer)
        If diagnosisId <= 0 Then
            Throw New ArgumentException("الرجاء تحديد تشخيص صالح للحذف.")
        End If

        diagnosisDal.Delete(diagnosisId)
    End Sub

    Private Shared Sub ValidateDiagnosis(diagnosis As Diagnosis, requireDiagnosisId As Boolean)
        If diagnosis Is Nothing Then
            Throw New ArgumentNullException(NameOf(diagnosis))
        End If

        If requireDiagnosisId AndAlso diagnosis.DiagnosisID <= 0 Then
            Throw New ArgumentException("رقم التشخيص غير صحيح.")
        End If

        If diagnosis.AppointmentID <= 0 Then
            Throw New ArgumentException("يرجى اختيار موعد صالح.")
        End If

        diagnosis.DiagnosisText = If(diagnosis.DiagnosisText, String.Empty).Trim()
        diagnosis.Report = If(diagnosis.Report, String.Empty).Trim()
        diagnosis.Notes = If(diagnosis.Notes, String.Empty).Trim()
    End Sub
End Class
