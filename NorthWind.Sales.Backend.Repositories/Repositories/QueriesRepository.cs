namespace NorthWind.Sales.Backend.Repositories.Repositories;

internal class QueriesRepository(INorthWindSalesQueriesDataContext context) :
    IQueriesRepository
{
    public async Task<decimal?> GetCustomerCurrentBalance(string customerId)
    {
        var queryable = context.Customers
            .Where(c => c.Id == customerId)
            .Select(c => new { c.CurrentBalance });
        var result = await context.FirstOrDefaultAync(queryable);
        return result?.CurrentBalance;
    }

    public async Task<IEnumerable<ProductUnitsInStock>>
        GetProductsUnitsInStock(IEnumerable<int> productIds)
    {
        var queryable = context.Products
            .Where(p => productIds.Contains(p.Id))
            .Select(p => new ProductUnitsInStock(
                p.Id, p.UnitsInStock));
        return await context.ToListAsync(queryable);
    }

    public async Task<PagedResult<CustomerSearchDto>> SearchCustomers(CustomerSearchCriteriaDto criteria)
    {
        var query = context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(criteria.SearchTerm))
        {
            string term = criteria.SearchTerm.Trim();
            query = query.Where(c =>
                c.Id.Contains(term) ||
                c.Name.Contains(term) ||
                (c.ContactName != null && c.ContactName.Contains(term)));
        }

        if (!string.IsNullOrWhiteSpace(criteria.CustomerId))
        {
            string id = criteria.CustomerId.Trim();
            query = query.Where(c => c.Id.Contains(id));
        }

        if (!string.IsNullOrWhiteSpace(criteria.CompanyName))
        {
            string company = criteria.CompanyName.Trim();
            query = query.Where(c => c.Name.Contains(company));
        }

        if (!string.IsNullOrWhiteSpace(criteria.ContactName))
        {
            string contact = criteria.ContactName.Trim();
            query = query.Where(c => c.ContactName != null && c.ContactName.Contains(contact));
        }

        int totalCount = await context.CountAsync(query);

        var pagedQuery = query
            .OrderBy(c => c.Name)
            .Skip((criteria.Page - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .Select(c => new CustomerSearchDto(
                c.Id,
                c.Name,
                c.ContactName,
                c.Address,
                c.City,
                c.Country,
                c.PostalCode));

        var items = (await context.ToListAsync(pagedQuery)).ToList();

        return new PagedResult<CustomerSearchDto>(items, totalCount, criteria.Page, criteria.PageSize);
    }

    public async Task<PagedResult<ProductSearchDto>> SearchAvailableProducts(ProductSearchCriteriaDto criteria)
    {
        var query = context.Products.Where(p => p.UnitsInStock > 0);

        if (criteria.ProductId.HasValue)
        {
            query = query.Where(p => p.Id == criteria.ProductId.Value);
        }

        if (!string.IsNullOrWhiteSpace(criteria.ProductName))
        {
            string name = criteria.ProductName.Trim();
            query = query.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(criteria.SearchTerm))
        {
            string term = criteria.SearchTerm.Trim();
            if (int.TryParse(term, out int parsedId))
            {
                query = query.Where(p => p.Id == parsedId || p.Name.Contains(term));
            }
            else
            {
                query = query.Where(p => p.Name.Contains(term));
            }
        }

        int totalCount = await context.CountAsync(query);

        var pagedQuery = query
            .OrderBy(p => p.Name)
            .Skip((criteria.Page - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .Select(p => new ProductSearchDto(
                p.Id,
                p.Name,
                p.UnitPrice,
                p.UnitsInStock));

        var items = (await context.ToListAsync(pagedQuery)).ToList();

        return new PagedResult<ProductSearchDto>(items, totalCount, criteria.Page, criteria.PageSize);
    }

    public async Task<OrderInvoiceDto?> GetOrderInvoice(int orderId)
    {
        var orderQuery = context.Orders.Where(o => o.Id == orderId);
        var order = await context.FirstOrDefaultAync(orderQuery);
        if (order == null)
        {
            return null;
        }

        var customerQuery = context.Customers.Where(c => c.Id == order.CustomerId);
        var customer = await context.FirstOrDefaultAync(customerQuery);

        var detailsQuery = from d in context.OrderDetails
                           where d.OrderId == orderId
                           join p in context.Products on d.ProductId equals p.Id
                           select new OrderInvoiceDetailDto(
                               d.ProductId,
                               p.Name,
                               d.UnitPrice,
                               d.Quantity);

        var details = (await context.ToListAsync(detailsQuery)).ToList();
        decimal subtotal = details.Sum(d => d.Total);

        return new OrderInvoiceDto(
            order.Id,
            order.OrderDate,
            order.CustomerId,
            customer?.Name ?? order.CustomerId,
            customer?.ContactName,
            order.ShipAddress,
            order.ShipCity,
            order.ShipCountry,
            order.ShipPostalCode,
            details,
            subtotal,
            0.15m);
    }

    public async Task<IEnumerable<OrderInvoiceDto>> GetOrdersByCustomer(string customerId)
    {
        var ordersQuery = context.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.OrderDate);

        var orders = (await context.ToListAsync(ordersQuery)).ToList();
        if (orders.Count == 0)
        {
            return [];
        }

        var customerQuery = context.Customers.Where(c => c.Id == customerId);
        var customer = await context.FirstOrDefaultAync(customerQuery);

        var orderIds = orders.Select(o => o.Id).ToList();
        var detailsQuery = from d in context.OrderDetails
                           where orderIds.Contains(d.OrderId)
                           join p in context.Products on d.ProductId equals p.Id
                           select new
                           {
                               d.OrderId,
                               Detail = new OrderInvoiceDetailDto(
                                   d.ProductId,
                                   p.Name,
                                   d.UnitPrice,
                                   d.Quantity)
                           };

        var allDetails = (await context.ToListAsync(detailsQuery)).ToList();
        var detailsGrouped = allDetails.GroupBy(x => x.OrderId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Detail).ToList());

        var result = new List<OrderInvoiceDto>();
        foreach (var order in orders)
        {
            var details = detailsGrouped.TryGetValue(order.Id, out var detailList)
                ? detailList
                : [];
            decimal subtotal = details.Sum(d => d.Total);

            result.Add(new OrderInvoiceDto(
                order.Id,
                order.OrderDate,
                order.CustomerId,
                customer?.Name ?? order.CustomerId,
                customer?.ContactName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipCountry,
                order.ShipPostalCode,
                details,
                subtotal,
                0.15m));
        }

        return result;
    }
}
