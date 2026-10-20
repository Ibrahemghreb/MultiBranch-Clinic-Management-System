Imports System.Data
Imports System.Linq

Public Class DoctorBAL
    Private ReadOnly doctorDal As DoctorDAL

    Public Sub New()
        Me.New(New DoctorDAL())
    End Sub

    Public Sub New(repository As DoctorDAL)
        doctorDal = repository
    End Sub

    Public Function GetAllDoctors() As DataTable
        Return doctorDal.GetAll()
    End Function

    Public Function GetBranches() As List(Of BranchListItem)
        Return doctorDal.GetBranches()
    End Function

    Public Function GetSpecialties() As List(Of SpecialtyListItem)
        Return doctorDal.GetSpecialties()
    End Function

    Public Function GetDoctorById(doctorId As Integer) As Doctor
        If doctorId <= 0 Then
            Throw New ArgumentException("Invalid doctor ID.")
        End If

        Return doctorDal.GetById(doctorId)
    End Function

    Public Sub AddDoctor(doctor As Doctor)
        ValidateDoctor(doctor, requireDoctorId:=False)

        If Not doctorDal.UserExists(doctor.UserID) Then
            Throw New InvalidOperationException("Linked user account was not found.")
        End If

        If doctorDal.UserAlreadyLinked(doctor.UserID) Then
            Throw New InvalidOperationException("This user is already linked to another doctor.")
        End If

        If doctorDal.DoctorExists(doctor.FirstName, doctor.LastName, doctor.Phone) Then
            Throw New InvalidOperationException("A doctor with the same name and phone already exists.")
        End If

        doctorDal.Insert(doctor)
    End Sub

    Public Sub UpdateDoctor(doctor As Doctor)
        ValidateDoctor(doctor, requireDoctorId:=True)

        If Not doctorDal.UserExists(doctor.UserID) Then
            Throw New InvalidOperationException("Linked user account was not found.")
        End If

        If doctorDal.UserAlreadyLinked(doctor.UserID, doctor.DoctorID) Then
            Throw New InvalidOperationException("This user is already linked to another doctor.")
        End If

        If doctorDal.DoctorExists(doctor.FirstName, doctor.LastName, doctor.Phone, doctor.DoctorID) Then
            Throw New InvalidOperationException("Another doctor with the same name and phone already exists.")
        End If

        doctorDal.Update(doctor)
    End Sub

    Public Sub DeleteDoctor(doctorId As Integer)
        If doctorId <= 0 Then
            Throw New ArgumentException("Please select a valid doctor to delete.")
        End If

        doctorDal.Delete(doctorId)
    End Sub

    Private Shared Sub ValidateDoctor(doctor As Doctor, requireDoctorId As Boolean)
        If doctor Is Nothing Then
            Throw New ArgumentNullException(NameOf(doctor))
        End If

        If requireDoctorId AndAlso doctor.DoctorID <= 0 Then
            Throw New ArgumentException("Invalid doctor ID.")
        End If

        If doctor.UserID <= 0 Then
            Throw New ArgumentException("Please enter a valid user ID.")
        End If

        If doctor.BranchID <= 0 Then
            Throw New ArgumentException("Please select a branch.")
        End If

        If String.IsNullOrWhiteSpace(doctor.FirstName) OrElse String.IsNullOrWhiteSpace(doctor.LastName) Then
            Throw New ArgumentException("Please enter first name and last name.")
        End If

        If Not String.IsNullOrWhiteSpace(doctor.Phone) AndAlso Not doctor.Phone.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("Phone number must contain digits only.")
        End If

        doctor.FirstName = doctor.FirstName.Trim()
        doctor.LastName = doctor.LastName.Trim()
        doctor.Phone = If(doctor.Phone, String.Empty).Trim()
        doctor.Email = If(doctor.Email, String.Empty).Trim()
        doctor.LicenseNo = If(doctor.LicenseNo, String.Empty).Trim()
    End Sub
End Class
