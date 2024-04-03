using ApplyForJob.Models;

namespace ApplyForJob.Repository
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllJobApplications();
        Task<JobApplication> GetJobApplicationById(int id);
        Task<JobApplication> AddJobApplication(JobApplication jobApplication);
        Task<JobApplication> UpdateJobApplication(JobApplication jobApplication);
        Task DeleteJobApplication(int id);
    }
}
