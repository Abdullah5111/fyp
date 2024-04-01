using CompanyProfile.Models;
namespace CompanyProfile.Repository
{
    public interface ICompanyProfileRepository
    {
        Task<CompanyProfile.Models.CompanyProfile> GetCompanyProfileByEmailAsync(string email);

        Task<bool> AddCompanyProfileAsync(CompanyProfile.Models.CompanyProfile companyProfile);

        Task<bool> UpdateCompanyProfileAsync(string email, CompanyProfile.Models.CompanyProfile updatedProfile);
    }
}
