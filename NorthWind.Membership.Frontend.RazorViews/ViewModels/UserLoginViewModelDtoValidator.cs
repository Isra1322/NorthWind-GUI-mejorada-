using NorthWind.Membership.Entities.Dtos.UserLogin;

namespace NorthWind.Membership.Frontend.RazorViews.ViewModels;

internal class UserLoginViewModelDtoValidator(
    IModelValidatorHub<UserCredentialsDto> validator) :
    AbstractViewModelValidator<UserCredentialsDto, UserLoginViewModel>(
        validator, ValidationConstraint.AlwaysValidate)
{
}
