using DinDin.Domain.Users;
using DinDin.Infra.RavenDB;
using DinDin.Infra.Users;
using DinDin.Services.Auth;
using DinDin.Services.Users;
using FluentValidation;
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
        }
    }
}