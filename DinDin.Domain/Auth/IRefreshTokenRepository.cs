namespace DinDin.Domain.Auth
{
    public interface IRefreshTokenRepository
    {
        Task Add(RefreshToken refreshToken);
        Task<RefreshToken?> GetValidTokenByTokenHash(string hashedRefreshToken);
        Task<RefreshToken?> GetValidTokenByUserId(int userId);
        Task UpdateRevoked(RefreshToken refreshToken);
    }
}