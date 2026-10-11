namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddDomainEventHub(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IDomainEventHub<>), typeof(DomainEventHub<>));
        return services;
    }

    public static IServiceCollection AddDomainEventHandler<TEventType, TEventHandler>(
        this IServiceCollection services)
        where TEventType : IDomainEvent
        where TEventHandler : class, IDomainEventHandler<TEventType>
    {
        services.AddScoped<IDomainEventHandler<TEventType>, TEventHandler>();
        return services;
    }
}
