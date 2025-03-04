using DinDin.Domain.Users;
using DinDin.Infra.RavenDB;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.Users
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(user);
                session.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}