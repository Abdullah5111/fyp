using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Resume.Repository
{
    public class ResumeRepository: IResumeRepository
    {
        private readonly ResumeContext _context;
        public ResumeRepository(ResumeContext context)
        {
            _context = context;
        }   

        public async Task<ResumePdf> AddResume(ResumePdf resumePdf)
        {
           _context.resumes.Add(resumePdf);
            await _context.SaveChangesAsync();
            return resumePdf;
        }
        public async Task<IEnumerable<ResumePdf>> getAllResumes()
        {
            return await _context.resumes.ToListAsync();
        }
		public async Task<ResumePdf?> getResumebyid(int id)
		{
			try
			{
				var resume = await _context.resumes.FindAsync(id);
				Console.WriteLine(resume.ResumeId);
				return resume;
			}
			catch (Exception ex)
			{
				// Log the exception or handle it appropriately
				Console.WriteLine(ex.Message);
				return null; // Return null or throw a custom exception
			}
		}

		public async Task<List<ResumePdf>> getResumes(string email)
		{
			// Query resumes where the email matches the provided email
			return await _context.resumes.Where(r => r.userEmail == email).ToListAsync();
		}
	}
}
