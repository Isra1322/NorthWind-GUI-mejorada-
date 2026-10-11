namespace NorthWind.Sales.Entities.Dtos.OrderInvoice;

public class OrderInvoiceDetailDto(
    int productId,
    string productName,
    decimal unitPrice,
    short quantity)
{
    public int ProductId => productId;
    public string ProductName => productName;
    public decimal UnitPrice => unitPrice;
    public short Quantity => quantity;
    public decimal Total => Math.Round(UnitPrice * Quantity, 2);
}
