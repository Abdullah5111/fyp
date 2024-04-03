using ApplyForJob.Models;
using ApplyForJob.Repository;
using ApplyForJob.ResumeIDEventConsumer;
using ApplyForJob.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplyForJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly RabbitMQ.RabbitMQ _rabbitMQ;
        private readonly ITokenReceiverHandler<ApplyForJobJwtEvent> _jwtEventHandler;
        public JobApplicationController(IJobApplicationRepository jobApplicationRepository, RabbitMQ.RabbitMQ rabbitMQConnection, ITokenReceiverHandler<ApplyForJobJwtEvent> jwtEventHandler)
        {
            //Console.WriteLine("Has many\n");
            _jobApplicationRepository = jobApplicationRepository;
            _rabbitMQ = rabbitMQConnection;
            _jwtEventHandler = jwtEventHandler;
            JwtService ser = new JwtService(_rabbitMQ, _jwtEventHandler);
            ser.StartConsumingJwts();
             //Console.WriteLine($"The token is {}\n");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplication>>> Get()
        {
            Console.WriteLine("jajajajajajaj\n");
            var jobApplications = await this._jobApplicationRepository.GetAllJobApplications();
            return Ok(jobApplications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> Get(int id)
        {
            var jobApplication = await this._jobApplicationRepository.GetJobApplicationById(id);
            if (jobApplication == null)
                return NotFound();
            return Ok(jobApplication);
        }

        [HttpPost]
        public async Task<ActionResult<JobApplication>> Post([FromBody] JobApplication jobApplication)
        {
            int id=SendResumeEventConsumer.GetId();
            Console.WriteLine($"The id is {id} \n");
            jobApplication.ResumeId = id;
            var createdJobApplication = await this._jobApplicationRepository.AddJobApplication(jobApplication);
            return CreatedAtAction(nameof(Get), new { id = createdJobApplication.Id }, createdJobApplication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
                return BadRequest();

            var updatedNotification = await this._jobApplicationRepository.UpdateJobApplication(jobApplication);
            if (updatedNotification == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobApplication = await this._jobApplicationRepository.GetJobApplicationById(id);
            if (jobApplication == null)
                return NotFound();

            await this._jobApplicationRepository.DeleteJobApplication(id);

            return NoContent();
        }
    }
}
