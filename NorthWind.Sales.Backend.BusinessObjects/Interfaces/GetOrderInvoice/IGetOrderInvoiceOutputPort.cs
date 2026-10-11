namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.GetOrderInvoice;

public interface IGetOrderInvoiceOutputPort
{
    OrderInvoiceDto? Invoice { get; }
    Task Handle(OrderInvoiceDto? invoice);
}
