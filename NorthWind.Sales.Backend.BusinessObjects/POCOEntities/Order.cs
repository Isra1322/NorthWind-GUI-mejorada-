namespace NorthWind.Sales.Backend.BusinessObjects.POCOEntities;

public class Order
{
    public int Id { get; set; }
    public string CustomerId { get; set; } = default!;
    public string ShipAddress { get; set; } = default!;
    public string ShipCity { get; set; } = default!;
    public string ShipCountry { get; set; } = default!;
    public string ShipPostalCode { get; set; } = default!;
    public ShippingType ShippingType { get; set; } = ShippingType.Road;
    public DiscountType DiscountType { get; set; } = DiscountType.Percentage;
    public double Discount { get; set; } = 10;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}
