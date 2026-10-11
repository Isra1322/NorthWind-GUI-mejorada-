namespace NorthWind.Sales.Frontend.WebApiGateways;

internal class ProductGateway(HttpClient client) : IProductGateway
{
    public async Task<PagedResult<ProductSearchDto>?> SearchAvailableProductsAsync(
        string? searchTerm = null,
        int? productId = null,
        string? productName = null,
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

        if (productId.HasValue)
        {
            queryParams.Add($"productId={productId.Value}");
        }

        if (!string.IsNullOrWhiteSpace(productName))
        {
            queryParams.Add($"productName={Uri.EscapeDataString(productName)}");
        }

        string url = $"{Endpoints.ProductAvailable}?{string.Join("&", queryParams)}";
        return await client.GetFromJsonAsync<PagedResult<ProductSearchDto>>(url);
    }
}
