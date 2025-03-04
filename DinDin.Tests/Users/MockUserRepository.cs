﻿using DinDin.Domain.Users;
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

        public void Add(User user)
        {
            _instance.Add(user);
        }

        public void Delete(int id)
        {
            var userThatWillBeDeleted = GetById(id);

            _instance.Remove(userThatWillBeDeleted);
        }

        public User GetById(int id)
        {
            return _instance.Find(user => user.UserId == id);
        }

        public void Update(User user)
        {
            var dataBaseUser = GetById(user.UserId);

            _instance[_instance.IndexOf(dataBaseUser)] = user;
        }
    }
}