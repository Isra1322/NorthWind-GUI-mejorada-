namespace NorthWind.Membership.Backend.Core.UseCases.UserRegistration;

internal class UserRegistrationInteractor(
    IMembershipService membershipService,
    IUserRegistrationOutputPort presenter,
    IModelValidatorHub<UserRegistrationDto> validationService)
    : IUserRegistrationInputPort
{
    public async Task Handle(UserRegistrationDto userData)
    {
        Result<IEnumerable<ValidationError>> result;
        if (!await validationService.Validate(userData))
        {
            result = new Result<IEnumerable<ValidationError>>(validationService.Errors);
        }
        else
        {
            result = await membershipService.Register(userData);
        }
        await presenter.Handle(result);
    }
}
