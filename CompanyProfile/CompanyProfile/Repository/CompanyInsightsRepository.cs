using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompanyProfile.Models;

namespace CompanyProfile.Repository
{
    public class CompanyInsightsRepository : ICompanyInsightsRepository
    {
        private readonly CompanyContext _context;

        public CompanyInsightsRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCompanyInsightsAsync(CompanyInsights insights)
        {
            try
            {
                var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Email == insights.CompanyEmail);

                if (existingCompany == null)
                {
                    return false;
                }
                _context.CompanyInsights.Add(insights);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CompanyInsights?> GetCompanyInsightsByEmailAsync(string email)
        {
            try
            {
                return await _context.CompanyInsights
                    .Where(ci => ci.CompanyEmail == email)
                    .SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateCompanyInsightsAsync(string email, CompanyInsights updatedInsights)
        {
            try
            {
                var existingInsights = await _context.CompanyInsights
                    .Where(ci => ci.CompanyEmail == email)
                    .SingleOrDefaultAsync();

                if (existingInsights != null)
                {
                    // Update properties of existingInsights with values from updatedInsights
                    existingInsights.EstablishedSince = updatedInsights.EstablishedSince;
                    existingInsights.OrdersCompleted = updatedInsights.OrdersCompleted;
                    existingInsights.EstimatedRevenue = updatedInsights.EstimatedRevenue;
                    existingInsights.ProductsSold = updatedInsights.ProductsSold;
                    existingInsights.SatisfiedCustomers = updatedInsights.SatisfiedCustomers;
                    existingInsights.CustomerGrowthPercentage = updatedInsights.CustomerGrowthPercentage;

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
