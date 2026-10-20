Public Class InventoryItem
    Public Property InventoryID As Integer
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property Category As String
    Public Property QuantityInStock As Decimal
    Public Property UnitOfMeasure As String
    Public Property PurchasePrice As Decimal
    Public Property SellingPrice As Decimal
    Public Property MinStockLevel As Decimal
    Public Property SupplierName As String
    Public Property Notes As String
    Public Property IsActive As Boolean = True
End Class