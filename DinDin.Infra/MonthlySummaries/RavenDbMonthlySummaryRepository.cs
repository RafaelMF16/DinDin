using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
using DinDin.Infra.RavenDB.Extensions;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.MonthlySummaries
{
    public class RavenDbMonthlySummaryRepository : IMonthlySummaryRepository
    {
        private readonly IAsyncDocumentSession _session;

        public RavenDbMonthlySummaryRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Add(MonthlySummary monthlySummary)
        {
            await _session.StoreAsync(monthlySummary);
            await _session.SaveChangesAsync();
        }

        public void AddTransactionInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction)
        {
            monthlySummary.Transactions.Add(transaction);
            AddAmontInMonthlySummary(monthlySummary, transaction);

            _session.SaveChangesAsync();
        }

        private static void AddAmontInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction)
        {
            const string expense = "despesa";
            if (transaction.Type == expense)
                monthlySummary.TotalExpense += transaction.Amont;
            else
                monthlySummary.TotalIncome += transaction.Amont;
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

        public async Task<MonthlySummary> GetByMonthAndYear(Transaction transaction, string userId)
        {
            var monthlySummary = await _session.MonthlySummaries().WithUserId(userId).WithMonth(transaction.TransactionDate).WithYear(transaction.TransactionDate).FirstOrDefaultAsync();
            if (monthlySummary is null)
            {
                monthlySummary = new MonthlySummary { Month = transaction.TransactionDate.Month, Year = transaction.TransactionDate.Year, UserId = userId };
                await _session.StoreAsync(monthlySummary);
                await _session.SaveChangesAsync();
            }

            return monthlySummary;
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