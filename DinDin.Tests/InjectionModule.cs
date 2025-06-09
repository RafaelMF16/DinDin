using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using DinDin.Services.Auth;
using DinDin.Services.MonthlySummaries;
using DinDin.Services.Users;
using DinDin.Tests.MonthlySummaries;
using DinDin.Tests.Users;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests
{
    public static class InjectionModule
    {
        public static void AddServicesInScope(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, MockUserRepository>();
            services.AddScoped<IValidator<User>, ValidatorUser>();

            services.AddScoped<MonthlySummaryService>();
            services.AddScoped<IMonthlySummaryRepository, MockMonthlySummaryRepository>();
            services.AddScoped<IValidator<MonthlySummary>, ValidatorMonthlySummary>();
        }
    }
}