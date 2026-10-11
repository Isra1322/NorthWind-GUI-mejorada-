namespace NorthWind.Sales.Entities.Dtos.OrderInvoice;

public class OrderInvoiceDto(
    int orderId,
    DateTime orderDate,
    string customerId,
    string companyName,
    string? contactName,
    string? address,
    string? city,
    string? country,
    string? postalCode,
    IEnumerable<OrderInvoiceDetailDto> details,
    decimal subtotal,
    decimal vatRate = 0.15m)
{
    public int OrderId => orderId;
    public DateTime OrderDate => orderDate;
    public string CustomerId => customerId;
    public string CompanyName => companyName;
    public string? ContactName => contactName;
    public string? Address => address;
    public string? City => city;
    public string? Country => country;
    public string? PostalCode => postalCode;
    public IEnumerable<OrderInvoiceDetailDto> Details => details;
    public decimal Subtotal => subtotal;
    public decimal VatRate => vatRate;
    public decimal Vat => Math.Round(Subtotal * VatRate, 2);
    public decimal Total => Math.Round(Subtotal + Vat, 2);
}
