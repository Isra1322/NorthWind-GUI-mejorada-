namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.GetOrdersByCustomer;

public interface IGetOrdersByCustomerOutputPort
{
    IEnumerable<OrderInvoiceDto>? Invoices { get; }
    Task Handle(IEnumerable<OrderInvoiceDto> invoices);
}
