namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddUseCasesServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderInputPort, CreateOrderInteractor>();
        services.AddScoped<ICustomerSearchInputPort, CustomerSearchInteractor>();
        services.AddScoped<IProductSearchInputPort, ProductSearchInteractor>();
        services.AddScoped<IGetOrderInvoiceInputPort, GetOrderInvoiceInteractor>();
        services.AddScoped<IGetOrdersByCustomerInputPort, GetOrdersByCustomerInteractor>();
        services.AddModelValidator<CreateOrderDto, CreateOrderCustomerValidator>();
        services.AddModelValidator<CreateOrderDto, CreateOrderProductValidator>();
        return services;
    }
}
