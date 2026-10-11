using NorthWind.Membership.Entities.Dtos.UserLogin;
using NorthWind.Membership.Frontend.RazorViews.Interfaces;

namespace NorthWind.Membership.Frontend.RazorViews.Gateways;

public class MembershipGateway(HttpClient client) : IMembershipGateway
{
    public async Task RegisterAsync(UserRegistrationDto userData)
    {
        await client.PostAsJsonAsync(Endpoints.Register, userData);
    }

    public async Task<TokensDto> LoginAsync(UserCredentialsDto userCredentials)
    {
        var response = await client.PostAsJsonAsync(Endpoints.Login, userCredentials);
        return (await response.Content.ReadFromJsonAsync<TokensDto>())!;
    }
}
