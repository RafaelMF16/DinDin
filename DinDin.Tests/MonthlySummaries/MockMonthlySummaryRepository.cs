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

        public async Task Add(MonthlySummary monthlySummary)
        {
            _instance.Add(monthlySummary);
        }

        public void Delete(string id)
        {
            var monthlySummaryThatWillBeDeleted = GetById(id);

            _instance.Remove(monthlySummaryThatWillBeDeleted);
        }

        public List<MonthlySummary> GetAll()
        {
            return _instance;
        }

        public MonthlySummary GetById(string id)
        {
            return _instance.Find(monthlySummary => monthlySummary.Id == id);
        }

        public void Update(MonthlySummary monthlySummary)
        {
            var dataBaseMonthlySummary = GetById(monthlySummary.Id);

            _instance[_instance.IndexOf(dataBaseMonthlySummary)] = monthlySummary;
        }
    }
}