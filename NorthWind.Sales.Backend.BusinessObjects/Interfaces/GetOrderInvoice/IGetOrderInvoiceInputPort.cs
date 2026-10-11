namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.GetOrderInvoice;

public interface IGetOrderInvoiceInputPort
{
    Task Handle(int orderId);
}
