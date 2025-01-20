using DinDin.Domain.Users;

namespace DinDin.Tests.Users
{
    public class MockUserRepository : IUserRepository
    {
        private readonly UserSingleton _instance;

        public MockUserRepository ()
        {
            _instance = UserSingleton.Instance;
        }

        public void Add(User user)
        {
            _instance.Add(user);
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