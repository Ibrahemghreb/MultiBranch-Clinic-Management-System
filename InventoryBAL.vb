Imports System.Data

Public Class InventoryBAL
    Private ReadOnly _dal As InventoryDAL

    Public Sub New()
        Me.New(New InventoryDAL())
    End Sub

    Public Sub New(repository As InventoryDAL)
        _dal = repository
    End Sub

    Public Function GetAllItems() As DataTable
        Return _dal.GetAll()
    End Function

    Public Function GetItemById(id As Integer) As InventoryItem
        If id <= 0 Then Throw New ArgumentException("Invalid item ID.")

        Return _dal.GetById(id)
    End Function

    Public Sub AddItem(item As InventoryItem)
        ValidateItem(item, requireId:=False)

        If _dal.CodeExists(item.ItemCode) Then
            Throw New InvalidOperationException("Item code already exists.")
        End If

        _dal.Insert(item)
    End Sub

    Public Sub UpdateItem(item As InventoryItem)
        ValidateItem(item, requireId:=True)

        If _dal.CodeExists(item.ItemCode, item.InventoryID) Then
            Throw New InvalidOperationException("Another item with the same code already exists.")
        End If

        _dal.Update(item)
    End Sub

    Public Sub DeleteItem(id As Integer)
        If id <= 0 Then Throw New ArgumentException("Please select a valid item to delete.")

        _dal.Delete(id)
    End Sub

    Private Shared Sub ValidateItem(item As InventoryItem, requireId As Boolean)
        If item Is Nothing Then Throw New ArgumentNullException(NameOf(item))
        If requireId AndAlso item.InventoryID <= 0 Then Throw New ArgumentException("Invalid item ID.")

        If String.IsNullOrWhiteSpace(item.ItemCode) Then Throw New ArgumentException("يرجى إدخال كود الصنف.")
        If String.IsNullOrWhiteSpace(item.ItemName) Then Throw New ArgumentException("يرجى إدخال اسم الصنف.")
        If String.IsNullOrWhiteSpace(item.UnitOfMeasure) Then Throw New ArgumentException("يرجى إدخال وحدة القياس.")
        If item.QuantityInStock < 0 Then Throw New ArgumentException("الكمية لا يمكن أن تكون سالبة.")
        If item.PurchasePrice < 0 Then Throw New ArgumentException("سعر الشراء لا يمكن أن يكون سالباً.")
        If item.SellingPrice < 0 Then Throw New ArgumentException("سعر البيع لا يمكن أن يكون سالباً.")
        If item.MinStockLevel < 0 Then Throw New ArgumentException("الحد الأدنى لا يمكن أن يكون سالباً.")

        item.ItemCode = item.ItemCode.Trim()
        item.ItemName = item.ItemName.Trim()
        item.UnitOfMeasure = item.UnitOfMeasure.Trim()
        item.Category = If(item.Category, String.Empty).Trim()
        item.SupplierName = If(item.SupplierName, String.Empty).Trim()
        item.Notes = If(item.Notes, String.Empty).Trim()
    End Sub
End Class