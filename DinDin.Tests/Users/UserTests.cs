using DinDin.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests.Users
{
    public class UserTests : BaseTest
    {
        private readonly UserService _userService;

        public UserTests() 
        {
            _userService = ServiceProvider.GetRequiredService<UserService>();
        }
    }
}