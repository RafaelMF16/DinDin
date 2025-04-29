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

        public async Task Delete(string id)
        {
            var monthlySummaryThatWillBeDeleted = await GetById(id);

            _instance.Remove(monthlySummaryThatWillBeDeleted);
        }

        public async Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            var monthlySummary = _instance.Where(monthlySummary => monthlySummary.UserId == id).ToList();
            return await Task.FromResult(monthlySummary);
        }

        public async Task<MonthlySummary> GetById(string id)
        {
            var monthlySummary = _instance.Find(monthlySummary => monthlySummary.Id == id);
            return await Task.FromResult(monthlySummary);
        }

        public async Task Update(MonthlySummary monthlySummary)
        {
            var dataBaseMonthlySummary = await GetById(monthlySummary.Id);

            _instance[_instance.IndexOf(dataBaseMonthlySummary)] = monthlySummary;
        }
    }
}