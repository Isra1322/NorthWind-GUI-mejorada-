namespace NorthWind.Sales.Backend.Presenters.ProductSearch;

internal class ProductSearchPresenter : IProductSearchOutputPort
{
    public PagedResult<ProductSearchDto>? Result { get; private set; }

    public Task Handle(PagedResult<ProductSearchDto> result)
    {
        Result = result;
        return Task.CompletedTask;
    }
}
