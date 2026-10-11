namespace NorthWind.Sales.Frontend.WebApiGateways;

internal class OrderInvoiceGateway(HttpClient client) : IOrderInvoiceGateway
{
    public async Task<OrderInvoiceDto?> GetOrderInvoiceByIdAsync(int orderId)
    {
        try
        {
            return await client.GetFromJsonAsync<OrderInvoiceDto>($"/order/{orderId}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<OrderInvoiceDto>> GetOrdersByCustomerIdAsync(string customerId)
    {
        try
        {
            var result = await client.GetFromJsonAsync<IEnumerable<OrderInvoiceDto>>(
                $"/order/customer/{Uri.EscapeDataString(customerId)}");
            return result ?? [];
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return [];
        }
    }
}
