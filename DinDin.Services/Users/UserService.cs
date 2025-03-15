using DinDin.Domain.Users;
using DinDin.Services.Auth;
using FluentValidation;

namespace DinDin.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly AuthService _authService;

        public UserService(
            IUserRepository userRepository, 
            IValidator<User> userValidator,
            AuthService authService)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _authService = authService;
        }

        public async Task Add(User user)
        {
            try
            {
                _userValidator.ValidateAndThrow(user);
                user.Password = _authService.HashPassword(user.Password);
                await _userRepository.Add(user);
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

        public User GetById(string id)
        {
            return _userRepository.GetById(id)
                ?? throw new ArgumentNullException($"Not find user with id: {id}");
        }

        public void Delete(string id)
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