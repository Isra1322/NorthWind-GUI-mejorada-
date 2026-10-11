namespace NorthWind.Sales.Entities.Dtos.CustomerSearch;

public class CustomerSearchCriteriaDto(
    string? customerId = null,
    string? companyName = null,
    string? contactName = null,
    string? searchTerm = null,
    int page = 1,
    int pageSize = 10)
{
    public string? CustomerId => customerId;
    public string? CompanyName => companyName;
    public string? ContactName => contactName;
    public string? SearchTerm => searchTerm;
    public int Page => page <= 0 ? 1 : page;
    public int PageSize => pageSize <= 0 ? 10 : pageSize;
}
