namespace NorthWind.Sales.Frontend.Views.ViewModels.CreateOrder;

public class CreateOrderDetailViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; } = 1;
    public short UnitsInStock { get; set; }

    public decimal Total => Math.Round(UnitPrice * Quantity, 2);
    public bool HasStockExceeded => UnitsInStock > 0 && Quantity > UnitsInStock;
    public bool HasStockReached => UnitsInStock > 0 && Quantity >= UnitsInStock;
}
