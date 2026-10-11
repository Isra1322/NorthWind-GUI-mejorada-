namespace NorthWind.Sales.Frontend.BusinessObjects.Interfaces;

public interface IProductGateway
{
    Task<PagedResult<ProductSearchDto>?> SearchAvailableProductsAsync(
        string? searchTerm = null,
        int? productId = null,
        string? productName = null,
        int page = 1,
        int pageSize = 10);
}
