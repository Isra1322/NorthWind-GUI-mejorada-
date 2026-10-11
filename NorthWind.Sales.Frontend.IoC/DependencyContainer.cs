using NorthWind.Membership.Frontend.RazorViews;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices(
        this IServiceCollection services,
        Action<HttpClient> configureHttpClient,
        Action<IHttpClientBuilder>? configureHttpClientBuilder = null) =>
        services.AddNorthWindSalesServices(configureHttpClient, configureHttpClient, configureHttpClientBuilder);

    public static IServiceCollection AddNorthWindSalesServices(
        this IServiceCollection services,
        Action<HttpClient> configureSalesHttpClient,
        Action<HttpClient> configureMembershipHttpClient,
        Action<IHttpClientBuilder>? configureHttpClientBuilder = null)
    {
        services.AddWebApiGateways(configureSalesHttpClient, configureHttpClientBuilder)
            .AddViewsServices()
            .AddValidationService()
            .AddValidators()
            .AddMembershipServices(configureMembershipHttpClient);
        return services;
    }
}
