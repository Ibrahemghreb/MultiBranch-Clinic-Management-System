Imports System.Data
Imports System.Linq

Public Class BranchBAL
    Private ReadOnly branchDal As BranchDAL

    Public Sub New()
        Me.New(New BranchDAL())
    End Sub

    Public Sub New(repository As BranchDAL)
        branchDal = repository
    End Sub

    Public Function GetAllBranches() As DataTable
        Return branchDal.GetAll()
    End Function

    Public Function GetBranchById(branchId As Integer) As Branch
        If branchId <= 0 Then
            Throw New ArgumentException("Invalid branch ID.")
        End If

        Return branchDal.GetById(branchId)
    End Function

    Public Sub AddBranch(branch As Branch)
        ValidateBranch(branch, requireBranchId:=False)

        If branchDal.BranchNameExists(branch.BranchName) Then
            Throw New InvalidOperationException("Branch name already exists.")
        End If

        branchDal.Insert(branch)
    End Sub

    Public Sub UpdateBranch(branch As Branch)
        ValidateBranch(branch, requireBranchId:=True)

        If branchDal.BranchNameExists(branch.BranchName, branch.BranchID) Then
            Throw New InvalidOperationException("Branch name already exists for another branch.")
        End If

        branchDal.Update(branch)
    End Sub

    Public Sub DeleteBranch(branchId As Integer)
        If branchId <= 0 Then
            Throw New ArgumentException("Please select a valid branch to delete.")
        End If

        branchDal.Delete(branchId)
    End Sub

    Private Shared Sub ValidateBranch(branch As Branch, requireBranchId As Boolean)
        If branch Is Nothing Then
            Throw New ArgumentNullException(NameOf(branch))
        End If

        If requireBranchId AndAlso branch.BranchID <= 0 Then
            Throw New ArgumentException("Invalid branch ID.")
        End If

        If String.IsNullOrWhiteSpace(branch.BranchName) Then
            Throw New ArgumentException("Please enter branch name.")
        End If

        If String.IsNullOrWhiteSpace(branch.Phone) Then
            Throw New ArgumentException("Please enter phone number.")
        End If

        If Not branch.Phone.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("Phone number must contain digits only.")
        End If

        If branch.OpenTime.TimeOfDay >= branch.CloseTime.TimeOfDay Then
            Throw New ArgumentException("Close time must be after open time.")
        End If

        branch.BranchName = branch.BranchName.Trim()
        branch.Address = If(branch.Address, String.Empty).Trim()
        branch.Phone = branch.Phone.Trim()
        branch.Email = If(branch.Email, String.Empty).Trim()
    End Sub
End Class
