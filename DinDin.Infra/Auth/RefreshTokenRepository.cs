using DinDin.Domain.Auth;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;

namespace DinDin.Infra.Auth
{
    public class RefreshTokenRepository(DinDinDbContext dbContext) : IRefreshTokenRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task Add(RefreshToken refreshToken)
        {
            var refreshTokenModel = new RefreshTokenModel
            {
                CreatedAt = refreshToken.CreatedAt,
                ExpiresAt = refreshToken.ExpiresAt,
                Revoked = refreshToken.Revoked,
                TokenHash = refreshToken.TokenHash,
                UserId = refreshToken.UserId,
            };

            await _dbContext.AddAsync(refreshTokenModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}