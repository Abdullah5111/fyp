using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.EntityFrameworkCore;

namespace CompanyProfile.Models
{
    public class CompanyProfileRepository : ICompanyProfileRepository
    {
        private readonly CompanyContext _context;

        public CompanyProfileRepository(CompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CompanyProfile> GetCompanyProfileByEmailAsync(string email)
        {
            return await _context.CompanyProfiles.SingleOrDefaultAsync(cp => cp.CompanyEmail == email);
        }

        public async Task<bool> AddCompanyProfileAsync(CompanyProfile companyProfile)
        {
            // Check if the email exists in the Company table
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Email == companyProfile.CompanyEmail);

            if (existingCompany == null)
            {
                return false;
            }
            _context.CompanyProfiles.Add(companyProfile);
            await _context.SaveChangesAsync();
            return true; // Return true if the addition is successful.
        }


        public async Task<bool> UpdateCompanyProfileAsync(string email, CompanyProfile updatedProfile)
        {
            var existingProfile = await _context.CompanyProfiles.SingleOrDefaultAsync(cp => cp.CompanyEmail == email);

            if (existingProfile != null)
            {
                existingProfile.Name = updatedProfile.Name;
                existingProfile.Location = updatedProfile.Location;
                existingProfile.Logo = updatedProfile.Logo;
                existingProfile.PhoneNumber=updatedProfile.PhoneNumber;
                existingProfile.Bio=updatedProfile.Bio;
                existingProfile.NumberOfEmployees = updatedProfile.NumberOfEmployees;
                existingProfile.Facebook = updatedProfile.Facebook;
                existingProfile.Website=updatedProfile.Website;
                existingProfile.LinkedIn=updatedProfile.LinkedIn;
                existingProfile.Twitter = updatedProfile.Twitter;
                await _context.SaveChangesAsync();
                return true; 
            }

            return false;
        }

    }
}
