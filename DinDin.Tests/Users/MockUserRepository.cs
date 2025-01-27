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
            var userThatWillBeDeleted = _instance.Find(user => user.Id == id);

            _instance.Remove(userThatWillBeDeleted);
        }

        public User GetById(int id)
        {
            return _instance.Find(user => user.Id == id);
        }

        public void Update(User user)
        {
            var dataBaseUser = GetById(user.Id);

            _instance[_instance.IndexOf(dataBaseUser)] = user;
        }
    }
}