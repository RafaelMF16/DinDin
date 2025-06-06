namespace DinDin.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task Add(User user);
        Task Update(User user);
        Task Delete(string id);
        Task<User?> GetUserByEmail(string email);
    }
}