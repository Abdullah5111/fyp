using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyReviewController : ControllerBase
    {
        private readonly ICompanyReviewRepository _companyReviewRepository;

        public CompanyReviewController(ICompanyReviewRepository companyReviewRepository)
        {
            _companyReviewRepository = companyReviewRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyReview([FromBody] CompanyReview reviewInput)
        {
            try
            {
                await _companyReviewRepository.AddCompanyReviewAsync(reviewInput);
                return Ok("Company review added successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<CompanyReview>>> GetReviews(string email)
        {
            try
            {
                var reviews = await _companyReviewRepository.GetReviewsAsync(email);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("average-rating/{companyEmail}")]
        public async Task<ActionResult<double>> GetAverageRating(string companyEmail)
        {
            try
            {
                var averageRating = await _companyReviewRepository.CalculateAverageRatingAsync(companyEmail);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
