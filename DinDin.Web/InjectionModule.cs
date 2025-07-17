using DinDin.Domain.Auth;
using DinDin.Domain.Constantes;
using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Domain.Users;
using DinDin.Infra.Auth;
using DinDin.Infra.MonthlySummaries;
using DinDin.Infra.Postgres;
using DinDin.Infra.Transactions;
using DinDin.Infra.Users;
using DinDin.Services.Auth;
using DinDin.Services.Enums;
using DinDin.Services.MonthlySummaries;
using DinDin.Services.Transactions;
using DinDin.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DinDin.Web
{
    public static class InjectionModule
    {
        public static void AddServicesInScope(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<TransactionService>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IValidator<Transaction>, TransactionValidator>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IValidator<User>, ValidatorUser>();

            builder.Services.AddScoped<MonthlySummaryService>();
            builder.Services.AddScoped<IMonthlySummaryRepository, MonthlySummaryRepository>();
            builder.Services.AddScoped<IValidator<MonthlySummary>, ValidatorMonthlySummary>();

            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            builder.Services.AddScoped<EnumService>();

            var connectionString = Environment.GetEnvironmentVariable(ApplicationConstants.CONNECTION_STRING_ENVIRONMENT_VARIABLE)
                ?? throw new Exception($"Environment variable [{ApplicationConstants.CONNECTION_STRING_ENVIRONMENT_VARIABLE}] not found");

            builder.Services.AddDbContext<DinDinDbContext>(options =>
                options.UseNpgsql(connectionString));

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
                    policy.WithOrigins(ApplicationConstants.LOCAL_FRONT_END_URL, ApplicationConstants.BUILD_LOCAL_FRONT_END_URL)
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}