namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        List<MonthlySummary> GetAll();
        MonthlySummary GetById(string id);
        Task Add(MonthlySummary monthlySummary);
        void Update(MonthlySummary monthlySummary);
        void Delete(string id);
    }
}