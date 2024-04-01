using CompanyProfile.Models;

namespace CompanyProfile.Repository
{
    public interface ICompanyReviewRepository
    {
        Task<bool> AddCompanyReviewAsync(CompanyReview reviewInput);
        Task<double> CalculateAverageRatingAsync(string companyEmail);
        Task<IEnumerable<CompanyReview>> GetReviewsAsync(string companyEmail);
    }
}
