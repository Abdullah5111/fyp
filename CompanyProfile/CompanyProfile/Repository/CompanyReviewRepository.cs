using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompanyProfile.Models;

namespace CompanyProfile.Repository
{
    public class CompanyReviewRepository:ICompanyReviewRepository
    {
        private readonly CompanyContext _context;

        public CompanyReviewRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCompanyReviewAsync(CompanyReview reviewInput)
        {
            try
            {
                var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Email == reviewInput.CompanyEmail);

                if (existingCompany == null)
                {
                    return false;
                }
                _context.CompanyReviews.Add(reviewInput);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<double> CalculateAverageRatingAsync(string companyEmail)
        {
            var reviews = await GetReviewsAsync(companyEmail);

            if (reviews.Any())
            {
                double totalRating = reviews.Sum(r => r.Rating);
                return totalRating / reviews.Count();
            }

            return 0; // No reviews yet
        }

        public async Task<IEnumerable<CompanyReview>> GetReviewsAsync(string companyEmail)
        {
            return await _context.CompanyReviews
                .Where(r => r.CompanyEmail == companyEmail)
                .ToListAsync();
        }
    }
}
