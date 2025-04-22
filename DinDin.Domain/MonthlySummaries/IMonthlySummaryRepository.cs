namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        Task<List<MonthlySummary>> GetAll();
        Task<MonthlySummary> GetById(string id);
        Task Add(MonthlySummary monthlySummary);
        Task Update(MonthlySummary monthlySummary);
        Task Delete(string id);
    }
}