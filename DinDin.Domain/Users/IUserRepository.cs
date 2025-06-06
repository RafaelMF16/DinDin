namespace DinDin.Domain.Users
{
    public interface IUserRepository
    {
        User GetById(int id);
        Task Add(User user);
        void Update(User user);
        void Delete(int id);
        Task<User> GetUserByEmail(string email);
    }
}