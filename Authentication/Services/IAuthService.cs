using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Services
{
    public interface IAuthService
    {
        string generateTokenString(User user);
        Task<bool> Login(User user);
        Task<IdentityResult> RegisterUser(User user);
    }
}