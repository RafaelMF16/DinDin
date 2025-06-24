namespace DinDin.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId);
        Task Add(Transaction transaction);
        Task Delete(int id);
        Task<Transaction> GetById(int id);
    }
}