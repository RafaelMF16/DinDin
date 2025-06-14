using DinDin.Domain.Transactions;

namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        Task<List<MonthlySummary>> GetAllWithUserId(int id);
        Task<MonthlySummary> GetById(int id);
        Task<int> Add(Transaction transaction, int userId);
        Task Update(Transaction transaction);
    }
}