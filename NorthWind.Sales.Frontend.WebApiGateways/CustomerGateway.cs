namespace NorthWind.Sales.Frontend.WebApiGateways;

internal class CustomerGateway(HttpClient client) : ICustomerGateway
{
    public async Task<PagedResult<CustomerSearchDto>?> SearchCustomersAsync(
        string? searchTerm = null,
        string? customerId = null,
        string? companyName = null,
        string? contactName = null,
        int page = 1,
        int pageSize = 10)
    {
        var queryParams = new List<string>
        {
            $"page={page}",
            $"pageSize={pageSize}"
        };

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            queryParams.Add($"searchTerm={Uri.EscapeDataString(searchTerm)}");
        }

        if (!string.IsNullOrWhiteSpace(customerId))
        {
            queryParams.Add($"customerId={Uri.EscapeDataString(customerId)}");
        }

        if (!string.IsNullOrWhiteSpace(companyName))
        {
            queryParams.Add($"companyName={Uri.EscapeDataString(companyName)}");
        }

        if (!string.IsNullOrWhiteSpace(contactName))
        {
            queryParams.Add($"contactName={Uri.EscapeDataString(contactName)}");
        }

        string url = $"{Endpoints.CustomerSearch}?{string.Join("&", queryParams)}";
        return await client.GetFromJsonAsync<PagedResult<CustomerSearchDto>>(url);
    }
}
