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

        public Task Add(MonthlySummary monthlySummary)
        {
            _instance.Add(monthlySummary);
            return Task.CompletedTask;
        }

        public Task<List<MonthlySummary>> GetAllWithUserId(int id)
        {
            var monthlySummary = _instance.Where(monthlySummary => monthlySummary.UserId == id).ToList();
            return Task.FromResult(monthlySummary);
        }

        public Task<MonthlySummary> GetById(int id)
        {
            var monthlySummary = _instance.FirstOrDefault(monthlySummary => monthlySummary.Id == id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {id}");

            return Task.FromResult(monthlySummary);
        }
    }
}