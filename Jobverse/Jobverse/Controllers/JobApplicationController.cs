using Jobverse.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jobverse.Controllers
{
	public class JobApplicationController : Controller
	{
		private readonly HttpClient _httpClient;

		public JobApplicationController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			//_httpClient.BaseAddress = new Uri("https://localhost:7025/");
			//7025
		}
		public async Task<IActionResult> JobApplication(int jobId)
		{
			List<ResumePdf> resumes = await getResumesEmailAsync("usamaikram228@gmail.com");
			if (Request.Cookies["Username"] != null)
			{
				Console.WriteLine("cookie exists");
				Console.WriteLine(Request.Cookies["Username"] == "");
				Console.WriteLine(Request.Cookies["Username"]);
				ViewBag.JobId = jobId;
				return View(resumes);
			}
			else
			{
				//return RedirectToAction("SignupEmployee", "Employee");
				//Console.WriteLine(jobId);
				ViewBag.JobId = jobId;
				return View(resumes);
			}
		}
		private async Task<List<ResumePdf>> getResumesEmailAsync(string email)
		{
			var response = await _httpClient.GetAsync($"https://localhost:7142/api/resume/resumes/{email}");
			List<ResumePdf> resumes = new List<ResumePdf>();
			//Console.WriteLine(response);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				//Console.WriteLine(content);
				Console.Write("aaaaa");
				resumes = JsonConvert.DeserializeObject<List<ResumePdf>>(content);
				return resumes;
			}
			else
			{
				return resumes;
			}


		}
		[HttpPost]
		public async Task<IActionResult> SaveApplication(int JobId, string Applier, string UserEmail, string PhoneNumber, IFormFile resume, int Experience, int resumeId)
		{
			try
			{
				if (resume != null)
				{
					var addedResumeResponse = await AddResumeAsync(UserEmail, resume, "https://localhost:7142/");
					//Console.WriteLine(addedResumeResponse);
					// Check if adding resume was successful
					if (!addedResumeResponse.IsSuccessStatusCode)
					{
						return Json(new { success = false, message = "Error adding resume" });
					}
					var addedResume = JsonConvert.DeserializeObject<ResumePdf>(await addedResumeResponse.Content.ReadAsStringAsync());
					resumeId = addedResume.resumeId;
				}
				var saveApplicationResponse = await SaveJobApplicationAsync(JobId, Applier, UserEmail, PhoneNumber, resumeId, Experience, "https://localhost:7025/");

				//Console.WriteLine("//////");
				//Console.WriteLine(saveApplicationResponse);
				// Check if saving the job application was successful
				if (!saveApplicationResponse.IsSuccessStatusCode)
				{
					return Json(new { success = false, message = "Error saving job application" });
				}

				return View();
			}
			catch (Exception ex)
			{
				// Handle any exceptions
				return Json(new { success = false, message = "Error: " + ex.Message });
			}
		}

		// Method to add resume
		private async Task<HttpResponseMessage> AddResumeAsync(string userEmail, IFormFile resume, string baseUrl)
		{

			using (var ms = new MemoryStream())
			{

				await resume.CopyToAsync(ms);

				var resumeContent = new ByteArrayContent(ms.ToArray());
				resumeContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
				var formData = new MultipartFormDataContent();
				Console.WriteLine(resumeContent);
				formData.Add(resumeContent, "file", resume.FileName);
				formData.Add(new StringContent(userEmail), "userEmail");
				Console.WriteLine("Form Data: ", formData);

				return await _httpClient.PostAsync(baseUrl + "api/resume", formData);
			}
		}

		// Method to save job application
		private async Task<HttpResponseMessage> SaveJobApplicationAsync(int jobId, string applier, string userEmail, string phoneNumber, int addedResume, int experience, string baseUrl)
		{
			var jobApplication = new JobApplication
			{
				Applier = applier,
				UserEmail = userEmail,
				JobId = jobId,
				PhoneNumber = phoneNumber,
				ResumeId = addedResume,
				Experience = experience
			};

			var jsonContent = new StringContent(JsonConvert.SerializeObject(jobApplication), Encoding.UTF8, "application/json");
			return await _httpClient.PostAsync(baseUrl + "api/JobApplication", jsonContent);
		}

		public IActionResult Success()
		{
			return View();
		}
	}
}
