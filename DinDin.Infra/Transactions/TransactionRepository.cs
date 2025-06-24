using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDin.Infra.Transactions
{
    public class TransactionRepository(DinDinDbContext dbContext) : ITransactionRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task Add(Transaction transaction)
        {
            var transactionModel = new TransactionModel 
            { 
                Amont = transaction.Amont,
                Description = transaction.Description,
                ExpenseCategory = transaction.ExpenseCategory,
                IncomeCategory = transaction.IncomeCategory,
                TransactionDate = transaction.TransactionDate,
                MonthlySummaryId = transaction.MonthlySummaryId,
                Type = transaction.Type,
            };

            await _dbContext.AddAsync(transactionModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhuma transação com id: {id}");

            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            var transactionModelList = await _dbContext.Transactions
                .AsNoTracking()
                .Where(transactionModel => transactionModel.MonthlySummaryId == monthlySummaryId)
                .ToListAsync();

            var transactionsList = transactionModelList
                .Select(transactionModel => new Transaction 
                { 
                    Id = transactionModel.Id,
                    Amont = transactionModel.Amont,
                    Description = transactionModel.Description,
                    ExpenseCategory = transactionModel.ExpenseCategory,
                    IncomeCategory= transactionModel.IncomeCategory,
                    TransactionDate = transactionModel.TransactionDate,
                    Type = transactionModel.Type,
                    MonthlySummaryId = transactionModel.MonthlySummaryId
                }).ToList();

            return transactionsList;
        }

        public async Task<Transaction> GetById(int id)
        {
            var transactionModel = await _dbContext.Transactions.AsNoTracking().FirstOrDefaultAsync(model => model.Id == id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {id}");

            return new Transaction
            {
                Id = transactionModel.Id,
                Amont = transactionModel.Amont,
                Description = transactionModel.Description,
                Type = transactionModel.Type,
                MonthlySummaryId = transactionModel.MonthlySummaryId,
                TransactionDate = transactionModel.TransactionDate,
                ExpenseCategory = transactionModel.ExpenseCategory,
                IncomeCategory = transactionModel.IncomeCategory
            };
        }
    }
}
