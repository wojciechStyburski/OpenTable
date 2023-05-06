namespace OpenTable.Infrastructure.Auth;

internal static class AuthExtensions
{
    private const string SectionName = "Auth";
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));
        var options = configuration.GetOptions<AuthOptions>(SectionName);

        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<ITokenStorage, HttpContextTokenStorage>();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.Audience = options.Audience;
            x.IncludeErrorDetails = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = options.Issuer,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
            };
        });

        services.AddAuthorization(authorizationOptions =>
        {
            authorizationOptions.AddPolicy("is-admin", policy =>
            {
                policy.RequireRole("admin");
            });
        });
        
        return services;
    }
}