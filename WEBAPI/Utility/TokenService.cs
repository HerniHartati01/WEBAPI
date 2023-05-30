using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using WEBAPI.Contracts;
using WEBAPI.ViewModels.Others;

namespace WEBAPI.Utility
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimVM ExtractClaimsFromJwt(string token) //Extraxt Payload 
        {
            if (token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                //Configure the token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                                                               .UTF8
                                                               .GetBytes(_configuration["JWT:Key"])),
                };

                // Parse and Validate the JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, 
                                                                 tokenValidationParameters, 
                                                                 out var securityToken);

                //Extract the Claims from Jwt Token
                if (securityToken !=null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };

                    var roles = identity.Claims
                                        .Where(c => c.Type == ClaimTypes.Role)
                                        .Select(claim => claim.Value).ToList();


                    claims.Roles = roles;
                    return claims;
                }
 
            }
            catch 
            { 
                return new ClaimVM();
            }

            return new ClaimVM();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(IEnumerable<Claim> claims) 
        {
            var secretKey = new SymmetricSecurityKey(Encoding
                                                    .UTF8
                                                    .GetBytes(_configuration["JWT:Key"]));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims:claims,
                                                    expires: DateTime.Now.AddMinutes(10),
                                                    signingCredentials:signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
