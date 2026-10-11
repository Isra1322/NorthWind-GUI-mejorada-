namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyContainer
{
    public static IServiceCollection AddUserServices(
        this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection AddUserServiceFake(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserServiceFake>();
        return services;
    }
}
