namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddDomainLogsServices(
        this IServiceCollection services,
        Action<DomainLogsOptions> configureDomainLogsOptions)
    {
        services.AddOptions<DomainLogsOptions>()
            .Configure(configureDomainLogsOptions);

        services.AddScoped<NorthWindDomainLogsContext>();
        services.AddScoped<IDomainLogger, DomainLogger>();

        return services;
    }
}
