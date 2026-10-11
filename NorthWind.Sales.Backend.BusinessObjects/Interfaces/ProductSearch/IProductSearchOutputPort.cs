namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.ProductSearch;

public interface IProductSearchOutputPort
{
    PagedResult<ProductSearchDto>? Result { get; }
    Task Handle(PagedResult<ProductSearchDto> result);
}
