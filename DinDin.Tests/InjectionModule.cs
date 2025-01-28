﻿using DinDin.Domain.Users;
using DinDin.Services.Users;
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
            services.AddScoped<IValidator<User>, UserValidator>();
        }
    }
}