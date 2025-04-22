using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.RavenDB.Extensions
{
    public static class CollectionsExtension
    {
        public static IRavenQueryable<User> Users(this IAsyncDocumentSession session)
        {
            return session.Query<User>();
        }
        public static IRavenQueryable<MonthlySummary> MonthlySummaryes(this IAsyncDocumentSession session)
        {
            return session.Query<MonthlySummary>();
        }
    }
}