namespace NorthWind.Sales.Frontend.BusinessObjects.Interfaces;

public interface ICustomerGateway
{
    Task<PagedResult<CustomerSearchDto>?> SearchCustomersAsync(
        string? searchTerm = null,
        string? customerId = null,
        string? companyName = null,
        string? contactName = null,
        int page = 1,
        int pageSize = 10);
}
