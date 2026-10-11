using System.Net.Http.Headers;
using NorthWind.Membership.Frontend.RazorViews.Interfaces;

namespace NorthWind.Membership.Frontend.RazorViews.HttpMessageHandlers;

public class MembershipBearerTokenHandler(ITokenStorage storage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var tokens = await storage.GetTokensAsync();
        if (tokens != null && !string.IsNullOrWhiteSpace(tokens.AccessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
