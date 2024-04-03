using jobPosting.Models;

namespace jobPosting.Repository
{
    public interface IJobPostingRepository
    {
        Task<IEnumerable<JobPosting>> GetAllJobPosts();
        Task<JobPosting> AddJobPosts(JobPosting jobPosting);
        
    }
}
