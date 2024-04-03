using Jobverse.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jobverse.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly HttpClient _httpClient;
        
        public JobPostingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7199/");
            
        }

        public async Task<IActionResult> JobPosting(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostJob(JobPosting jobPosting)
        {
            Console.WriteLine("Salary Range: " + jobPosting.SalaryRange);
            Console.WriteLine("Location: " + jobPosting.Location);
            Console.WriteLine("Job Description: " + jobPosting.JobDescription);
            Console.WriteLine("Company: " + jobPosting.Company);
            Console.WriteLine("Experience: " + jobPosting.Experience);
            Console.WriteLine("Job Title: " + jobPosting.JobTitle);

            if (ModelState.IsValid)
            {
                try
                {
                    string apiEndpoint = "api/JobPosting";

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(jobPosting), Encoding.UTF8, "application/json");

                   

                    // Make a POST request to the API endpoint
                    var response = await _httpClient.PostAsync(apiEndpoint, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(new { success = true, message = "Job Posted successfully" });
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
            else
            {
                return Json(new { success = false, message = "Model validation failed" });
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}