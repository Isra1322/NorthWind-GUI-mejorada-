namespace NorthWind.Membership.Backend.Core.Dtos.UserLogin;

public class UserDto(string email, string firstName, string lastName)
{
    public string Email => email;
    public string FirstName => firstName;
    public string LastName => lastName;
}
