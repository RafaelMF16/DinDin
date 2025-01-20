using DinDin.Domain.Users;

namespace DinDin.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            _userRepository.Add(user);
        }
    }
}