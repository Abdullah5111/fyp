using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInsightsController : ControllerBase
    {
        private readonly ICompanyInsightsRepository _companyInsightsRepository;

        public CompanyInsightsController(ICompanyInsightsRepository companyInsightsRepository)
        {
            _companyInsightsRepository = companyInsightsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyInsights([FromBody] CompanyInsights companyInsights)
        {
            try
            {
                bool result = await _companyInsightsRepository.AddCompanyInsightsAsync(companyInsights);

                if (result)
                {
                    return Ok("Company insights added successfully.");
                }

                return BadRequest("Failed to add company insights.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("{email}")]
        public async Task<ActionResult<CompanyInsights>> GetCompanyInsightsByEmail(string email)
        {
            try
            {
                var insights = await _companyInsightsRepository.GetCompanyInsightsByEmailAsync(email);

                if (insights != null)
                {
                    return Ok(insights);
                }

                return NotFound($"Company insights not found for email {email}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPut("{email}")]
        public async Task<ActionResult<bool>> UpdateCompanyInsights(string email, [FromForm] CompanyInsights updatedInsights)
        {
            try
            {
                var result = await _companyInsightsRepository.UpdateCompanyInsightsAsync(email, updatedInsights);

                if (result)
                {
                    return Ok(true);
                }

                return BadRequest($"Failed to update company insights for email {email}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
