using jobPosting.Models;
using jobPosting.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace jobPosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingRepository _jobPostingRepository;

        public JobPostingController(IJobPostingRepository jobPostingRepository)
        {
            _jobPostingRepository = jobPostingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosting>>> Get()
        {
            var jobPosts = await this._jobPostingRepository.GetAllJobPosts();
            return Ok(jobPosts);
        }


        [HttpPost]
        public async Task<ActionResult<JobPosting>> Post([FromBody] JobPosting jobPosting)
        {
            var createdJobPosting = await this._jobPostingRepository.AddJobPosts(jobPosting);
            return CreatedAtAction(nameof(Get), new { id = createdJobPosting.Id }, createdJobPosting);
        }

        

    }
}
