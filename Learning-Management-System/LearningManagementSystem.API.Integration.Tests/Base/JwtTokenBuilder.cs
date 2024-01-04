using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningManagementSystem.API.Integration.Tests.Base
{
    public class JwtTokenBuilder
    {
        public List<Claim> Claims { get; set; } = new();
        public int ExpiresInMinutes { get; set; } = 60;

        public JwtTokenBuilder WithRole(string roleName)
        {
            Claims.Add(new Claim(ClaimTypes.Role, roleName));
            return this;
        }

        public JwtTokenBuilder WithUserName(string userName)
        {
            Claims.Add(new Claim(ClaimTypes.Name, userName));
            return this;
        }

        public JwtTokenBuilder WithNameIdentifier(string nameIdentifier)
        {
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
            return this;
        }

        public JwtTokenBuilder WithJwtId(string jwtId)
        {
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jwtId));
            return this;
        }

        public JwtTokenBuilder WithExpiresInMinutes(int expiresInMinutes)
        {
            ExpiresInMinutes = expiresInMinutes;
            return this;
        }

        public JwtTokenBuilder WithIssuer(string issuer)
        {
            Claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer));
            return this;
        }

        public JwtTokenBuilder WithAudience(string audience)
        {
            Claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            return this;
        }

        public string Build()
        {
            var token = new JwtSecurityToken(
                issuer: JwtTokenProvider.Issuer,
                audience: JwtTokenProvider.Issuer,
                claims: Claims,
                expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
                signingCredentials: JwtTokenProvider.SigningCredentials
            );
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
