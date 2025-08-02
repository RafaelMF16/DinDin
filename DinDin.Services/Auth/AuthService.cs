using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DinDin.Domain.Auth;
using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using DinDin.Services.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DinDin.Services.Auth
{
    public class AuthService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

        public async Task<string> HashPassword(string password)
        {
            return await Task.Run(() =>
                BCrypt.Net.BCrypt.HashPassword(password, ApplicationConstants.WORK_FACTOR));
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<LoginResponseDto> GenerateTokens(int userId)
        {
            var secretKey = GetSecretKey();

            var accessToken = GenerateAccessToken(secretKey, userId);
            var refreshToken = GenerateRefreshToken(secretKey);
            var hashedRefreshToken = HashRefreshToken(refreshToken, secretKey);

            await SaveRefreshToken(hashedRefreshToken, userId);

            return new LoginResponseDto(accessToken, refreshToken);
        }

        public async Task<LoginResponseDto?> VerifyRefreshToken(string refreshToken)
        {
            var secretKey = GetSecretKey();

            var hashedRefreshToken = HashRefreshToken(refreshToken, secretKey);

            var databaseRefreshToken = await _refreshTokenRepository.GetValidTokenByTokenHash(hashedRefreshToken);

            var isValid = ValidateRefreshToken(databaseRefreshToken);

            if (!isValid)
                return null;

            await RevokeRefreshToken(databaseRefreshToken);
            return await GenerateTokens(databaseRefreshToken.UserId);
        }

        private static bool ValidateRefreshToken(RefreshToken? refreshToken)
        {
            if (refreshToken is null)
                return false;

            if (refreshToken.Revoked)
                return false;

            return !(refreshToken.ExpiresAt <= DateTime.UtcNow);
        }

        public async Task LogoutUser(string refreshToken)
        {
            var secretKey = GetSecretKey();

            var hashedRefreshToken = HashRefreshToken(refreshToken, secretKey);

            var databaseRefreshToken = await _refreshTokenRepository.GetValidTokenByTokenHash(hashedRefreshToken);

            await RevokeRefreshToken(databaseRefreshToken!);
        }

        private async Task RevokeRefreshToken(RefreshToken databaseRefreshToken)
        {
            databaseRefreshToken.Revoked = true;
            await _refreshTokenRepository.UpdateRevoked(databaseRefreshToken);
        }

        private static string GenerateAccessToken(string secretKey, int userId)
        {
            var claims = new ClaimsIdentity([new(ClaimTypes.NameIdentifier, userId.ToString())]);
            var encodedSecretKey = Encoding.ASCII.GetBytes(secretKey);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(ApplicationConstants.ACCESS_TOKEN_EXPIRATION_MINUTES),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }

        private string GetSecretKey()
        {
            return _configuration[ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE]
                ?? throw new Exception($"Environment variable [{ApplicationConstants.SECRET_KEY_ENVIRONMENT_VARIABLE}] not found");
        }

        private static string GenerateRefreshToken(string secretKey)
        {
            return Guid.NewGuid().ToString();
        }

        private async Task SaveRefreshToken(string token, int userId)
        {
            var refreshToken = new RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(ApplicationConstants.REFRESH_TOKEN_EXPIRATION_DAYS),
                Revoked = false,
                TokenHash = token,
                UserId = userId
            };

            await _refreshTokenRepository.Add(refreshToken);
        }

        private static string HashRefreshToken(string refreshToken, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var tokenBytes = Encoding.UTF8.GetBytes(refreshToken);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(tokenBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}