using NorthWind.Membership.Entities.Dtos.UserLogin;

namespace NorthWind.Membership.Frontend.RazorViews.Interfaces;

public interface IMembershipGateway
{
    Task RegisterAsync(UserRegistrationDto userData);
    Task<TokensDto> LoginAsync(UserCredentialsDto userCredentials);
}
