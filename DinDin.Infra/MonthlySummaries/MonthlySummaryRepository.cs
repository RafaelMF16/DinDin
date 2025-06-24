using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDin.Infra.MonthlySummaries
{
    public class MonthlySummaryRepository(DinDinDbContext dbContext) : IMonthlySummaryRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task<int> Add(MonthlySummary monthlySummary)
        {
            var monthlySummaryModel = new MonthlySummaryModel
            {
                Month = monthlySummary.Month,
                Year = monthlySummary.Year,
                UserId = monthlySummary.UserId,
                Balance = monthlySummary.Balance,
                TotalExpense = monthlySummary.TotalExpense,
                TotalIncome = monthlySummary.TotalIncome 
            };

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

        public async Task Update(MonthlySummary monthlySummary)
        {
            var monthlySummaryModel = await _dbContext.MonthlySummaries.FindAsync(monthlySummary.Id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {monthlySummary.Id}");

            monthlySummaryModel.Month = monthlySummary.Month;
            monthlySummaryModel.Year = monthlySummary.Year;
            monthlySummaryModel.TotalExpense = monthlySummary.TotalExpense;
            monthlySummaryModel.TotalIncome = monthlySummary.TotalIncome;
            monthlySummaryModel.UserId = monthlySummary.UserId;
            monthlySummaryModel.Balance = monthlySummary.Balance;

            await _dbContext.SaveChangesAsync();
        }
    }
}