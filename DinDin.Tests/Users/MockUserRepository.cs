using DinDin.Domain.Users;
using DinDin.Infra.Users;
using FluentValidation;

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

        public async Task Delete(string id)
        {
            var userThatWillBeDeleted = await GetById(id);

            _instance.Remove(userThatWillBeDeleted);
        }

        public async Task<User> GetById(string id)
        {
            return await Task.Run(() =>
                _instance.Find(user => user.Id == id)
                    ?? throw new ArgumentNullException($"Not find user with id: {id}"));
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await Task.Run(() =>
                _instance.Find(user => user.Email == email));
        }

        public async Task Update(User user)
        {
            var dataBaseUser = await GetById(user.Id!);

            _instance[_instance.IndexOf(dataBaseUser)] = user;
        }
    }
}