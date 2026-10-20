Imports System.Configuration
Imports System.Data.SqlClient

Public NotInheritable Class DbConnectionFactory
    Private Sub New()
    End Sub

    Public Shared Function CreateConnection() As SqlConnection
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ClinicDBConnection").ConnectionString
        Return New SqlConnection(connectionString)
    End Function
End Class