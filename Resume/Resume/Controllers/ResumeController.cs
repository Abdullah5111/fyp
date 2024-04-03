using AutoMapper;
using EventBus.Events;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Resume.RabbitMQ;
using Resume.Repository;
using Resume.Resume;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Resume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeIdProducer _resumeIdProducer;
        private readonly IResumeRepository _resumeRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ResumeController(IResumeRepository resumeRepository, IResumeIdProducer resumeIdProducer, IPublishEndpoint publishEndpoint)
        {
            _resumeRepository = resumeRepository;
            _resumeIdProducer = resumeIdProducer;
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        // GET: api/<ResumeController>
        [HttpGet]
        public  Task<IEnumerable<ResumePdf>> Get()
        {
            var resumesList = _resumeRepository.getAllResumes();
            return (resumesList); 
        }

        // GET api/<ResumeController>/5
        [HttpGet("{id}")]
        public  Task<ResumePdf?> Get(int id)
        {
            return _resumeRepository.getResumebyid(id);
        }

        // POST api/<ResumeController>
        [HttpPost]
        public async Task<ActionResult<ResumePdf>> Post([FromForm] IFormFile file, [FromForm] string userEmail)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email is required");
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var resume = new ResumePdf { userEmail = userEmail, Pdf = ms.ToArray() };

                var addedResume = await _resumeRepository.AddResume(resume);

                //_resumeIdProducer.SendResumeIdMessage(addedResume.ResumeId);
                try
                {
                    int rResumeId = addedResume.ResumeId;
                    string targetConsumer = "consumer1";

                    var eventMessage = new SendResumeEvent
                    {
                        rResumeId = rResumeId,
                        TargetConsumer = targetConsumer
                    };

                    await _publishEndpoint.Publish(eventMessage);

                    // Message published successfully
                    Console.WriteLine("Message published successfully.");
                }
                catch (Exception ex)
                {
                    // Exception occurred during publishing
                    Console.WriteLine($"Error publishing message: {ex.Message}");
                    // Optionally, you can log the exception or perform additional error handling here
                }

                return Ok(addedResume);
            }
        }

		//GET api/<ResumeController>
		[HttpGet("resumes/{email}")]
		public async Task<ActionResult<List<ResumePdf>>> GetResumes(string email)
		{
            Console.WriteLine("In Api");
			// Call the repository method to get resumes by email
			List<ResumePdf> resumes = await _resumeRepository.getResumes(email);

			// Check if resumes are found
			if (resumes == null || resumes.Count == 0)
			{
                Console.WriteLine("Uuuuuuuuu");
				return NotFound(); // Return 404 if no resumes found
			}

			return resumes;
		}
		// PUT api/<ResumeController>/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResumeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
