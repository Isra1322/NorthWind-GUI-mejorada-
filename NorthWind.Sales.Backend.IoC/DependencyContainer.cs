using NorthWind.Membership.Backend.AspNetIdentity;
using NorthWind.Membership.Backend.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddNorthWindSalesServices(
        this IServiceCollection services,
        Action<DBOptions> configureDBOptions,
        Action<SmtpOptions> configureSmtpOptions,
        Action<DomainLogsOptions> configureDomainLogsOptions,
        Action<MembershipDBOptions> configureMembershipDBOptions,
        Action<JwtOptions> configureJwtOptions)
    {
        services.AddUseCasesServices()
            .AddRepositories()
            .AddDataContexts(configureDBOptions)
            .AddPresenters()
            .AddValidationService()
            .AddValidators()
            .AddValidationExceptionHandler()
            .AddUpdateExceptionHandler()
            .AddUnauthorizedAccessExceptionHandler()
            .AddUnhandledExceptionHandler()
            .AddDomainEventHub()
            .AddSmtpGateways(configureSmtpOptions)
            .AddDomainLogsServices(configureDomainLogsOptions)
            .AddTransactionServices()
            .AddUserServices()
            .AddMembershipCoreServices(configureJwtOptions)
            .AddMembershipIdentityServices(configureMembershipDBOptions);

        return services;
    }
}
