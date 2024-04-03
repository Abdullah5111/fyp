using ApplyForJob.Models;
using Microsoft.EntityFrameworkCore;
using ApplyForJob.DbContexts;

namespace ApplyForJob.Repository
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobApplicationContext _context;

        public JobApplicationRepository(JobApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplication>> GetAllJobApplications()
        {
            return await _context.JobApplications.ToListAsync();
        }

        public async Task<JobApplication> GetJobApplicationById(int id)
        {
            return await _context.JobApplications.FindAsync(id);
        }

        public async Task<JobApplication> AddJobApplication(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
            return jobApplication;
        }

        public async Task<JobApplication> UpdateJobApplication(JobApplication jobApplication)
        {
            _context.Entry(jobApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return jobApplication;
        }

        public async Task DeleteJobApplication(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
                await _context.SaveChangesAsync();
            }
        }
    }
}
