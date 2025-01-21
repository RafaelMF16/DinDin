using DinDin.Domain.Users;
using FluentValidation;

namespace DinDin.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;

        public UserService(IUserRepository userRepository, IValidator<User> userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public void Add(User user)
        {
            try
            {
                _userValidator.ValidateAndThrow(user);
                _userRepository.Add(user);
            }
            catch (ValidationException validationException)
            {
                throw new ValidationException(validationException.Errors);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}