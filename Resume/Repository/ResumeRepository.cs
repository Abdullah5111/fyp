using Microsoft.EntityFrameworkCore;

namespace Resume.Repository
{
    public class ResumeRepository:IResumeRepository
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
            var resume =  await _context.resumes.FindAsync(id);
            return resume;
        }
    }
}
