using CompanyProfile.Models;

namespace CompanyProfile.Repository
{
    public interface ICompanyDepartmentsRepository
    {
        Task<bool> AddCompanyDepartmentsAsync(CompanyDepartments companyDepartments);
        Task<bool> UpdateCompanyDepartmentsAsync(string companyEmail, CompanyDepartments updatedDepartments);
        Task<CompanyDepartments> GetCompanyDepartmentsByEmailAsync(string companyEmail);
    }
}
