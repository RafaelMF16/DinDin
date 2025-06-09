namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        Task<List<MonthlySummary>> GetAllWithUserId(int id);
        Task<MonthlySummary> GetById(int id);
        Task Add(MonthlySummary monthlySummary);
    }
}