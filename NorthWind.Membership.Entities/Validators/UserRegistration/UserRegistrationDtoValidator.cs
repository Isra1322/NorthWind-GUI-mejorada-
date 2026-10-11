namespace NorthWind.Membership.Entities.Validators.UserRegistration;

internal class UserRegistrationDtoValidator : AbstractModelValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator(
        IValidationService<UserRegistrationDto> validationService) :
        base(validationService)
    {
        AddRuleFor(u => u.FirstName)
            .NotEmpty("El nombre es requerido.");

        AddRuleFor(u => u.LastName)
            .NotEmpty("El apellido es requerido.");

        AddRuleFor(u => u.Email)
            .NotEmpty("El correo es requerido.")
            .EmailAddress("El formato de correo no es válido.");

        AddRuleFor(u => u.Password)
            .StopOnFirstFailure()
            .NotEmpty("La contraseña es requerida.")
            .MinimumLength(6, "Se requiere al menos 6 caracteres en la contraseña.")
            .Must(p => p.Any(char.IsLower), "Se requiere al menos un caracter minúscula en la contraseña.")
            .Must(p => p.Any(char.IsUpper), "Se requiere al menos un caracter mayúscula en la contraseña.")
            .Must(p => p.Any(char.IsDigit), "Se requiere al menos un dígito en la contraseña.")
            .Must(p => p.Any(c => !char.IsLetterOrDigit(c)), "Se requiere al menos un caracter no alfanumérico en la contraseña.");
    }
}
