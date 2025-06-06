using DinDin.Domain.Constantes;

namespace DinDin.Services.Auth
{
    public class AuthService
    {
        public async Task<string> HashPassword(string password)
        {
            return await Task.Run(() => 
                BCrypt.Net.BCrypt.HashPassword(password, ApplicationConstants.WORK_FACTOR));
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}