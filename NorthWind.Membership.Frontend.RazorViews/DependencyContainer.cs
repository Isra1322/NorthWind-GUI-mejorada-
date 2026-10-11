using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NorthWind.HttpDelegatingHandlers;
using NorthWind.Membership.Entities;
using NorthWind.Membership.Frontend.RazorViews.AuthenticationStateProviders;
using NorthWind.Membership.Frontend.RazorViews.Gateways;
using NorthWind.Membership.Frontend.RazorViews.HttpMessageHandlers;
using NorthWind.Membership.Frontend.RazorViews.Interfaces;
using NorthWind.Membership.Frontend.RazorViews.Services;
using NorthWind.Membership.Frontend.RazorViews.ViewModels;

namespace NorthWind.Membership.Frontend.RazorViews;

public static class DependencyContainer
{
    public static IServiceCollection AddMembershipServices(
        this IServiceCollection services,
        Action<HttpClient> configureHttpClient)
    {
        services.AddAuthorizationCore();

        services.AddExceptionDelegatingHandler();

        services.AddHttpClient<IMembershipGateway, MembershipGateway>(configureHttpClient)
            .AddHttpMessageHandler<ExceptionDelegatingHandler>();

        services.AddScoped(sp => (MembershipGateway)sp.GetRequiredService<IMembershipGateway>());

        services.AddMembershipValidators();

        services.AddScoped<ITokenStorage, TokenLocalStorage>();
        services.AddScoped<JWTAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<JWTAuthenticationStateProvider>());

        services.AddScoped<UserRegistrationViewModel>();
        services.AddModelValidator<UserRegistrationViewModel, UserRegistrationViewModelValidator>();

        services.AddScoped<UserLoginViewModel>();
        services.AddModelValidator<UserLoginViewModel, UserLoginViewModelDtoValidator>();

        return services;
    }

    public static IHttpClientBuilder AddMembershipBearerTokenHandler(
        this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<MembershipBearerTokenHandler>();
        builder.AddHttpMessageHandler<MembershipBearerTokenHandler>();
        return builder;
    }
}
