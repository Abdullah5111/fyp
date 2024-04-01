using Authentication.Models;
using Authentication.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedContent.Messages;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITokenEncryptionService _encryptionService;

        public AuthController(IAuthService authService, IPublishEndpoint publishEndpoint, ITokenEncryptionService encryptionService)
        {
            _authService = authService;
            _publishEndpoint = publishEndpoint;
            _encryptionService = encryptionService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterUser(user);
                if (result.Succeeded)
                {
                    int code = 200;
                    string message = "Register successful";

                    var tokenString = _authService.generateTokenString(user);
                    var encryptedToken = _encryptionService.EncryptToken(tokenString);

                    // Here the jwtoken is publishing to the queues
                    var jwToken = new JWToken();
                    jwToken.TokenString = tokenString;
                    await _publishEndpoint.Publish<JWToken>(jwToken);

                    var jwTokenJobPosting = new JWTokenJobPosting();
                    jwTokenJobPosting.TokenString = encryptedToken;
                    await _publishEndpoint.Publish<JWTokenJobPosting>(jwTokenJobPosting);

                    var jwTokenApplyForJob = new JWTokenApplyForJob();
                    jwTokenApplyForJob.TokenString = encryptedToken;
                    Console.WriteLine("Encrypted token from auth to applyforjob: ");
                    Console.WriteLine(encryptedToken);
                    await _publishEndpoint.Publish<JWTokenApplyForJob>(jwTokenApplyForJob);

                    var jwTokenResume = new JWTokenResume();
                    jwTokenResume.TokenString = encryptedToken;
                    await _publishEndpoint.Publish<JWTokenResume>(jwTokenResume);

                    return StatusCode(code, new { Code = code, Message = message, Token = tokenString });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    int code = 409;
                    string message = string.Join(", ", errors);
                    Console.WriteLine("Error Register User");

                    return StatusCode(code, new { Code = code, Message = message });
                }
            }

            return BadRequest("Somthing went wrong!");
           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            if(ModelState.IsValid)
            {
                var result = await _authService.Login(user);
                if (result == true)
                {
                    var tokenString = _authService.generateTokenString(user);
                    var encryptedToken = _encryptionService.EncryptToken(tokenString);

                    // Here the jwtoken is publishing to the queues
                    var jwToken = new JWToken();
                    jwToken.TokenString = tokenString;
                    await _publishEndpoint.Publish<JWToken>(jwToken);

                    var jwTokenJobPosting = new JWTokenJobPosting();
                    jwTokenJobPosting.TokenString = encryptedToken;
                    await _publishEndpoint.Publish<JWTokenJobPosting>(jwTokenJobPosting);

                    var jwTokenApplyForJob = new JWTokenApplyForJob();
                    jwTokenApplyForJob.TokenString = encryptedToken;
                    Console.WriteLine(jwTokenApplyForJob.TokenString);
                    await _publishEndpoint.Publish<JWTokenApplyForJob>(jwTokenApplyForJob);

                    var jwTokenResume = new JWTokenResume();
                    jwTokenResume.TokenString = encryptedToken;
                    await _publishEndpoint.Publish<JWTokenResume>(jwTokenResume);

                    return Ok(tokenString);
                }
                return BadRequest("Incorrect password or username");
            }

            return BadRequest("Something went wrong");
        }
    }
}
