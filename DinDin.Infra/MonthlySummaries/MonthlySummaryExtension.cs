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
    }
}