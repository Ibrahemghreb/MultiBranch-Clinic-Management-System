Public Class PatientListItem
    Public Property ID As Integer
    Public Property DisplayText As String

    Public Sub New(id As Integer, displayText As String)
        Me.ID = id
        Me.DisplayText = displayText
    End Sub

    Public Overrides Function ToString() As String
        Return DisplayText
    End Function
End Class
