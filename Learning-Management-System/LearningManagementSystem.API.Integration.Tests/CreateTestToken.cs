using LearningManagementSystem.API.Integration.Tests.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningManagementSystem.API.Integration.Tests
{
    public static class CreateTestToken
    {
        public static string CreateToken()
        {
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                issuer: JwtTokenProvider.Issuer,
                audience: JwtTokenProvider.Issuer,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Name, "Test"),
                    new Claim(ClaimTypes.NameIdentifier, "ca5096cd-acf0-4d28-ac1b-38aa4556c85d"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
        }
    }
}
