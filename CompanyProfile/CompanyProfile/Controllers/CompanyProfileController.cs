using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly ICompanyProfileRepository _companyProfileRepository;
        public CompanyProfileController(ICompanyProfileRepository companyProfileRepository)
        {
            _companyProfileRepository = companyProfileRepository;
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCompanyProfileByEmail(string email)
        {
            try
            {
                var companyProfile = await _companyProfileRepository.GetCompanyProfileByEmailAsync(email);

                if (companyProfile == null)
                {
                    return NotFound($"Company profile with email {email} not found.");
                }

                return Ok(companyProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyProfile([FromForm] CompanyProfile.Models.CompanyProfile companyProfile)
        {
            try
            {
                // Check if the form data contains a file
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    // Get the logo file from form data
                    var logoFile = HttpContext.Request.Form.Files[0];

                    // Convert the logo file to byte array
                    byte[] logoBytes = await ConvertLogoToBytes(logoFile);

                    // Set the logo byte array in the companyProfile object
                    companyProfile.Logo = logoBytes;
                }

                bool result = await _companyProfileRepository.AddCompanyProfileAsync(companyProfile);

                if (result)
                {
                    return Ok("Company profile added successfully.");
                }

                return BadRequest("Failed to add company profile.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private async Task<byte[]> ConvertLogoToBytes(IFormFile logoFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await logoFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<bool>> UpdateCompanyProfileAsync(string email, [FromForm] CompanyProfile.Models.CompanyProfile updatedProfile)
        {
            try
            {
                // Check if the form data contains a file (logo)
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    // Get the logo file from form data
                    var logoFile = HttpContext.Request.Form.Files[0];

                    // Convert the logo file to byte array
                    byte[] logoBytes = await ConvertLogoToBytes(logoFile);

                    // Set the logo byte array in the updatedProfile object
                    updatedProfile.Logo = logoBytes;
                }

                var result = await _companyProfileRepository.UpdateCompanyProfileAsync(email, updatedProfile);

                if (!result)
                {
                    return BadRequest($"Failed to update company profile for email {email}.");
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
