namespace NorthWind.Sales.Backend.UseCases.GetOrdersByCustomer;

internal class GetOrdersByCustomerInteractor(
    IGetOrdersByCustomerOutputPort outputPort,
    IQueriesRepository queriesRepository) : IGetOrdersByCustomerInputPort
{
    public async Task Handle(string customerId)
    {
        var invoices = await queriesRepository.GetOrdersByCustomer(customerId);
        await outputPort.Handle(invoices);
    }
}
