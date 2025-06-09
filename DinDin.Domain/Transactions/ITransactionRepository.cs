namespace DinDin.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllByMonthlySummaryId(int monthlySummaryId);
        Task Add(Transaction transaction);
        Task Update(Transaction transaction);
        Task Delete(int id);
    }
}