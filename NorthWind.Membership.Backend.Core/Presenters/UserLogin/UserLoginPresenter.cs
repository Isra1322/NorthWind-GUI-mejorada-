using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Membership.Backend.Core.Dtos.UserLogin;
using NorthWind.Membership.Backend.Core.Interfaces.UserLogin;
using NorthWind.Membership.Backend.Core.Services;
using NorthWind.Membership.Entities.Dtos.UserLogin;
using NorthWind.Result.Entities;
using NorthWind.Validation.Entities.ValueObjects;

namespace NorthWind.Membership.Backend.Core.Presenters.UserLogin;

internal class UserLoginPresenter(JwtService service) : IUserLoginOutputPort
{
    public IResult Result { get; private set; } = default!;

    public Task Handle(Result<UserDto, IEnumerable<ValidationError>> userLoginResult)
    {
        userLoginResult.HandleResult(
            userDto =>
            {
                Result = Results.Ok(new TokensDto(service.GetToken(userDto)));
            },
            errors =>
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Error de inicio de sesión.",
                    Detail = "El correo o la contraseña son incorrectos.",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Instance = $"{nameof(ProblemDetails)}/{nameof(UserLoginPresenter)}"
                };
                problemDetails.Extensions.Add("errors", errors);
                Result = Results.Problem(problemDetails);
            }
        );

        return Task.CompletedTask;
    }
}
