Imports System.Data
Imports System.Data.SqlClient

Public Class InventoryDAL
    Private Const SelectColumns As String =
        "InventoryID, ItemCode, ItemName, Category, QuantityInStock, UnitOfMeasure, " &
        "PurchasePrice, SellingPrice, MinStockLevel, SupplierName, Notes, IsActive, CreatedAt"

    Public Function GetAll() As DataTable
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Inventory ORDER BY ItemName"
            Using adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function GetById(id As Integer) As InventoryItem
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = $"SELECT {SelectColumns} FROM dbo.Inventory WHERE InventoryID = @ID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If Not reader.Read() Then Return Nothing
                    Return MapItem(reader)
                End Using
            End Using
        End Using
    End Function

    Public Function CodeExists(itemCode As String, Optional excludeId As Integer = 0) As Boolean
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Dim query As String = "SELECT COUNT(*) FROM dbo.Inventory WHERE ItemCode = @Code"
            If excludeId > 0 Then query &= " AND InventoryID <> @ID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = itemCode
                If excludeId > 0 Then cmd.Parameters.Add("@ID", SqlDbType.Int).Value = excludeId
                conn.Open()
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Sub Insert(item As InventoryItem)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "INSERT INTO dbo.Inventory (ItemCode, ItemName, Category, QuantityInStock, UnitOfMeasure, " &
                "PurchasePrice, SellingPrice, MinStockLevel, SupplierName, Notes, IsActive) " &
                "OUTPUT INSERTED.InventoryID " &
                "VALUES (@Code, @Name, @Category, @Qty, @UOM, @Purchase, @Selling, @MinStock, @Supplier, @Notes, @Active)"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = item.ItemCode
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = item.ItemName
                cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 100).Value = ToDb(item.Category)
                cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = item.QuantityInStock
                cmd.Parameters.Add("@UOM", SqlDbType.NVarChar, 30).Value = item.UnitOfMeasure
                cmd.Parameters.Add("@Purchase", SqlDbType.Decimal).Value = item.PurchasePrice
                cmd.Parameters.Add("@Selling", SqlDbType.Decimal).Value = item.SellingPrice
                cmd.Parameters.Add("@MinStock", SqlDbType.Decimal).Value = item.MinStockLevel
                cmd.Parameters.Add("@Supplier", SqlDbType.NVarChar, 200).Value = ToDb(item.SupplierName)
                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = ToDb(item.Notes)
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = item.IsActive
                conn.Open()
                item.InventoryID = CInt(cmd.ExecuteScalar())
            End Using
        End Using
    End Sub

    Public Sub Update(item As InventoryItem)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String =
                "UPDATE dbo.Inventory SET ItemCode = @Code, ItemName = @Name, Category = @Category, " &
                "QuantityInStock = @Qty, UnitOfMeasure = @UOM, PurchasePrice = @Purchase, " &
                "SellingPrice = @Selling, MinStockLevel = @MinStock, SupplierName = @Supplier, " &
                "Notes = @Notes, IsActive = @Active, UpdatedAt = GETDATE() WHERE InventoryID = @ID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = item.InventoryID
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = item.ItemCode
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = item.ItemName
                cmd.Parameters.Add("@Category", SqlDbType.NVarChar, 100).Value = ToDb(item.Category)
                cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = item.QuantityInStock
                cmd.Parameters.Add("@UOM", SqlDbType.NVarChar, 30).Value = item.UnitOfMeasure
                cmd.Parameters.Add("@Purchase", SqlDbType.Decimal).Value = item.PurchasePrice
                cmd.Parameters.Add("@Selling", SqlDbType.Decimal).Value = item.SellingPrice
                cmd.Parameters.Add("@MinStock", SqlDbType.Decimal).Value = item.MinStockLevel
                cmd.Parameters.Add("@Supplier", SqlDbType.NVarChar, 200).Value = ToDb(item.SupplierName)
                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = ToDb(item.Notes)
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = item.IsActive
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer)
        Using conn As SqlConnection = DbConnectionFactory.CreateConnection()
            Const query As String = "DELETE FROM dbo.Inventory WHERE InventoryID = @ID"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Shared Function ToDb(value As String) As Object
        Return If(String.IsNullOrWhiteSpace(value), DBNull.Value, value.Trim())
    End Function

    Private Shared Function MapItem(reader As SqlDataReader) As InventoryItem
        Return New InventoryItem With {
            .InventoryID = CInt(reader("InventoryID")),
            .ItemCode = reader("ItemCode").ToString(),
            .ItemName = reader("ItemName").ToString(),
            .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
            .QuantityInStock = CDec(reader("QuantityInStock")),
            .UnitOfMeasure = reader("UnitOfMeasure").ToString(),
            .PurchasePrice = CDec(reader("PurchasePrice")),
            .SellingPrice = CDec(reader("SellingPrice")),
            .MinStockLevel = CDec(reader("MinStockLevel")),
            .SupplierName = If(IsDBNull(reader("SupplierName")), "", reader("SupplierName").ToString()),
            .Notes = If(IsDBNull(reader("Notes")), "", reader("Notes").ToString()),
            .IsActive = CBool(reader("IsActive"))
        }
    End Function
End Class