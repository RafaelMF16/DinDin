using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.RavenDB.Extensions
{
    public static class CollectionsExtension
    {
        public static async Task<T> GetById<T>(this IAsyncDocumentSession session, string id) where T : class
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            return await session.LoadAsync<T>(id);
        }
        public static IRavenQueryable<User> Users(this IAsyncDocumentSession session)
        {
            return session.Query<User>();
        }
        public static IRavenQueryable<MonthlySummary> MonthlySummaries(this IAsyncDocumentSession session)
        {
            return session.Query<MonthlySummary>();
        }
    }
}