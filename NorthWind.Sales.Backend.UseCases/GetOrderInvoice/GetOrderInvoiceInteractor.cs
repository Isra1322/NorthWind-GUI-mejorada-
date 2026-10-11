namespace NorthWind.Sales.Backend.UseCases.GetOrderInvoice;

internal class GetOrderInvoiceInteractor(
    IGetOrderInvoiceOutputPort outputPort,
    IQueriesRepository queriesRepository) : IGetOrderInvoiceInputPort
{
    public async Task Handle(int orderId)
    {
        var invoice = await queriesRepository.GetOrderInvoice(orderId);
        await outputPort.Handle(invoice);
    }
}
