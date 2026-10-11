namespace NorthWind.Membership.Backend.Core.Presenters.UserRegistration;

internal class UserRegistrationPresenter : IUserRegistrationOutputPort
{
    public IResult Result { get; private set; } = default!;

    public Task Handle(Result<IEnumerable<ValidationError>> userRegistrationResult)
    {
        userRegistrationResult.HandleResult(
            errors =>
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Error en los datos de entrada.",
                    Detail = "Se encontraron uno o más errores de validación de datos.",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Instance = $"{nameof(ProblemDetails)}/{nameof(UserRegistrationPresenter)}"
                };
                problemDetails.Extensions.Add("errors", errors);
                Result = Results.Problem(problemDetails);
            },
            () => Result = Results.Ok()
        );
        return Task.CompletedTask;
    }
}
