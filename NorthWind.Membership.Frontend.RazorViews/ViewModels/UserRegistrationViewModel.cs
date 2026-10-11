using NorthWind.Membership.Frontend.RazorViews.Interfaces;
using NorthWind.RazorComponents.Validators;

namespace NorthWind.Membership.Frontend.RazorViews.ViewModels;

public class UserRegistrationViewModel(
    IMembershipGateway gateway,
    IModelValidatorHub<UserRegistrationViewModel> validator)
{
    public IModelValidatorHub<UserRegistrationViewModel> Validator => validator;
    public ModelValidator<UserRegistrationViewModel>? ModelValidatorComponentReference { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string InformationMessage { get; private set; } = string.Empty;

    public async Task Register()
    {
        InformationMessage = string.Empty;
        try
        {
            await gateway.RegisterAsync((UserRegistrationDto)this);
            InformationMessage = "Usuario registrado exitosamente.";
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

    public static explicit operator UserRegistrationDto(UserRegistrationViewModel model) =>
        new UserRegistrationDto(model.FirstName, model.LastName, model.Email, model.Password);
}
