namespace NorthWind.Membership.Entities.Validators.UserLogin;

public class UserCredentialsDtoValidator : AbstractModelValidator<UserCredentialsDto>
{
    public UserCredentialsDtoValidator(
        IValidationService<UserCredentialsDto> validationService) :
        base(validationService)
    {
        AddRuleFor(u => u.Email)
            .NotEmpty("El correo es requerido.")
            .EmailAddress("El formato de correo no es válido.");

        AddRuleFor(u => u.Password)
            .NotEmpty("La contraseña es requerida.");
    }
}
