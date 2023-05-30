using System.Security.Claims;
using WEBAPI.ViewModels.Others;

namespace WEBAPI.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        ClaimVM ExtractClaimsFromJwt(string token);

    }
}
