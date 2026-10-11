using NorthWind.Membership.Entities.Dtos.UserLogin;
using NorthWind.Membership.Frontend.RazorViews.AuthenticationStateProviders;
using NorthWind.Membership.Frontend.RazorViews.Interfaces;
using NorthWind.RazorComponents.Validators;

namespace NorthWind.Membership.Frontend.RazorViews.ViewModels;

public class UserLoginViewModel(
    IMembershipGateway gateway,
    IModelValidatorHub<UserLoginViewModel> validator,
    JWTAuthenticationStateProvider authenticationStateProvider)
{
    public IModelValidatorHub<UserLoginViewModel> Validator => validator;
    public ModelValidator<UserLoginViewModel>? ModelValidatorComponentReference { get; set; }

    public event Action? OnLogin;

    #region Datos de entrada
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    #endregion

    public async Task Login()
    {
        try
        {
            TokensDto tokens = await gateway.LoginAsync((UserCredentialsDto)this);
            await authenticationStateProvider.LoginAsync(tokens);
            OnLogin?.Invoke();
        }
        catch (HttpRequestException ex)
        {
            if (ex.Data.Contains("Errors"))
            {
                var errors = ex.Data["Errors"] as IEnumerable<ValidationError>;
                if (errors != null && ModelValidatorComponentReference != null)
                {
                    ModelValidatorComponentReference.AddErrors(errors);
                }
            }
            else
            {
                throw;
            }
        }
    }

    public static explicit operator UserCredentialsDto(UserLoginViewModel model) =>
        new UserCredentialsDto(model.Email, model.Password);
}
