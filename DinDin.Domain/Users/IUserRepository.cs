namespace DinDin.Domain.Users
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetUserByEmail(string email);
    }
}