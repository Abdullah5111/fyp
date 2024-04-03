using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly JwtClass _jwtClass;

        public CompanyController(ICompanyRepository companyRepository, JwtClass jwtClass)
        {
            _companyRepository = companyRepository;
            _jwtClass = jwtClass;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Company>> GetCompanyByEmail(string email)
        {
            var company = await _companyRepository.GetCompanyByEmailAsync(email);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
        [HttpPost]
        public async Task<ActionResult> AddCompany([FromBody] Company company)
        {
            // Check if the email already exists
            var existingCompany = await _companyRepository.GetCompanyByEmailAsync(company.Email);
            if (existingCompany != null)
            {
                return Conflict("Company with the same email already exists.");
            }

            // Add the new company
            var addCompanyResponse = await _companyRepository.AddCompanyAsync(company);
            if (addCompanyResponse == 200)
            {
                Console.WriteLine("yes its added ");
                return CreatedAtAction(nameof(GetCompanyByEmail), new { email = company.Email }, new { Message = "Company added successfully", StatusCode = 201, Company = company });

            }
            else
            {
                // Some error occurred while adding the company
                return StatusCode(addCompanyResponse);
            }
        }

        [HttpPut("{email}")]
        public async Task<bool> UpdateCompanyAsync(string email, [FromBody] Company newCompany)
        {
            try
            {
                // Check if the existing email exists
                var existingCompany = await _companyRepository.GetCompanyByEmailAsync(email);
                if (existingCompany == null)
                {
                    // Existing email not found
                    return false;
                }

                // Check if the new email already exists
                var emailExists = await _companyRepository.GetCompanyByEmailAsync(newCompany.Email);
                if (emailExists != null)
                {
                    // New email already exists
                    return false;
                }

                // Delete the existing company
                await _companyRepository.DeleteCompanyAsync(email);

                // Add the new company with updated values
                var updatedCompany = new Company
                {
                    Email = newCompany.Email,
                    Username = newCompany.Username,
                    Password = newCompany.Password
                    // Add other properties as needed
                };

                await _companyRepository.AddCompanyAsync(updatedCompany);
                return true;
            }
            catch (Exception)
            {
                // Handle exceptions if necessary
                return false;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            Console.WriteLine(Email);
            var company = await _companyRepository.AuthenticateAsync(Email, Password);
            if (company == null)
            {
               
                return Unauthorized("Invalid email or password");
            }
            var token = _jwtClass.GenerateJwtToken(company.Email);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(22)
            };
            Console.WriteLine(company.Password);
            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                CompanyEmail = company.Email
            });
            
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteCompany(string email)
        {
            await _companyRepository.DeleteCompanyAsync(email);
            return NoContent();
        }
    }
}
