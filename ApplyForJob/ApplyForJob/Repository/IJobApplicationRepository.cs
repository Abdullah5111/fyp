using ApplyForJob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplyForJob.Repository
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllJobApplications();
        Task<JobApplication> GetJobApplicationById(int id);
        Task<IEnumerable<JobApplication>> GetJobApplicationsByUsername(string username);
        Task<JobApplication> AddJobApplication(JobApplication jobApplication);
        Task<JobApplication> UpdateJobApplication(JobApplication jobApplication);
        Task DeleteJobApplication(int id);
    }
}
