Imports System.Data
Imports System.Data.SqlClient

Public Class BranchDAL
    Private Const SelectColumns As String =
        "BranchID, BranchName, Address, Phone, Email, OpenTime, CloseTime, IsActive"

    Public Function GetAll() As DataTable
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Branches ORDER BY BranchName"
            Using adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetById(branchId As Integer) As Branch
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Branches WHERE BranchID = @BranchID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapBranch(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function BranchNameExists(branchName As String, Optional excludeBranchId As Integer = 0) As Boolean
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Branches WHERE BranchName = @BranchName"
            If excludeBranchId > 0 Then
                query &= " AND BranchID <> @BranchID"
            End If

            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@BranchName", SqlDbType.NVarChar, 100).Value = branchName
                If excludeBranchId > 0 Then
                    command.Parameters.Add("@BranchID", SqlDbType.Int).Value = excludeBranchId
                End If

                connection.Open()
                Return CInt(command.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(branch As Branch)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Branches (BranchName, Address, Phone, Email, OpenTime, CloseTime, IsActive) " &
                "VALUES (@BranchName, @Address, @Phone, @Email, @OpenTime, @CloseTime, @IsActive)"
            Using command As New SqlCommand(query, connection)
                AddBranchParameters(command, branch, includeId:=False)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(branch As Branch)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Branches SET BranchName = @BranchName, Address = @Address, Phone = @Phone, " &
                "Email = @Email, OpenTime = @OpenTime, CloseTime = @CloseTime, IsActive = @IsActive " &
                "WHERE BranchID = @BranchID"
            Using command As New SqlCommand(query, connection)
                AddBranchParameters(command, branch, includeId:=True)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(branchId As Integer)
        Using connection As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Branches WHERE BranchID = @BranchID"
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Sub AddBranchParameters(command As SqlCommand, branch As Branch, includeId As Boolean)
        If includeId Then
            command.Parameters.Add("@BranchID", SqlDbType.Int).Value = branch.BranchID
        End If

        command.Parameters.Add("@BranchName", SqlDbType.NVarChar, 100).Value = branch.BranchName
        command.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = ToDbValue(branch.Address)
        command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = ToDbValue(branch.Phone)
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDbValue(branch.Email)
        command.Parameters.Add("@OpenTime", SqlDbType.Time).Value = branch.OpenTime.TimeOfDay
        command.Parameters.Add("@CloseTime", SqlDbType.Time).Value = branch.CloseTime.TimeOfDay
        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = branch.IsActive
    End Sub

    Private Shared Function ToDbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function

    Private Shared Function MapBranch(reader As SqlDataReader) As Branch
        Dim openTime As DateTime = Today.AddHours(9)
        Dim closeTime As DateTime = Today.AddHours(21)

        If Not IsDBNull(reader("OpenTime")) Then
            openTime = Today.Add(CType(reader("OpenTime"), TimeSpan))
        End If

        If Not IsDBNull(reader("CloseTime")) Then
            closeTime = Today.Add(CType(reader("CloseTime"), TimeSpan))
        End If

        Return New Branch With {
            .BranchID = CInt(reader("BranchID")),
            .BranchName = reader("BranchName").ToString(),
            .Address = If(IsDBNull(reader("Address")), "", reader("Address").ToString()),
            .Phone = If(IsDBNull(reader("Phone")), "", reader("Phone").ToString()),
            .Email = If(IsDBNull(reader("Email")), "", reader("Email").ToString()),
            .OpenTime = openTime,
            .CloseTime = closeTime,
            .IsActive = CBool(reader("IsActive"))
        }
    End Function
End Class
