namespace Microsoft.AspNetCore.Builder;

public static class UserLoginController
{
    public static WebApplication UseUserLoginController(this WebApplication app)
    {
        app.MapPost(Endpoints.Login,
            async (UserCredentialsDto userCredentials,
                   IUserLoginInputPort inputPort,
                   IUserLoginOutputPort presenter) =>
            {
                await inputPort.Handle(userCredentials);
                return presenter.Result;
            });
        return app;
    }
}
