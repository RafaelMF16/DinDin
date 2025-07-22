using DinDin.Domain.Auth;
using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using DinDin.Services.Auth;
using DinDin.Services.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DinDin.Services.Users
{
    public class UserService(
        IUserRepository userRepository,
        IValidator<User> userValidator,
        AuthService authService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IValidator<User> _userValidator = userValidator;
        private readonly AuthService _authService = authService;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

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

        public async Task<LoginResponseDto?> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null || !_authService.VerifyPassword(password, user.Password))
                return null;

            var token = await _refreshTokenRepository.GetValidTokenByUserId(user.Id);
            if (token is not null)
            {
                token.Revoked = true;
                await _refreshTokenRepository.UpdateRevoked(token);
            }

            return await _authService.GenerateTokens(user.Id);
        }

        public async Task LogoutUser(int userId)
        {
            var token = await _refreshTokenRepository.GetValidTokenByUserId(userId);
            token.Revoked = true;

            await _refreshTokenRepository.UpdateRevoked(token);
        }
    }
}