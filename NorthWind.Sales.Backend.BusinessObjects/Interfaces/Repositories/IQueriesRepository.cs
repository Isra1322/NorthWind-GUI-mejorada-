namespace NorthWind.Sales.Backend.BusinessObjects.Interfaces.Repositories;

public interface IQueriesRepository
{
    Task<decimal?> GetCustomerCurrentBalance(string customerId);
    Task<IEnumerable<ProductUnitsInStock>> GetProductsUnitsInStock(
        IEnumerable<int> productIds);
    Task<PagedResult<CustomerSearchDto>> SearchCustomers(CustomerSearchCriteriaDto criteria);
    Task<PagedResult<ProductSearchDto>> SearchAvailableProducts(ProductSearchCriteriaDto criteria);
    Task<OrderInvoiceDto?> GetOrderInvoice(int orderId);
    Task<IEnumerable<OrderInvoiceDto>> GetOrdersByCustomer(string customerId);
}
