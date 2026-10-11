namespace NorthWind.Membership.Frontend.RazorViews.ViewModels;

internal class UserRegistrationViewModelValidator(
    IModelValidatorHub<UserRegistrationDto> validator) :
    AbstractViewModelValidator<UserRegistrationDto, UserRegistrationViewModel>(validator,
        ValidationConstraint.AlwaysValidate)
{
}
