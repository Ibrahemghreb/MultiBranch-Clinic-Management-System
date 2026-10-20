Imports System.Data

Public Class PatientBAL
    Private ReadOnly patientDal As PatientDAL

    Public Sub New()
        Me.New(New PatientDAL())
    End Sub

    Public Sub New(repository As PatientDAL)
        patientDal = repository
    End Sub

    Public Function GetAllPatients() As DataTable
        Return patientDal.GetAll()
    End Function

    Public Function GetDoctors() As List(Of PatientListItem)
        Return patientDal.GetDoctors()
    End Function

    Public Function GetBranches() As List(Of PatientListItem)
        Return patientDal.GetBranches()
    End Function

    Public Function GetPatientById(patientId As Integer) As Patient
        If patientId <= 0 Then
            Throw New ArgumentException("رقم المريض غير صحيح.")
        End If

        Return patientDal.GetById(patientId)
    End Function

    Public Sub AddPatient(patient As Patient)
        ValidatePatient(patient, requirePatientId:=False)

        If patientDal.PatientExists(patient.FirstName, patient.LastName, patient.Phone, patient.NationalID, patient.DateOfBirth) Then
            Throw New InvalidOperationException("المريض موجود مسبقاً ولا يمكن تكراره.")
        End If

        patientDal.Insert(patient)
    End Sub

    Public Sub UpdatePatient(patient As Patient)
        ValidatePatient(patient, requirePatientId:=True)

        If patientDal.PatientExists(patient.FirstName, patient.LastName, patient.Phone, patient.NationalID, patient.DateOfBirth, patient.PatientID) Then
            Throw New InvalidOperationException("يوجد مريض آخر بنفس البيانات.")
        End If

        patientDal.Update(patient)
    End Sub

    Public Sub DeletePatient(patientId As Integer)
        If patientId <= 0 Then
            Throw New ArgumentException("الرجاء تحديد مريض صالح للحذف.")
        End If

        patientDal.Delete(patientId)
    End Sub

    Private Shared Sub ValidatePatient(patient As Patient, requirePatientId As Boolean)
        If patient Is Nothing Then
            Throw New ArgumentNullException(NameOf(patient))
        End If

        If requirePatientId AndAlso patient.PatientID <= 0 Then
            Throw New ArgumentException("رقم المريض غير صحيح.")
        End If

        If String.IsNullOrWhiteSpace(patient.FirstName) OrElse String.IsNullOrWhiteSpace(patient.LastName) Then
            Throw New ArgumentException("الرجاء إدخال الاسم الأول واسم العائلة.")
        End If

        If String.IsNullOrWhiteSpace(patient.Gender) Then
            Throw New ArgumentException("الرجاء اختيار النوع.")
        End If

        If Not String.IsNullOrWhiteSpace(patient.Phone) AndAlso Not patient.Phone.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("رقم الهاتف يجب أن يحتوي على أرقام فقط.")
        End If

        If Not String.IsNullOrWhiteSpace(patient.Phone2) AndAlso Not patient.Phone2.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("رقم الهاتف البديل يجب أن يحتوي على أرقام فقط.")
        End If

        If Not String.IsNullOrWhiteSpace(patient.EmergencyPhone) AndAlso Not patient.EmergencyPhone.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("رقم هاتف الطوارئ يجب أن يحتوي على أرقام فقط.")
        End If

        patient.PatientCode = If(patient.PatientCode, String.Empty).Trim()
        patient.FirstName = patient.FirstName.Trim()
        patient.LastName = patient.LastName.Trim()
        patient.Gender = patient.Gender.Trim()
        patient.NationalID = If(patient.NationalID, String.Empty).Trim()
        patient.PassportNo = If(patient.PassportNo, String.Empty).Trim()
        patient.Address = If(patient.Address, String.Empty).Trim()
        patient.City = If(patient.City, String.Empty).Trim()
        patient.CountryCode = If(patient.CountryCode, String.Empty).Trim().ToUpper()
        patient.BloodType = If(patient.BloodType, String.Empty).Trim()
        patient.Phone = If(patient.Phone, String.Empty).Trim()
        patient.Phone2 = If(patient.Phone2, String.Empty).Trim()
        patient.EmergencyContact = If(patient.EmergencyContact, String.Empty).Trim()
        patient.EmergencyPhone = If(patient.EmergencyPhone, String.Empty).Trim()
        patient.Email = If(patient.Email, String.Empty).Trim()
        patient.Notes = If(patient.Notes, String.Empty).Trim()
    End Sub
End Class