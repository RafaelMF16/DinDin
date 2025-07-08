using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using DinDin.Services.Auth;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace DinDin.Services.Users
{
    public class UserService(
        IUserRepository userRepository,
        IValidator<User> userValidator,
        AuthService authService,
        IConfiguration configuration)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IValidator<User> _userValidator = userValidator;
        private readonly AuthService _authService = authService;
        private readonly IConfiguration _configuration = configuration;

        public async Task Add(User user)
        {
            try
            {
                var validationResult = await _userValidator.ValidateAsync(user, options => 
                    options.IncludeRuleSets(ApplicationConstants.USER_CREATE_RULE_SET_NAME));

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);

                user.Password = await _authService.HashPassword(user.Password);
                await _userRepository.Add(user);
            }
            catch (ValidationException validationException)
            {
                throw new ValidationException(validationException.Errors);
            }
            catch (DbUpdateException dbUpdateException) when (dbUpdateException.InnerException is PostgresException postgresException && postgresException.SqlState == "23505")
            {
                throw new ValidationException(
                [
                    new FluentValidation.Results.ValidationFailure("Email", "Já existe um usuário com este e-mail.")
                ]);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<string?> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null || !_authService.VerifyPassword(password, user.Password))
                return null;

            var secretKey = _configuration[ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE]
                ?? throw new Exception($"Environment variable [{ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE}] not found");

            var encodedSecretKey = Encoding.ASCII.GetBytes(secretKey);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier, user.Id.ToString())
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