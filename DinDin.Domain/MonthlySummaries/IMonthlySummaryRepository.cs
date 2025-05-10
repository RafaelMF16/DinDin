using DinDin.Domain.Transactions;

namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        Task<List<MonthlySummary>> GetAllWithUserId(string id);
        Task<MonthlySummary> GetById(string id);
        Task Add(MonthlySummary monthlySummary);
        Task Update(MonthlySummary monthlySummary);
        Task Delete(string id);
        Task<MonthlySummary> GetByMonthAndYear(Transaction transaction, string userId);
        void AddTransactionInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction);
    }
}