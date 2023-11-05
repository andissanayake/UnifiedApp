using Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.UserGroup
{
    public static class TokenUtil
    {

        public static string GetToken(AppSettings appSettings, ApplicationUser user, List<Claim> roleClaims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName??"")
            };
            userClaims.AddRange(roleClaims);
            var tokeOptions = new JwtSecurityToken(
                issuer: appSettings.Issuer,
                audience: appSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddSeconds(appSettings.TokenExpireSeconds),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(AppSettings appSettings, string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.Audience,
                ValidIssuer = appSettings.Issuer,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("GetPrincipalFromExpiredToken Token is not valiated");

            return principal;
        }
    }
}
