using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;
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

        public void AddTransactionInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction)
        {
            monthlySummary.Transactions.Add(transaction);
            AddAmontInMonthlySummary(monthlySummary, transaction);
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
            var monthlySummaryThatWillBeDeleted = await GetById(id);

            _instance.Remove(monthlySummaryThatWillBeDeleted);
        }

        public Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            var monthlySummary = _instance.Where(monthlySummary => monthlySummary.UserId == id).ToList();
            return Task.FromResult(monthlySummary);
        }

        public Task<MonthlySummary> GetById(string id)
        {
            var monthlySummary = _instance.FirstOrDefault(monthlySummary => monthlySummary.Id == id);
            return Task.FromResult(monthlySummary);
        }

        public Task<MonthlySummary> GetByMonthAndYear(Transaction transaction, string userId)
        {
            var monthlySummary = _instance.Where(monthlySummary => monthlySummary.UserId == userId)
                .FirstOrDefault(monthlySummary => monthlySummary.Month == transaction.TransactionDate.Month && monthlySummary.Year == transaction.TransactionDate.Year);

            return Task.FromResult(monthlySummary);
        }

        public async Task Update(MonthlySummary monthlySummary)
        {
            var dataBaseMonthlySummary = await GetById(monthlySummary.Id);

            _instance[_instance.IndexOf(dataBaseMonthlySummary)] = monthlySummary;
        }
    }
}