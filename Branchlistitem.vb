Public Class BranchListItem
    Public Property BranchID As Integer
    Public Property BranchName As String

    Public ReadOnly Property DisplayText As String
        Get
            Return $"{BranchID} - {BranchName}"
        End Get
    End Property
End Class