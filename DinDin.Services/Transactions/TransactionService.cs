using DinDin.Domain.Transactions;
using DinDin.Infra.Transactions;

namespace DinDin.Services.Transactions
{
    public class TransactionService(TransactionRepository transactionRepository)
    {
        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId)
        {
            return await _transactionRepository.GetAllByMonthlySummaryId(monthlySummaryId);
        }
    }
}