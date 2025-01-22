using System.Globalization;
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
        public void Must_be_able_to_create_a_new_user()
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void When_trying_to_create_a_user_with_a_name_of_null_or_empty_a_validation_error_must_be_returned(string? name)
        {
            var newUser = new User
            {
                Id = 1,
                Name = name,
                Login = "login",
                Password = "password",
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Name field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("Intelligent System for Personal Finance Management and Control with Advanced and Fully Customizable Features")]
        [InlineData("Comprehensive Financial Organization Platform for Users Focused on Income, Expenses, and Strategic Planning")]
        public void When_trying_to_create_a_user_with_a_name_of_more_than_hundred_characters_a_validation_error_must_be_returned(string name)
        {
            var newUser = new User
            {
                Id = 1,
                Name = name,
                Login = "login",
                Password = "password",
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Name field can contain a maximum of 100 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void When_trying_to_create_a_user_with_a_login_of_null_or_empty_a_validation_error_must_be_returned(string? login)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Rafael",
                Login = login,
                Password = "password",
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Login field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("Intelligent System for Personal Finance Management and Control with Advanced and Fully Customizable Features")]
        [InlineData("Comprehensive Financial Organization Platform for Users Focused on Income, Expenses, and Strategic Planning")]
        public void When_trying_to_create_a_user_with_a_login_of_more_than_hundred_characters_a_validation_error_must_be_returned(string login)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Rafael",
                Login = login,
                Password = "password",
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Login field can contain a maximum of 100 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void When_trying_to_create_a_user_with_a_password_of_null_or_empty_a_validation_error_must_be_returned(string? password)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Rafael",
                Login = "login",
                Password = password,
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Password field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("123")]
        [InlineData("12345")]
        [InlineData("1234567")]
        public void When_trying_to_create_a_user_with_a_password_of_less_than_eight_characters_a_validation_error_must_be_returned(string password)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Test",
                Login = "login",
                Password = password,
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Password field can contain a minimum of 8 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("*1A9xY&Lm@Q#Z3pB$tR4vN7kX2o!G5wH6cU^D8jL@T!VZ#1$r!!")]
        [InlineData("Qz!xW8n^K2j#B6mT4%L@R9oY7*P1G$cU3Z5v&H!!@#!@#!@#!@#jAlO12$")]
        [InlineData("&T4kZ2oL@Q7n*X1m#9cB5^Yv!G8p$%j6RT@W!D!@#!@#$%¨$#!@DçAlskpdaA")]
        public void When_trying_to_create_a_user_with_a_password_of_more_than_fifty_characters_a_validation_error_must_be_returned(string password)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Test",
                Login = "login",
                Password = password,
                CreationDate = DateTime.Now
            };

            const string errorMessageExpected = "The Password field can contain a maximum of 50 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("2/10/2024")]
        [InlineData("06/11/2024")]
        [InlineData("10/12/2025")]
        public void When_trying_to_create_a_user_with_a_creation_date_of_different_from_the_current_date_it_must_return_a_validation_error(string creationDate)
        {
            var newUser = new User
            {
                Id = 1,
                Name = "Test",
                Login = "login",
                Password = "password",
                CreationDate = DateTime.Parse(creationDate)
            };

            const string errorMessageExpected = "The Creation Date must be the current date";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }
    }
}