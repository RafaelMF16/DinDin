using System.Text;
using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using DinDin.Infra.RavenDB;
using DinDin.Infra.Users;
using DinDin.Services.Auth;
using DinDin.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DinDin.Web
{
    public static class InjectionModule
    {
        public static void AddServicesInScope(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IValidator<User>, ValidatorUser>();

            builder.Services.AddSingleton<IDocumentStore>(_ => DocumentStoreHolder.Store);
            builder.Services.AddScoped<IAsyncDocumentSession>(provider =>
            {
                var store = provider.GetRequiredService<IDocumentStore>();
                return store.OpenAsyncSession();
            });

            var secretKey = Environment.GetEnvironmentVariable(ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE)
                ?? throw new Exception($"Environment variable [{ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE}] not found");

            var encodedSecretKey = Encoding.ASCII.GetBytes(secretKey);     
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(encodedSecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Configuration
                .AddJsonFile(ApplicationConstants.APP_SETTINGS_NAME, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: ApplicationConstants.CORS_POLICY_NAME, policy =>
                {
                    policy.WithOrigins(ApplicationConstants.FRONT_END_URL).AllowAnyHeader().AllowAnyMethod();
                });
            });
            
        }
    }
}