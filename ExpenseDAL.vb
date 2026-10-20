Imports System.Data
Imports System.Data.SqlClient

Public Class ExpenseDAL
    Public Function GetAll() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "SELECT e.ExpenseID, e.BranchID, b.BranchName, e.CategoryID, c.CategoryName, e.Amount, e.ExpenseDate, e.Description " &
                "FROM dbo.Expenses e " &
                "INNER JOIN dbo.Branches b ON b.BranchID = e.BranchID " &
                "LEFT JOIN dbo.ExpenseCategories c ON c.CategoryID = e.CategoryID " &
                "ORDER BY e.ExpenseDate DESC"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetBranches() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT BranchID, BranchName FROM dbo.Branches WHERE IsActive = 1 ORDER BY BranchName"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetCategories() As DataTable
        Dim dt As New DataTable()
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "SELECT CategoryID, CategoryName FROM dbo.ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName"
            Using cmd As New SqlCommand(query, conn)
                conn.Open()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Sub Insert(branchId As Integer, categoryId As Integer, amount As Decimal, expenseDate As Date, description As String)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Expenses (BranchID, CategoryID, Amount, ExpenseDate, Description, CreatedBy) " &
                "VALUES (@BranchID, @CategoryID, @Amount, @ExpenseDate, @Description, @CreatedBy)"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = branchId
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = If(categoryId > 0, CObj(categoryId), DBNull.Value)
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = amount
                cmd.Parameters("@Amount").Precision = 18
                cmd.Parameters("@Amount").Scale = 2
                cmd.Parameters.Add("@ExpenseDate", SqlDbType.Date).Value = expenseDate.Date
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value = If(String.IsNullOrWhiteSpace(description), CObj(DBNull.Value), description.Trim())
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = If(SessionManager.UserID > 0, CObj(SessionManager.UserID), DBNull.Value)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
