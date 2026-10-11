namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddSmtpGateways(
        this IServiceCollection services,
        Action<SmtpOptions> configureSmtpOptions)
    {
        services.AddOptions<SmtpOptions>()
            .Configure(configureSmtpOptions);

        services.AddDomainEventHandler<SpecialOrderCreatedEvent, SpecialOrderCreatedEventHandler>();
        return services;
    }
}
