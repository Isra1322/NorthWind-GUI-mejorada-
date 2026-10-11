namespace NorthWind.Sales.Entities.Dtos.ProductSearch;

public class ProductSearchDto(
    int productId,
    string productName,
    decimal unitPrice,
    short unitsInStock)
{
    public int ProductId => productId;
    public string ProductName => productName;
    public decimal UnitPrice => unitPrice;
    public short UnitsInStock => unitsInStock;
}
