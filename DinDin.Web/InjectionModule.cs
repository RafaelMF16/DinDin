using DinDin.Domain.Users;
using DinDin.Infra.Users;
using DinDin.Services.Users;

namespace DinDin.Web
{
    public static class InjectionModule
    {
        public static void AddServicesInScope(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}