using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LearningManagementSystem.API.Integration.Tests.Base
{
    public static class JwtTokenProvider
    {
        public static string Issuer { get; } = "Sample_Auth_Server";
        public static SecurityKey SecurityKey { get; } = 
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("O_cheie_secreta_foarte_foarte_foarte_bine_ascunsa_pe_care_o_cunoaste_toata_lumea")
            );
        public static SigningCredentials SigningCredentials { get; } =
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        internal static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    }
}
