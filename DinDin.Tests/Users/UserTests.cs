﻿using DinDin.Domain.Users;
using DinDin.Infra.Users;
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
                Name = "Test",
                Email = "login@email.com",
                Password = "password",
                CreationDate = DateTime.UtcNow
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
                UserId = 1,
                Name = name,
                Email = "login@email.com",
                Password = "password",
                CreationDate = DateTime.UtcNow
            };

            const string errorMessageExpected = "The Name field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser)).Errors.First().ErrorMessage;

            Assert.Equal(errorMessageExpected, errorMessage);
        }

        [Theory]
        [InlineData("Intelligent System for Personal Finance Management and Control with Advanced and Fully Customizable Features")]
        [InlineData("Comprehensive Financial Organization Platform for Users Focused on Income, Expenses, and Strategic Planning")]
        public void When_trying_to_create_a_user_with_a_name_of_more_than_hundred_characters_a_validation_error_must_be_returned(string name)
        {
            var newUser = new User
            {
                UserId = 1,
                Name = name,
                Email = "login@email.com",
                Password = "password",
                CreationDate = DateTime.UtcNow
            };

            const string errorMessageExpected = "The Name field can contain a maximum of 100 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void When_trying_to_create_a_user_with_a_email_of_null_or_empty_a_validation_error_must_be_returned(string? email)
        {
            var newUser = new User
            {
                UserId = 1,
                Name = "Rafael",
                Email = email,
                Password = "password",
                CreationDate = DateTime.UtcNow
            };

            const string errorMessageExpected = "The Email field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("IntelligentSystemforPersonalFinanceManagementandControlwithAdvancedandFullyCustomizableFeatures@email.com")]
        [InlineData("ComprehensiveFinancialOrganizationPlatformforUsersFocusedonIncomeExpensesandStrategicPlanning@email.com")]
        public void When_trying_to_create_a_user_with_a_email_of_more_than_hundred_characters_a_validation_error_must_be_returned(string email)
        {
            var newUser = new User
            {
                UserId = 1,
                Name = "Rafael",
                Email = email,
                Password = "password",
                CreationDate = DateTime.UtcNow
            };

            const string errorMessageExpected = "The Email field can contain a maximum of 100 characters";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("Email")]
        [InlineData("EmailInvalid")]
        public void When_trying_to_create_a_user_with_a_email_invalid_validation_error_must_be_returned(string email)
        {
            var newUser = new User
            {
                UserId = 1,
                Name = "Rafael",
                Email = email,
                Password = "password",
                CreationDate = DateTime.UtcNow
            };

            const string errorMessageExpected = "Email Invalid";

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
                UserId = 1,
                Name = "Rafael",
                Email = "login@email.com",
                Password = password,
                CreationDate = DateTime.UtcNow
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
                UserId = 1,
                Name = "Test",
                Email = "login@email.com",
                Password = password,
                CreationDate = DateTime.UtcNow
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
                UserId = 1,
                Name = "Test",
                Email = "login@email.com",
                Password = password,
                CreationDate = DateTime.UtcNow
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
                Name = "Test",
                Email = "login@email.com",
                Password = "password",
                CreationDate = DateTime.Parse(creationDate).ToUniversalTime()
            };

            const string errorMessageExpected = "The Creation Date not is valid";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _userService.Add(newUser));

            Assert.Equal(errorMessageExpected, errorMessage.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_by_id_must_return_user_with_id_expected(int id)
        {
            CreateUsersList();

            var expectedId = id; 

            var dataBaseUser = _userService.GetById(id);

            Assert.Equal(expectedId, dataBaseUser.UserId);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Get_by_id_must_throw_exception_if_id_is_null(int id)
        {
            CreateUsersList();

            var errorMessageExpected = $"Not find user with id: {id}";

            var errorMessage = Assert.Throws<ArgumentNullException>(() => _userService.GetById(id)).ParamName;

            Assert.Equal(errorMessageExpected, errorMessage);
        }

        [Fact]
        public void Delete_should_delete_user_with_id_one()
        {
            CreateUsersList();

            const int deletedUserId = 1;

            _userService.Delete(deletedUserId);

            var dataBaseList = UserSingleton.Instance;

            Assert.DoesNotContain(dataBaseList, user => user.UserId == deletedUserId);
        }

        [Fact]
        public void Delete_not_must_delete_user_with_id_four()
        {
            CreateUsersList();

            const int deletedUserId = 4;

            _userService.Delete(deletedUserId);

            var dataBaseList = UserSingleton.Instance;

            const int expectedListSize = 3;

            Assert.Equal(expectedListSize, dataBaseList.Count);
        }

        [Fact]
        public void Update_should_update_name_of_user_with_id_one()
        {
            CreateUsersList();

            var dataBaseList = UserSingleton.Instance;

            var updatedUser = new User
            {
                UserId = 1,
                Name = "Updated User",
                Email = "Login@email.com",
                Password = "password",
                CreationDate = DateTime.Parse("06/11/2024").ToUniversalTime()
            };

            _userService.Update(updatedUser);

            Assert.Contains(UserSingleton.Instance, user => user == updatedUser);
        }

        [Fact]
        public void Update_should_update_password_of_user_with_id_one()
        {
            CreateUsersList();

            var dataBaseList = UserSingleton.Instance;

            var updatedUser = new User
            {
                UserId = 1,
                Name = "User",
                Email = "Login@email.com",
                Password = "wordpass",
                CreationDate = DateTime.Parse("06/11/2024").ToUniversalTime()
            };

            _userService.Update(updatedUser);

            Assert.Contains(UserSingleton.Instance, user => user == updatedUser);
        }

        private static void CreateUsersList()
        {
            var UsersSingletonList = UserSingleton.Instance;

            var UsersList = new List<User>
            {
                new()
                {
                    UserId = 1,
                    Name = "User",
                    Email = "Login@email.com",
                    Password = "password",
                    CreationDate = DateTime.Parse("06/11/2024").ToUniversalTime()
                },

                new()
                {
                    UserId = 2,
                    Name = "User02",
                    Email = "Login02@email.com",
                    Password = "password02",
                    CreationDate = DateTime.Parse("11/16/2023").ToUniversalTime()
                },

                new()
                {
                    UserId = 3,
                    Name = "User03",
                    Email = "Login03@email.com",
                    Password = "password03",
                    CreationDate = DateTime.UtcNow
                }
            };

            UsersSingletonList.AddRange(UsersList);
        }
    }
}