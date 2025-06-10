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

        public async Task Update(Transaction transaction)
        {
            var transactionModel = await _dbContext.Transactions.FindAsync(transaction.Id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhuma transação com id: {transaction.Id}");

            transactionModel.Id = transaction.Id;
            transactionModel.Amont = transaction.Amont;
            transactionModel.Description = transaction.Description;
            transactionModel.ExpenseCategory = transaction.ExpenseCategory;
            transactionModel.IncomeCategory = transaction.IncomeCategory;
            transactionModel.TransactionDate = transaction.TransactionDate;
            transactionModel.Type = transaction.Type;
            transactionModel.MonthlySummaryId = transaction.MonthlySummaryId;

            await _dbContext.SaveChangesAsync();
        }
    }
}
