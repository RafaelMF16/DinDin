using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;

namespace DinDin.Infra.MonthlySummaries
{
    public class MonthlySummaryRepository(DinDinDbContext dbContext) : IMonthlySummaryRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task Add(MonthlySummary monthlySummary)
        {
            var monthlySummaryModel = new MonthlySummaryModel
            {
                Month = monthlySummary.Month,
                Year = monthlySummary.Year,
                Balance = monthlySummary.Balance,
                TotalExpense = monthlySummary.TotalExpense,
                TotalIncome = monthlySummary.TotalIncome,
                UserId = int.Parse(monthlySummary.UserId)
            };

            await _dbContext.AddAsync(monthlySummaryModel);
            await _dbContext.SaveChangesAsync();
        }

        public void AddTransactionInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MonthlySummary> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MonthlySummary> GetByMonthAndYear(Transaction transaction, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(MonthlySummary monthlySummary)
        {
            throw new NotImplementedException();
        }
    }
}