using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.MonthlySummaries;

namespace DinDin.Tests.MonthlySummaries
{
    public class MockMonthlySummaryRepository : IMonthlySummaryRepository
    {
        private readonly MonthlySummarySingleton _instance;

        public MockMonthlySummaryRepository()
        {
            _instance = MonthlySummarySingleton.Instance;
        }

        public void Add(MonthlySummary monthlySummary)
        {
            _instance.Add(monthlySummary);
        }

        public void Delete(int id)
        {
            var monthlySummaryThatWillBeDeleted = GetById(id);

            _instance.Remove(monthlySummaryThatWillBeDeleted);
        }

        public List<MonthlySummary> GetAll()
        {
            return _instance;
        }

        public MonthlySummary GetById(int id)
        {
            return _instance.Find(monthlySummary => monthlySummary.Id == id);
        }

        public void Update(MonthlySummary monthlySummary)
        {
            throw new NotImplementedException();
        }
    }
}