using DinDin.Domain.Users;
using DinDin.Infra.Users;

namespace DinDin.Tests.Users
{
    public class MockUserRepository : IUserRepository
    {
        private readonly UserSingleton _instance;

        public MockUserRepository ()
        {
            _instance = UserSingleton.Instance;
        }

        public Task Add(User user)
        {
            _instance.Add(user);
            return Task.CompletedTask;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await Task.Run(() =>
                _instance.Find(user => user.Email == email));
        }
    }
}