using DinDin.Domain.MonthlySummaries;
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

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<MonthlySummary> GetAll()
        {
            throw new NotImplementedException();
        }

        public MonthlySummary GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(MonthlySummary monthlySummary)
        {
            throw new NotImplementedException();
        }
    }
}