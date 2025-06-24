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
        public async Task Should_be_able_to_get_all_monthly_summaries_with_user_id_when_calling_get_all()
        {
            var monthlySummaryList = CreateMonthlySummaryList();

            const int userId = 1;
            var dataBaseList = await _monthlySummaryService.GetAllWithUserId(userId);

            Assert.Equivalent(monthlySummaryList, dataBaseList);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Get_by_id_must_return_monthly_summary_with_id_expected(int id)
        {
            CreateMonthlySummaryList();
            var expectedId = id;

            var dataBaseUser = await _monthlySummaryService.GetById(id);

            Assert.Equal(expectedId, dataBaseUser.Id);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task Get_by_id_must_throw_exception_if_id_is_null(int id)
        {
            CreateMonthlySummaryList();

            var errorMessageExpected = $"Não foi encontrado nenhum usuário com id: {id}";

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _monthlySummaryService.GetById(id));

            Assert.Equal(errorMessageExpected, exception.ParamName);
        }

        private static List<MonthlySummary> CreateMonthlySummaryList()
        {
            var monthlySummarySingletonList = MonthlySummarySingleton.Instance;

            var monthlySummaryList = new List<MonthlySummary>
            {
                new()
                {
                    Id = 1,
                    TotalExpense = 300,
                    TotalIncome = 500,
                    UserId = 1,
                },

                new()
                {
                    Id = 2,
                    TotalExpense = 200,
                    TotalIncome = 300,
                    UserId = 1,
                },

                new()
                {
                    Id = 3,
                    TotalExpense = 100,
                    TotalIncome = 700,
                    UserId = 1,
                }
            };

            monthlySummarySingletonList.AddRange(monthlySummaryList);
            return monthlySummaryList;
        }
    }
}