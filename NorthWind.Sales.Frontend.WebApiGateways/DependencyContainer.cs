namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddWebApiGateways(
        this IServiceCollection services,
        Action<HttpClient> configureHttpClient,
        Action<IHttpClientBuilder>? configureHttpClientBuilder = null)
    {
        services.AddExceptionDelegatingHandler();

        var orderBuilder = services.AddHttpClient<ICreateOrderGateway, CreateOrderGateway>(configureHttpClient)
            .AddHttpMessageHandler<ExceptionDelegatingHandler>();
        configureHttpClientBuilder?.Invoke(orderBuilder);

        var customerBuilder = services.AddHttpClient<ICustomerGateway, CustomerGateway>(configureHttpClient)
            .AddHttpMessageHandler<ExceptionDelegatingHandler>();
        configureHttpClientBuilder?.Invoke(customerBuilder);

        var productBuilder = services.AddHttpClient<IProductGateway, ProductGateway>(configureHttpClient)
            .AddHttpMessageHandler<ExceptionDelegatingHandler>();
        configureHttpClientBuilder?.Invoke(productBuilder);

        var invoiceBuilder = services.AddHttpClient<IOrderInvoiceGateway, OrderInvoiceGateway>(configureHttpClient)
            .AddHttpMessageHandler<ExceptionDelegatingHandler>();
        configureHttpClientBuilder?.Invoke(invoiceBuilder);

        return services;
    }
}
