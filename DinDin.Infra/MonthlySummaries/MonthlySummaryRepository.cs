using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Domain.Transactions.Enums;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDin.Infra.MonthlySummaries
{
    public class MonthlySummaryRepository(DinDinDbContext dbContext) : IMonthlySummaryRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task<int> Add(Transaction transaction, int userId)
        {
            var monthlySummaryModel = new MonthlySummaryModel
            {
                Month = transaction.TransactionDate.Month,
                Year = transaction.TransactionDate.Year,
                UserId = userId,
            };

            if (transaction.Type == TransactionType.Expense)
                monthlySummaryModel.TotalExpense = transaction.Amont;
            else 
                monthlySummaryModel.TotalIncome = transaction.Amont;

            var entityEntry = await _dbContext.AddAsync(monthlySummaryModel);
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task<List<MonthlySummary>> GetAllWithUserId(int id)
        {
            var monthlySummaryModelList = await _dbContext.MonthlySummaries
                .AsNoTracking()
                .Where(monthlySummary => monthlySummary.UserId == id)
                .ToListAsync();

            var monthlySummaryList = monthlySummaryModelList
                .Select(model => new MonthlySummary
                {
                    Id = model.Id,
                    Month = model.Month,
                    Year = model.Year,
                    TotalExpense = model.TotalExpense,
                    TotalIncome= model.TotalIncome,
                    UserId = model.UserId
                }).ToList();

            return monthlySummaryList;
        }

        public async Task<MonthlySummary> GetById(int id)
        {
            var monthlySummaryModel = await _dbContext.MonthlySummaries.AsNoTracking().FirstOrDefaultAsync(model => model.Id == id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {id}");

            return new MonthlySummary
            {
                Id = monthlySummaryModel.Id,
                Month = monthlySummaryModel.Month,
                Year = monthlySummaryModel.Year,
                TotalExpense = monthlySummaryModel.TotalExpense,
                TotalIncome = monthlySummaryModel.TotalIncome,
                UserId = monthlySummaryModel.UserId
            };
        }

        public async Task Update(Transaction transaction)
        {
            var monthlySummaryModel = await _dbContext.MonthlySummaries.FindAsync(transaction.MonthlySummaryId)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {transaction.MonthlySummaryId}");

            if (transaction.Type == TransactionType.Expense)
                monthlySummaryModel.TotalExpense += transaction.Amont;
            else
                monthlySummaryModel.TotalIncome += transaction.Amont;

            await _dbContext.SaveChangesAsync();
        }
    }
}