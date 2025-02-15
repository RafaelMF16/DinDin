using DinDin.Domain.Users;
using DinDin.Infra.Users;
using DinDin.Services.Users;
using FluentValidation;

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
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IValidator<User>, ValidatorUser>();
        }
    }
}