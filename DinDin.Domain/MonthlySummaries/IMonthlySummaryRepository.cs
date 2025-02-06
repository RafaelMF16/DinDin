namespace DinDin.Domain.MonthlySummaries
{
    public interface IMonthlySummaryRepository
    {
        List<MonthlySummary> GetAll();
        MonthlySummary GetById(int id);
        void Add(MonthlySummary monthlySummary);
        void Update(MonthlySummary monthlySummary);
        void Delete(int id);
    }
}