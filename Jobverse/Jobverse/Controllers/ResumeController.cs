using DinkToPdf;
using DinkToPdf.Contracts;
using Jobverse.Models;
using Jobverse.Models.Resume.Resume;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add this line
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jobverse.Controllers
{
    public class ResumeController : Controller
    {
        private readonly PdfService _pdfService;
        private readonly ILogger<ResumeController> _logger; // Add this line

        public ResumeController(PdfService pdfService, ILogger<ResumeController> logger)
        {
            _pdfService = pdfService;
            _logger = logger;
        }

        public IActionResult buildResume()
        {
            _logger.LogInformation("Rendering buildResume view.");
            return View();
        }

        public IActionResult reviewResume(ResumeModel m)
        {
            _logger.LogInformation("Rendering reviewResume view.");
            return View(m);
        }

        public async Task<IActionResult> GeneratePdf(ResumeModel model)
        {
            _logger.LogInformation("Generating PDF.");

            try
            {
                string htmlContent = _pdfService.ConvertModelToHtml(model);
                var pdfBytes = _pdfService.GeneratePdf(htmlContent);

                using (var pdfStream = new MemoryStream(pdfBytes))
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("https://localhost:7142/");

                        var formContent = new MultipartFormDataContent();
                        formContent.Add(new StreamContent(pdfStream), "file", "Resume.pdf");

                        try
                        {
                            var response = await httpClient.PostAsync("api/resume", formContent);

                            if (response.IsSuccessStatusCode)
                            {
                                var resultString = await response.Content.ReadAsStringAsync();

                                var contentType = response.Content.Headers.ContentType?.MediaType;

                                if (contentType != null && contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                                {
                                    var result = JsonConvert.DeserializeObject<ResumePdf>(resultString);

                                    _logger.LogInformation("PDF generated and uploaded successfully.");

                                    return File(pdfBytes, "application/pdf", "Resume.pdf");
                                }
                                else
                                {
                                    _logger.LogError("Unexpected response content type.");
                                    return BadRequest("Unexpected response content type");
                                }
                            }
                            else
                            {
                                _logger.LogError($"Error uploading PDF to API. Status code: {response.StatusCode}");
                                return StatusCode((int)response.StatusCode, "Error uploading PDF to API");
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            _logger.LogError($"HTTP request error: {ex.Message}");
                            return BadRequest($"HTTP request error: {ex.Message}");
                        }
                        catch (JsonException ex)
                        {
                            _logger.LogError($"Error deserializing JSON response: {ex.Message}");
                            return BadRequest("Error deserializing JSON response");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating PDF: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

