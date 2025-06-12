using DinDin.Domain.Transactions;

namespace DinDin.Services.Transactions
{
    public class TransactionService(ITransactionRepository transactionRepository)
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            return await _transactionRepository.GetAllByMonthlySummaryId(monthlySummaryId);
        }

        public async Task DeleteTransaction(int id)
        {
            await _transactionRepository.Delete(id);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            try
            {
                await _transactionRepository.Add(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            try
            {
                await _transactionRepository.Update(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}