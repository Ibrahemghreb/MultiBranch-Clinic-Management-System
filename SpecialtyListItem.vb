Public Class SpecialtyListItem
    Public Property SpecialtyID As Integer
    Public Property SpecialtyName As String

    Public ReadOnly Property DisplayText As String
        Get
            Return SpecialtyName
        End Get
    End Property
End Class
