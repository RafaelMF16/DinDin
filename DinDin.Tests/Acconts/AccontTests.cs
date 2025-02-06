using DinDin.Domain.Acconts;
using DinDin.Infra.Acconts;
using DinDin.Services.Acconts;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests.Acconts
{
    public class AccontTests : BaseTest
    {
        private readonly AccontService _accontService;

        public AccontTests()
        {
            _accontService = ServiceProvider.GetRequiredService<AccontService>();

            AccontSingleton.Instance.Clear();
        }

        [Fact]
        public void Must_be_able_to_create_a_new_accont()
        {
            var newAccont = new Accont
            {
                UserId = 1
            };

            _accontService.Add(newAccont);

            Assert.Contains(AccontSingleton.Instance, user => user == newAccont);
        }

        [Fact]
        public void When_trying_to_create_a_accont_with_a_user_id_of_null_a_validation_error_must_be_returned()
        {
            var newAccont = new Accont();

            const string errorMessageExpected = "The UserId field is mandatory";

            var errorMessage = Assert.Throws<FluentValidation.ValidationException>(() => _accontService.Add(newAccont)).Errors.First().ErrorMessage;

            Assert.Equal(errorMessageExpected, errorMessage);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_by_id_must_return_accont_with_id_expected(int id)
        {
            CreateAccontList();

            var expectedId = id;

            var dataBaseUser = _accontService.GetById(id);

            Assert.Equal(expectedId, dataBaseUser.Id);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Get_by_id_must_throw_exception_if_id_is_null(int id)
        {
            CreateAccontList();

            var errorMessageExpected = $"Not find accont with id: {id}";

            var errorMessage = Assert.Throws<ArgumentNullException>(() => _accontService.GetById(id)).ParamName;

            Assert.Equal(errorMessageExpected, errorMessage);
        }

        [Fact]
        public void Delete_should_delete_accont_with_id_one()
        {
            CreateAccontList();

            const int deletedUserId = 1;

            _accontService.Delete(deletedUserId);

            var dataBaseList = AccontSingleton.Instance;

            Assert.DoesNotContain(dataBaseList, user => user.Id == deletedUserId);
        }

        [Fact]
        public void Delete_not_must_delete_accont_with_id_four()
        {
            CreateAccontList();

            const int deletedUserId = 4;

            _accontService.Delete(deletedUserId);

            var dataBaseList = AccontSingleton.Instance;

            const int expectedListSize = 3;

            Assert.Equal(expectedListSize, dataBaseList.Count);
        }

        private void CreateAccontList()
        {
            var accontSingletonList = AccontSingleton.Instance;

            var AccontsList = new List<Accont>
            {
                new()
                {
                    Id = 1,
                    UserId = 1
                },

                new()
                {
                    Id = 2,
                    UserId = 2
                },

                new()
                {
                    Id = 3,
                    UserId = 3
                },
            };

            accontSingletonList.AddRange(AccontsList);
        }
    }
}