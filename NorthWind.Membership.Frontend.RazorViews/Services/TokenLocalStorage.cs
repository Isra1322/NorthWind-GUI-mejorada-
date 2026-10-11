using System.Text.Json;
using Microsoft.JSInterop;
using NorthWind.Membership.Entities.Dtos.UserLogin;
using NorthWind.Membership.Frontend.RazorViews.Interfaces;

namespace NorthWind.Membership.Frontend.RazorViews.Services;

internal class TokenLocalStorage(IJSRuntime jsRuntime) : ITokenStorage
{
    private const string StorageKey = "tokens";
    private const string StoreIdentifier = "localStorage.setItem";
    private const string GetIdentifier = "localStorage.getItem";
    private const string RemoveIdentifier = "localStorage.removeItem";

    public async Task StoreTokensAsync(TokensDto tokens)
    {
        string value = JsonSerializer.Serialize(tokens);
        await jsRuntime.InvokeVoidAsync(StoreIdentifier, StorageKey, value);
    }

    public async Task<TokensDto?> GetTokensAsync()
    {
        string? value = await jsRuntime.InvokeAsync<string?>(GetIdentifier, StorageKey);
        return value == null ? null : JsonSerializer.Deserialize<TokensDto>(value);
    }

    public async Task RemoveTokensAsync()
    {
        await jsRuntime.InvokeVoidAsync(RemoveIdentifier, StorageKey);
    }
}
