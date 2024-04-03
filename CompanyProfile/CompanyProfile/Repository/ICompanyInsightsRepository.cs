using CompanyProfile.Models;

namespace CompanyProfile.Repository
{
    public interface ICompanyInsightsRepository
    {
        Task<bool> AddCompanyInsightsAsync(CompanyInsights insights);

        Task<CompanyInsights> GetCompanyInsightsByEmailAsync(string email);

        Task<bool> UpdateCompanyInsightsAsync(string email, CompanyInsights updatedInsights);
    }
}
