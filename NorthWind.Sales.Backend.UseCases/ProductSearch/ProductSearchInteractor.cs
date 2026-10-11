namespace NorthWind.Sales.Backend.UseCases.ProductSearch;

internal class ProductSearchInteractor(
    IProductSearchOutputPort outputPort,
    IQueriesRepository queriesRepository) : IProductSearchInputPort
{
    public async Task Handle(ProductSearchCriteriaDto criteria)
    {
        var result = await queriesRepository.SearchAvailableProducts(criteria);
        await outputPort.Handle(result);
    }
}
