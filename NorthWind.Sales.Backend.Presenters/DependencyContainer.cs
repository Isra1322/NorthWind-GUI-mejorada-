namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddPresenters(
        this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderOutputPort,
            CreateOrderPresenter>();
        services.AddScoped<ICustomerSearchOutputPort,
            CustomerSearchPresenter>();
        services.AddScoped<IProductSearchOutputPort,
            ProductSearchPresenter>();
        services.AddScoped<IGetOrderInvoiceOutputPort,
            GetOrderInvoicePresenter>();
        services.AddScoped<IGetOrdersByCustomerOutputPort,
            GetOrdersByCustomerPresenter>();
        return services;
    }
}
