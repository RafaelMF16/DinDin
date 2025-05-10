using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using Raven.Client.Documents.Linq;

namespace DinDin.Infra.MonthlySummaries
{
    public static class MonthlySummaryExtension
    {
        public static IRavenQueryable<MonthlySummary> WithUserId(this IRavenQueryable<MonthlySummary> query, string id)
        {
            return query.Where(monthlySummary => monthlySummary.UserId == id);
        }

        public static IRavenQueryable<MonthlySummary> WithMonth(this IRavenQueryable<MonthlySummary> query, DateTime transactionDate)
        {
            return query.Where(monthlySummary => monthlySummary.Month == transactionDate.Month);
        }

        public static IRavenQueryable<MonthlySummary> WithYear(this IRavenQueryable<MonthlySummary> query, DateTime transactionDate)
        {
            return query.Where(monthlySummary => monthlySummary.Year == transactionDate.Year);
        }
    }
}