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
        public async Task<IActionResult> SignupSuccess(Authentication.Models.User user)
        {
            try
            {
                Console.WriteLine("herere");
                var apiUrl = "https://localhost:7065/api/Auth/Register";
                Console.WriteLine("herere233322");
                var result = await PostToApiAsync(apiUrl, user);
                Console.WriteLine(result);
                if (result == HttpStatusCode.Conflict)
                {
                    ViewBag.ErrorMessage = "User with the same email already exists.";
                    return View("~/Views/Employee/SignupEmployee.cshtml");
                }
                if((int)result==200)
                {
                    Console.WriteLine(result);
                    return RedirectToAction("Index", "Home");
                }
                return View("~/Views/Employee/SignupEmployee.cshtml");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                return StatusCode(500, "Internal Server Error");
            }
        }
        private async Task<HttpStatusCode> PostToApiAsync(string apiUrl, object data)
        {
            
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync(apiUrl, content);
            Console.WriteLine("hereresajjad");
            return (response.StatusCode);
        }
    }
}
