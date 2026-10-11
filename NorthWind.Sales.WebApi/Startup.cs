namespace NorthWind.Sales.WebApi;

internal static class Startup
{
    public static WebApplication CreateWebApplication(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenBearer();
        builder.Services.AddNorthWindSalesServices(
            dbOptions => builder.Configuration.GetSection(DBOptions.SectionKey).Bind(dbOptions),
            smtpOptions => builder.Configuration.GetSection(SmtpOptions.SectionKey).Bind(smtpOptions),
            logsOptions => builder.Configuration.GetSection(DomainLogsOptions.SectionKey).Bind(logsOptions),
            membershipOptions => builder.Configuration.GetSection(MembershipDBOptions.SectionKey).Bind(membershipOptions),
            jwtOptions => builder.Configuration.GetSection(JwtOptions.SectionKey).Bind(jwtOptions));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                builder.Configuration.GetSection(JwtOptions.SectionKey)
                    .Bind(options.TokenValidationParameters);

                string securityKey = builder.Configuration
                    .GetSection(JwtOptions.SectionKey)[nameof(JwtOptions.SecurityKey)]!;
                byte[] securityKeyBytes = Encoding.UTF8.GetBytes(securityKey);
                options.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(securityKeyBytes);
            });

        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(config =>
            {
                config.AllowAnyMethod();
                config.AllowAnyHeader();
                config.AllowAnyOrigin();
            });
        });

        return builder.Build();
    }

    public static WebApplication ConfigureWebApplication(
        this WebApplication app)
    {
        app.UseExceptionHandler(builder => { });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapNorthWindSalesEndpoints();
        return app;
    }
}
