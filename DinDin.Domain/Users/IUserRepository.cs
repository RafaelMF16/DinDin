namespace DinDin.Domain.Users
{
    public interface IUserRepository
    {
        User GetById(string id);
        Task Add(User user);
        void Update(User user);
        void Delete(string id);
    }
}