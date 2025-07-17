namespace DinDin.Domain.Auth
{
    public interface IRefreshTokenRepository
    {
        Task Add(RefreshToken refreshToken);
    }
}