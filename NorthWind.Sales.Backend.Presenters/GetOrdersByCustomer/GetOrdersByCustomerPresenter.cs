namespace NorthWind.Sales.Backend.Presenters.GetOrdersByCustomer;

internal class GetOrdersByCustomerPresenter : IGetOrdersByCustomerOutputPort
{
    public IEnumerable<OrderInvoiceDto>? Invoices { get; private set; }

    public Task Handle(IEnumerable<OrderInvoiceDto> invoices)
    {
        Invoices = invoices;
        return Task.CompletedTask;
    }
}
