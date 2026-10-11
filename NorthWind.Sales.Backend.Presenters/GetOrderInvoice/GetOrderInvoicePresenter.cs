namespace NorthWind.Sales.Backend.Presenters.GetOrderInvoice;

internal class GetOrderInvoicePresenter : IGetOrderInvoiceOutputPort
{
    public OrderInvoiceDto? Invoice { get; private set; }

    public Task Handle(OrderInvoiceDto? invoice)
    {
        Invoice = invoice;
        return Task.CompletedTask;
    }
}
