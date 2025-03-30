using System.Security.Claims;

namespace DigitalWallet.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim == null || !Guid.TryParse(claim.Value, out var guid))
                throw new UnauthorizedAccessException("Invalid or missing user ID in token.");

            return guid;
        }

    }
}
