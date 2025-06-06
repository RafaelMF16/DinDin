using DinDin.Domain.Users;
using DinDin.Infra.RavenDB.Extensions;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DinDin.Infra.Users
{
    public class RavenDbUserRepository : IUserRepository
    {
        private readonly IAsyncDocumentSession _session;

        public RavenDbUserRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task Add(User user)
        {
            await _session.StoreAsync(user);
            await _session.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            return _session.Users().WithEmail(email).FirstOrDefaultAsync();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}