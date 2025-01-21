using DinDin.Domain.Users;
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

            UserSingleton.Instance.Clear();
        }

        [Fact]
        public void Should_be_able_to_create_a_new_user()
        {
            var newUser = new User 
            {
                Id = 1,
                Name = "Test",
                Login = "login",
                Password = "password",
                CreationDate = DateTime.Now
            };

            _userService.Add(newUser);

            Assert.Contains(UserSingleton.Instance, user => user == newUser);
        }
    }
}