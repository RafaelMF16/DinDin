using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.RavenDB.Extensions;
using DinDin.Infra.Users;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.MonthlySummaries
{
    public class MonthlySummaryRepository : IMonthlySummaryRepository
    {
        private readonly IAsyncDocumentSession _session;

        public MonthlySummaryRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Add(MonthlySummary monthlySummary)
        {
            await _session.StoreAsync(monthlySummary);
            await _session.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {


             _session.Delete(id);
            await _session.SaveChangesAsync();
        }

        public async Task<List<MonthlySummary>> GetAll()
        {
            return await _session.MonthlySummaryes().ToListAsync();
        }

        public async Task<MonthlySummary> GetById(string id)
        {
            return await _session.MonthlySummaryes().WithId(id).FirstOrDefaultAsync();
        }

        public async Task Update(MonthlySummary monthlySummary)
        {
            var monthlySummaryUpdate = await _session.LoadAsync<MonthlySummary>(monthlySummary.Id);
            monthlySummaryUpdate.Month = monthlySummary.Month;
            monthlySummaryUpdate.Year = monthlySummary.Year;
            monthlySummaryUpdate.TotalIncome = monthlySummary.TotalIncome;
            monthlySummaryUpdate.TotalExpense = monthlySummary.TotalExpense;

            await _session.SaveChangesAsync();
        }
    }
}