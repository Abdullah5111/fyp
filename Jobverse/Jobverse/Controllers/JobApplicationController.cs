using ApplyForJob.Models;
using jobPosting.Models;
using Jobverse.Models;
using Jobverse.Services;
using Jobverse.Utils;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedContent.Messages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jobverse.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ITokenEncryptionService _encryptionService;

        public JobApplicationController(IHttpClientFactory httpClientFactory, ITokenEncryptionService encryptionService, IPublishEndpoint publishEndpoint)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7025/");
            _encryptionService = encryptionService;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IActionResult> JobApplication(int jobId)
        {
            // Check if the "username" cookie exists
            if (Request.Cookies["Username"] != null)
            {
                Console.WriteLine("cookie exists");
                Console.WriteLine(Request.Cookies["Username"]);

                ViewBag.JobId = jobId;
                return View();
            }
            else
            {
                return RedirectToAction("SignupEmployee", "Employee");
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveApplication([FromBody] JobApplication jobApplication)
        {
            try
            {
                var userResumeId = new ResumeId();
                userResumeId.UserResumeId = 50;
                await _publishEndpoint.Publish<ResumeId>(userResumeId);

                string encryptedToken = _encryptionService.EncryptToken(TokenManager.TokenString);

                jobApplication.UserEmail = Request.Cookies["Username"];
                string apiEndpoint = "api/JobApplication";

                var jsonContent = new StringContent(JsonConvert.SerializeObject(jobApplication), Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encryptedToken);

                var response = await _httpClient.PostAsync(apiEndpoint, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    return Json(new { success = true, message = "Application saved successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Error in API request" });
                }
            }

            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP request error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }

                return Json(new { success = false, message = "Error in HTTP request" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> MyJob()
        {
            if (Request.Cookies.ContainsKey("Username"))
            {
                try
                {
                    Console.WriteLine("In Myjob");
                    string username = Request.Cookies["Username"];

                    string encryptedToken = _encryptionService.EncryptToken(TokenManager.TokenString);

                    string apiEndpoint = $"https://localhost:7025/api/JobApplication/MyJob?username={username}";

                    // Sending token in the headers
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encryptedToken);

                    var response = await _httpClient.GetAsync(apiEndpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jobApplications = JsonConvert.DeserializeObject<List<JobApplication>>(content);
                        return View(jobApplications);
                    }
                    else
                    {

                        Console.WriteLine("Error in receiving user jobs");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("HTTP request error: " + ex.Message);
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("SignupEmployee", "Employee");
            }
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
