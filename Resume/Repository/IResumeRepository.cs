using System.Collections.Generic;

namespace Resume.Repository
{
    public interface IResumeRepository
    {
        Task<IEnumerable<ResumePdf>> getAllResumes();
        Task <ResumePdf>getResumebyid(int resumeid);
        Task<ResumePdf> AddResume(ResumePdf resumePdf);
    }
}
