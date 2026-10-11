namespace NorthWind.Sales.Entities.Dtos.ProductSearch;

public class ProductSearchCriteriaDto(
    int? productId = null,
    string? productName = null,
    string? searchTerm = null,
    int page = 1,
    int pageSize = 10)
{
    public int? ProductId => productId;
    public string? ProductName => productName;
    public string? SearchTerm => searchTerm;
    public int Page => page <= 0 ? 1 : page;
    public int PageSize => pageSize <= 0 ? 10 : pageSize;
}
