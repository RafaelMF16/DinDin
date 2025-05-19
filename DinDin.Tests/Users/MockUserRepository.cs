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

        public void Delete(string id)
        {
            var userThatWillBeDeleted = GetById(id);

            _instance.Remove(userThatWillBeDeleted);
        }

        public User GetById(string id)
        {
            return _instance.Find(user => user.Id == id);
        }

        public Task<User> GetUserByEmail(string email)
        {
            var user = _instance.Find(user => user.Email == email);
            return Task.FromResult(user);
        }

        public void Update(User user)
        {
            var dataBaseUser = GetById(user.Id!);

            _instance[_instance.IndexOf(dataBaseUser)] = user;
        }
    }
}