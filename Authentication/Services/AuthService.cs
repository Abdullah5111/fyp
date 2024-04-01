using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<IdentityResult> RegisterUser(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Username
            };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result;
        }

        public async Task<bool> Login(User user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Username);
            Console.WriteLine("Identity User");
            Console.WriteLine(identityUser);
            if(identityUser == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(identityUser, user.Password);

        }

        public string generateTokenString(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            SigningCredentials singingCred = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha512Signature);
            
            var securityToken = new JwtSecurityToken(
             claims: claims,
             expires: DateTime.Now.AddMinutes(60),
             issuer:_config.GetSection("Jwt:Issuer").Value,
             audience:_config.GetSection("Jwt:Audience").Value,
             signingCredentials:singingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;
        }
    }
}
