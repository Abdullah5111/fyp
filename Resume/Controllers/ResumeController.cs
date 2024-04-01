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
        public ResumeController(IResumeRepository resumeRepository, IResumeIdProducer resumeIdProducer)
        {
            _resumeRepository = resumeRepository;
            _resumeIdProducer = resumeIdProducer;
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
        public async Task<ActionResult<ResumePdf>> Post([FromForm(Name = "Pdf")] IFormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var resume = new ResumePdf { UserId = userId, Pdf = ms.ToArray() };

                var addedResume = await _resumeRepository.AddResume(resume);

                _resumeIdProducer.SendResumeIdMessage(addedResume.ResumeId);

                Console.WriteLine("Resume uploaded successfully");

                return Ok(addedResume);
            }
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
