using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CompanyProfile.Repository
{
    public class JwtClass
    {
        private readonly string _secretKey;

        public JwtClass(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateJwtToken(string companyEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, companyEmail)
                    // You can add more claims as needed (e.g., roles, user ID, etc.)
                }),
                Expires = DateTime.UtcNow.AddHours(24), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
