using Microsoft.AspNetCore.Mvc;
using Jobverse.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using Azure;
using System.Diagnostics.Metrics;

namespace Jobverse.Controllers
{
    public class CreateCompanyProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateCompanyProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult CreateCompanyProfile()
        {
            return View("~/Views/CompanyProfile/CreateCompanyProfile.cshtml");
        }
        public IActionResult SignupEmployer()
        {
            return View("~/Views/CompanyProfile/SignupEmployer.cshtml");
        }
        public IActionResult LoginEmployer()
        {
            return View("~/Views/CompanyProfile/LoginEmployer.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> SignupSuccess(Models.Company company)
        {
            Response.Cookies.Delete("Username");
            ViewBag.ShowSignup = true;
            try
            {
                var apiUrl = "https://localhost:7105/api/Company";
                var result = await PostToApiAsync(apiUrl, company);
                if (result.StatusCode == HttpStatusCode.Conflict)
                {
                    ViewBag.ErrorMessage = "Company with the same email already exists.";
                    return View("~/Views/CompanyProfile/SignupEmployer.cshtml");
                }

                return View("~/Views/CompanyProfile/CreateCompanyProfile.cshtml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginSuccess(Models.Company company)
        {
            Response.Cookies.Delete("Username");
            ViewBag.ShowSignup = true;
            Console.WriteLine(company.Email);
            try
            {
                var apiUrl = "https://localhost:7105/api/Company/login";
                var result = await PostToApiAsyncLogin(apiUrl, company);
                Console.WriteLine(result);
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.ErrorMessage = "Invalid credentials.";
                    return View("~/Views/CompanyProfile/LoginEmployer.cshtml");
                }
                if (result.IsSuccessStatusCode)
                {
                    return View("~/Views/CompanyProfile/CreateCompanyProfile.cshtml");
                }
                    //Console.WriteLine(result.StatusCode);

                    return View("~/Views/CompanyProfile/LoginEmployer.cshtml");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                return StatusCode(500, "Internal Server Error");
            }
        }
        private async Task<(string Result, HttpStatusCode StatusCode)> PostToApiAsync(string apiUrl, object data)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine((int)response.StatusCode);
            return (result, response.StatusCode);
        }
        private async Task<HttpResponseMessage> PostToApiAsyncLogin(string apiUrl, object data)
        {
       
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, content);
            Console.WriteLine(response);
            return (response);
        }
    }
}
