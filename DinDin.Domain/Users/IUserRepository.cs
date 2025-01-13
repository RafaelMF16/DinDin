namespace DinDin.Domain.Users
{
    public interface IUserRepository
    {
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}