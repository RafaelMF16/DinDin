using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Transactions;

namespace DinDin.Infra.MonthlySummaries
{
    public class PostgresMonthlySummaryRepository : IMonthlySummaryRepository
    {
        public Task Add(MonthlySummary monthlySummary)
        {
            throw new NotImplementedException();
        }

        public void AddTransactionInMonthlySummary(MonthlySummary monthlySummary, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MonthlySummary>> GetAllWithUserId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MonthlySummary> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MonthlySummary> GetByMonthAndYear(Transaction transaction, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(MonthlySummary monthlySummary)
        {
            throw new NotImplementedException();
        }
    }
}