namespace NorthWind.Sales.Backend.UseCases.CustomerSearch;

internal class CustomerSearchInteractor(
    ICustomerSearchOutputPort outputPort,
    IQueriesRepository queriesRepository) : ICustomerSearchInputPort
{
    public async Task Handle(CustomerSearchCriteriaDto criteria)
    {
        var result = await queriesRepository.SearchCustomers(criteria);
        await outputPort.Handle(result);
    }
}
