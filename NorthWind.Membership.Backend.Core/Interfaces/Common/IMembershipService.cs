using NorthWind.Membership.Backend.Core.Dtos.UserLogin;
using NorthWind.Membership.Entities.Dtos.UserLogin;

namespace NorthWind.Membership.Backend.Core.Interfaces.Common;

public interface IMembershipService
{
    Task<Result<IEnumerable<ValidationError>>> Register(UserRegistrationDto userData);
    Task<UserDto?> GetUserByCredentials(UserCredentialsDto userData);
}
