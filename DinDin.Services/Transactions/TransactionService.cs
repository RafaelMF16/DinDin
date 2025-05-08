using DinDin.Domain.Transactions;
using DinDin.Infra.Transactions;

namespace DinDin.Services.Transactions
{
    public class TransactionService(TransactionRepository transactionRepository)
    {
        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async Task Add(Transaction transaction)
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
    }
}