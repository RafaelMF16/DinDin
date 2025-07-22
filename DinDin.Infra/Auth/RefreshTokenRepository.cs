using DinDin.Domain.Auth;
using DinDin.Domain.MonthlySummaries;
using DinDin.Domain.Users;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<RefreshToken?> GetValidTokenByUserId(int userId)
        {
            var refreshTokenModel = await _dbContext.RefreshToken
                .AsNoTracking()
                .Where(refreshToken => refreshToken.UserId == userId && !refreshToken.Revoked)
                .FirstOrDefaultAsync();

            if (refreshTokenModel is null)
                return null;

            return new RefreshToken
            {
                Id = refreshTokenModel.Id,
                CreatedAt = refreshTokenModel.CreatedAt,
                ExpiresAt = refreshTokenModel.ExpiresAt,
                TokenHash = refreshTokenModel.TokenHash,
                UserId = refreshTokenModel.UserId,
                Revoked = refreshTokenModel.Revoked
            };
        }

        public async Task UpdateRevoked(RefreshToken refreshToken)
        {
            var refreshTokenModel = await _dbContext.RefreshToken.FindAsync(refreshToken.Id)
                ?? throw new ArgumentNullException($"Não foi encontrado um token válido vinculado ao usuário com id: {refreshToken.Id}");

            refreshTokenModel.Revoked = refreshToken.Revoked;

            await _dbContext.SaveChangesAsync();
        }
    }
}