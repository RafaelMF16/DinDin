using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace DinDin.Infra.Users
{
    public static class UserExtension
    {
        public static IRavenQueryable<User> WithEmail(this IRavenQueryable<User> query, string email)
        {
            return query.Where(user => user.Email == email);
        }

        public static IRavenQueryable<MonthlySummary> WithId(this IRavenQueryable<MonthlySummary> query, string id)
        {
            return query.Where(monthlySummary => monthlySummary.Id == id);
        }

    }
}