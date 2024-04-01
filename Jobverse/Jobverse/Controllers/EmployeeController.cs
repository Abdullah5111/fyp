using Microsoft.AspNetCore.Mvc;
using Jobverse.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using Authentication.Models;
namespace Jobverse.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult SignupEmployee()
        {
            return View("~/Views/Employee/SignupEmployee.cshtml");
        }
        public IActionResult LoginEmployee()
        {
            return View("~/Views/Employee/LoginEmployee.cshtml");
        }
        public IActionResult LogoutEmployee()
        {
            ViewBag.ShowSignup = true;
            Response.Cookies.Delete("Username");
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignupSuccess(Authentication.Models.User user)
        {
            try
            {
                var apiUrl = "https://localhost:7065/api/Auth/Register";
                var result = await PostToApiAsync(apiUrl, user);
                if (result == HttpStatusCode.Conflict)
                {
                    ViewBag.ErrorMessage = result;
                    return View("~/Views/Employee/SignupEmployee.cshtml");
                }
                if((int)result==200)
                {
                    Console.WriteLine(user.Username);
                    Response.Cookies.Append("Username", user.Username);
                    ViewBag.ShowSignup = false;
                    return RedirectToAction("Index", "Home");
                }
                return View("~/Views/Employee/SignupEmployee.cshtml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        public async Task<IActionResult> SigninSuccess(Authentication.Models.User user)
        {
            try
            {
                var apiUrl = "https://localhost:7065/api/Auth/Login";
                var result = await PostToApiAsync(apiUrl, user);
                Console.WriteLine(result);
                if (result == HttpStatusCode.Conflict)
                {
                    Console.WriteLine("Signin success conflict");
                    ViewBag.ErrorMessage = result;
                    return View("~/Views/Employee/LoginEmployee.cshtml");
                }
                if ((int)result == 200)
                {
                    Console.WriteLine("Signin success");
                    Console.WriteLine(user.Username);
                    Response.Cookies.Append("Username", user.Username);
                    ViewBag.ShowSignup = false;
                    return RedirectToAction("Index", "Home");
                }
                Console.WriteLine("Ambiguous response");
                return View("~/Views/Employee/LoginEmployee.cshtml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        private async Task<HttpStatusCode> PostToApiAsync(string apiUrl, object data)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync(apiUrl, content);
            return (response.StatusCode);
        }
    }
}
