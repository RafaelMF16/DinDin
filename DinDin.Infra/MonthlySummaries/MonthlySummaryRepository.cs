using DinDin.Domain.MonthlySummaries;
using DinDin.Infra.RavenDB.Extensions;
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

        public async Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            return await _session.MonthlySummaries().WithUserId(id).OrderBy(monthlySummary => monthlySummary.Month).ToListAsync();
        }

        public async Task<MonthlySummary> GetById(string id)
        {
            return await _session.GetById<MonthlySummary>(id);
        }

        public async Task Update(MonthlySummary updatedMonthlySummary)
        {
            var monthlySummary = await _session.GetById<MonthlySummary>(updatedMonthlySummary.Id);
            monthlySummary.Month = updatedMonthlySummary.Month;
            monthlySummary.Year = updatedMonthlySummary.Year;
            monthlySummary.TotalIncome = updatedMonthlySummary.TotalIncome;
            monthlySummary.TotalExpense = updatedMonthlySummary.TotalExpense;

            await _session.SaveChangesAsync();
        }
    }
}