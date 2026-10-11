namespace NorthWind.Membership.Backend.AspNetIdentity.Services;

public class MembershipService(UserManager<NorthWindUser> userManager) : IMembershipService
{
    public async Task<Result<IEnumerable<ValidationError>>> Register(UserRegistrationDto userData)
    {
        Result<IEnumerable<ValidationError>> result;

        NorthWindUser user = new()
        {
            UserName = userData.Email,
            Email = userData.Email,
            FirstName = userData.FirstName,
            LastName = userData.LastName
        };

        var identityResult = await userManager.CreateAsync(user, userData.Password);
        if (identityResult.Succeeded)
        {
            result = new Result<IEnumerable<ValidationError>>();
        }
        else
        {
            var errors = identityResult.Errors.Select(e => new ValidationError(e.Code, e.Description));
            result = new Result<IEnumerable<ValidationError>>(errors);
        }

        return result;
    }

    public async Task<UserDto?> GetUserByCredentials(UserCredentialsDto userData)
    {
        UserDto? foundUser = null;
        var user = await userManager.FindByNameAsync(userData.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, userData.Password))
        {
            foundUser = new UserDto(user.UserName ?? user.Email ?? string.Empty, user.FirstName, user.LastName);
        }
        return foundUser;
    }
}
