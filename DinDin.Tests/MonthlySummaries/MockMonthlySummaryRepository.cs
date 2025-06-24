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

        public async Task<int> Add(MonthlySummary monthlySummary)
        {
            _instance.Add(monthlySummary);
            return await Task.Run(() => monthlySummary.Id);
        }

        public async Task<List<MonthlySummary>> GetAllWithUserId(int id)
        {
            return await Task.Run(() => _instance.Where(x => x.UserId == id).ToList());
        }

        public async Task<MonthlySummary> GetById(int id)
        {
            var monthlySummary = _instance.FirstOrDefault(x => x.Id == id)
                ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {id}");

            return await Task.Run(() => monthlySummary);
        }

        public Task Update(MonthlySummary monthlySummary)
        {
            var dataBaseMonthlySummary = _instance.FirstOrDefault(x => x.Id == monthlySummary.Id)
               ?? throw new ArgumentNullException($"Não foi encontrado nenhum usuário com id: {monthlySummary.Id}");

            _instance[_instance.IndexOf(dataBaseMonthlySummary)] = monthlySummary;

            return Task.CompletedTask;
        }
    }
}