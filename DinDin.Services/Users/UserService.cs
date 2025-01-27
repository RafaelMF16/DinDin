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

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id)
                ?? throw new ArgumentNullException($"Not find user with id: {id}");

            return user;
        }

        public void Delete(int id)
        {
            try
            {
                _userRepository.Delete(id);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Update(User user)
        {
            try
            {
                _userValidator.ValidateAndThrow(user);
                _userRepository.Update(user);
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