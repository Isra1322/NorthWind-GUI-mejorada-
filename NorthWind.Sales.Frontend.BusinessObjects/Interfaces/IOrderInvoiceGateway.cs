namespace NorthWind.Sales.Frontend.BusinessObjects.Interfaces;

public interface IOrderInvoiceGateway
{
    Task<OrderInvoiceDto?> GetOrderInvoiceByIdAsync(int orderId);
    Task<IEnumerable<OrderInvoiceDto>> GetOrdersByCustomerIdAsync(string customerId);
}
