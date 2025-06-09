using DinDin.Domain.Transactions;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;

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

        public Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            throw new NotImplementedException();
        }

        public Task Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
