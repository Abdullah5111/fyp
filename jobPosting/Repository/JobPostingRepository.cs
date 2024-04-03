using jobPosting.Models;
using Microsoft.EntityFrameworkCore;
using jobPosting.DbContexts;
using Microsoft.AspNetCore.Builder;

namespace jobPosting.Repository
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly JobPostingContext _context;

        public JobPostingRepository(JobPostingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobPosting>> GetAllJobPosts()
        {
            return await _context.JobPosts.ToListAsync();
        }


        public async Task<JobPosting> AddJobPosts(JobPosting jobPosting)
        {
            _context.JobPosts.Add(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

    }
    
}
