using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDepartmentsController : ControllerBase
    {
        private readonly ICompanyDepartmentsRepository _companyDepartmentsRepository;

        public CompanyDepartmentsController(ICompanyDepartmentsRepository companyDepartmentsRepository)
        {
            _companyDepartmentsRepository = companyDepartmentsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyDepartments([FromBody] CompanyDepartments companyDepartments)
        {
            try
            {
                bool result = await _companyDepartmentsRepository.AddCompanyDepartmentsAsync(companyDepartments);

                if (result)
                {
                    return Ok("Company departments added successfully.");
                }

                return BadRequest("Failed to add company departments.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<bool>> UpdateCompanyDepartmentsAsync(string email, [FromBody] CompanyDepartments updatedDepartments)
        {
            try
            {
                var result = await _companyDepartmentsRepository.UpdateCompanyDepartmentsAsync(email, updatedDepartments);

                if (!result)
                {
                    return BadRequest($"Failed to update company departments for email {email}.");
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<CompanyDepartments>> GetCompanyDepartmentsByEmailAsync(string email)
        {
            try
            {
                var departments = await _companyDepartmentsRepository.GetCompanyDepartmentsByEmailAsync(email);

                if (departments != null)
                {
                    return Ok(departments);
                }

                return NotFound($"Company departments not found for email {email}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
