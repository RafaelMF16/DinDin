using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.MonthlySummaries;
using DinDin.Services.MonthlySummaries;
using Microsoft.Extensions.DependencyInjection;

namespace DinDin.Tests.MonthlySummaries
{
    public class MonthlySummaryTests : BaseTest
    {
        private readonly MonthlySummaryService _monthlySummaryService;

        public MonthlySummaryTests()
        {
            _monthlySummaryService = ServiceProvider.GetRequiredService<MonthlySummaryService>();

            MonthlySummarySingleton.Instance.Clear();
        }

        [Fact]
        public async Task Must_be_able_to_create_a_new_monthly_summary()
        {
            var newMonthlySummary = new MonthlySummary
            {
                Id = "1",
                TotalExpense = 15,
                TotalIncome = 20
            };

            await _monthlySummaryService.Add(newMonthlySummary);

            Assert.Contains(MonthlySummarySingleton.Instance, monthlySummary => monthlySummary == newMonthlySummary);
        }

        [Fact]
        public async Task The_balance_should_be_the_difference_between_total_income_and_total_expenses_when_creating_a_monthly_summary()
        {
            var newMonthlySummary = new MonthlySummary
            {
                Id = "1",
                TotalExpense = 15,
                TotalIncome = 20,
            };

            var expectedBalance = 5;

            await _monthlySummaryService.Add(newMonthlySummary);

            var dataBaseList = MonthlySummarySingleton.Instance;

            Assert.Equal(expectedBalance, dataBaseList.First().Balance);
        }

        [Fact]
        public async Task Should_be_able_to_get_all_monthly_summaries_with_user_id_when_calling_get_all()
        {
            var monthlySummaryList = CreateMonthlySummaryList();

            const string userId = "1";
            var dataBaseList = await _monthlySummaryService.GetAllWithUserId(userId);

            Assert.Equivalent(monthlySummaryList, dataBaseList);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public async Task Get_by_id_must_return_monthly_summary_with_id_expected(string id)
        {
            CreateMonthlySummaryList();
            var expectedId = id;

            var dataBaseUser = await _monthlySummaryService.GetById(id);

            Assert.Equal(expectedId, dataBaseUser.Id);
        }

        [Theory]
        [InlineData("4")]
        [InlineData("5")]
        [InlineData("6")]
        public async Task Get_by_id_must_throw_exception_if_id_is_null(string id)
        {
            CreateMonthlySummaryList();

            var errorMessageExpected = $"Not find monthly summary with id: {id}";

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _monthlySummaryService.GetById(id));

            Assert.Equal(errorMessageExpected, exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_delete_monthly_summary_with_id_one()
        {
            CreateMonthlySummaryList();

            const string deletedUserId = "1";

            await _monthlySummaryService.Delete(deletedUserId);

            var dataBaseList = MonthlySummarySingleton.Instance;

            Assert.DoesNotContain(dataBaseList, user => user.Id == deletedUserId);
        }

        [Fact]
        public async Task Delete_not_must_delete_monthly_summary_with_id_four()
        {
            CreateMonthlySummaryList();

            const string deletedUserId = "4";

            await _monthlySummaryService.Delete(deletedUserId);

            var dataBaseList = MonthlySummarySingleton.Instance;

            const int expectedListSize = 3;

            Assert.Equal(expectedListSize, dataBaseList.Count);
        }

        [Fact]
        public async Task Must_update_the_total_expense_of_the_monthly_summary_with_id_one()
        {
            CreateMonthlySummaryList();

            var updatedMonthlySummary = new MonthlySummary
            {
                Id = "1",
                TotalExpense = 500,
                TotalIncome = 500
            };

            await _monthlySummaryService.Update(updatedMonthlySummary);

            Assert.Contains(MonthlySummarySingleton.Instance, monthlySummary => monthlySummary == updatedMonthlySummary);
        }

        [Fact]
        public async Task Must_update_the_total_income_of_the_monthly_summary_with_id_one()
        {
            CreateMonthlySummaryList();

            var updatedMonthlySummary = new MonthlySummary
            {
                Id = "1",
                TotalExpense = 300,
                TotalIncome = 600
            };

            await _monthlySummaryService.Update(updatedMonthlySummary);

            Assert.Contains(MonthlySummarySingleton.Instance, monthlySummary => monthlySummary == updatedMonthlySummary);
        }

        private static List<MonthlySummary> CreateMonthlySummaryList()
        {
            var monthlySummarySingletonList = MonthlySummarySingleton.Instance;

            var monthlySummaryList = new List<MonthlySummary>
            {
                new()
                {
                    Id = "1",
                    TotalExpense = 300,
                    TotalIncome = 500,
                    UserId = "1",
                },

                new()
                {
                    Id = "2",
                    TotalExpense = 200,
                    TotalIncome = 300,
                    UserId = "1",
                },

                new()
                {
                    Id = "3",
                    TotalExpense = 100,
                    TotalIncome = 700,
                    UserId = "1",
                }
            };

            monthlySummarySingletonList.AddRange(monthlySummaryList);
            return monthlySummaryList;
        }
    }
}