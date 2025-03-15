using DinDin.Domain.Constantes;
using System.Text;
using DinDin.Domain.Users;
using DinDin.Services.Auth;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null || !_authService.VerifyPassword(password, user.Password))
                return null;

            var secretKey = Environment.GetEnvironmentVariable(ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE)
                ?? throw new Exception($"Environment variable [{ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE}] not found");

            var encodedSecretKey = Encoding.ASCII.GetBytes(secretKey);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier, user.Id!)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }
    }
}