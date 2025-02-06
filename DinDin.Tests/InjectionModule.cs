using DinDin.Domain.Acconts;
using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using DinDin.Services.Acconts;
using DinDin.Services.MonthlySummaries;
using DinDin.Services.Users;
using DinDin.Tests.Acconts;
using DinDin.Tests.MonthlySummaries;
using DinDin.Tests.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests
{
    public static class InjectionModule
    {
        public static void AddServicesInScope(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, MockUserRepository>();
            services.AddScoped<IValidator<User>, ValidatorUser>();

            services.AddScoped<AccontService>();
            services.AddScoped<IAccontRepository, MockAccontRepository>();
            services.AddScoped<IValidator<Accont>, ValidatorAccont>();

            services.AddScoped<MonthlySummaryService>();
            services.AddScoped<IMonthlySummaryRepository, MockMonthlySummaryRepository>();
        }
    }
}