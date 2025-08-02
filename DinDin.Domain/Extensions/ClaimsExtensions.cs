using System.Security.Claims;

namespace DinDin.Domain.Extensions
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier) 
                ?? throw new UnauthorizedAccessException("User ID claim not found.");

            if (!int.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("Invalid user ID claim.");

            return userId;
        }
    }
}
