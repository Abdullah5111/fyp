using CompanyProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyProfile.Repository
{
    public class CompanyDepartmentsRepository : ICompanyDepartmentsRepository
    {
        private readonly CompanyContext _context;

        public CompanyDepartmentsRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCompanyDepartmentsAsync(CompanyDepartments companyDepartments)
        {
            try
            {
                var existingCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Email == companyDepartments.CompanyEmail);

                if (existingCompany == null)
                {
                    return false;
                }
                _context.CompanyDepartments.Add(companyDepartments);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCompanyDepartmentsAsync(string companyEmail, CompanyDepartments updatedDepartments)
        {
            try
            {
                var existingDepartments = await _context.CompanyDepartments
                    .FirstOrDefaultAsync(cd => cd.CompanyEmail == companyEmail);

                if (existingDepartments != null)
                {
                    existingDepartments.Department1 = updatedDepartments.Department1;
                    existingDepartments.Department2 = updatedDepartments.Department2;
                    existingDepartments.Role1 = updatedDepartments.Role1;
                    existingDepartments.Salary1 = updatedDepartments.Salary1;
                    existingDepartments.Role2 = updatedDepartments.Role2;
                    existingDepartments.Salary2 = updatedDepartments.Salary2;
                    existingDepartments.Role3 = updatedDepartments.Role3;
                    existingDepartments.Salary3 = updatedDepartments.Salary3;
                    existingDepartments.Role4 = updatedDepartments.Role4;
                    existingDepartments.Salary4 = updatedDepartments.Salary4;
                    existingDepartments.Role5 = updatedDepartments.Role5;
                    existingDepartments.Salary5 = updatedDepartments.Salary5;
                    existingDepartments.Role6 = updatedDepartments.Role6;
                    existingDepartments.Salary6 = updatedDepartments.Salary6;
                    existingDepartments.Role7 = updatedDepartments.Role7;
                    existingDepartments.Salary7 = updatedDepartments.Salary7;
                    existingDepartments.Role8 = updatedDepartments.Role8;
                    existingDepartments.Salary8 = updatedDepartments.Salary8;
                    existingDepartments.Role9 = updatedDepartments.Role9;
                    existingDepartments.Salary9 = updatedDepartments.Salary9;
                    // Repeat for other properties

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; // If the department with the given email is not found
            }
            catch (Exception)
            {
                // Handle exceptions if needed
                return false;
            }
        }

        public async Task<CompanyDepartments> GetCompanyDepartmentsByEmailAsync(string companyEmail)
        {
            try
            {
                var departments = await _context.CompanyDepartments
                    .FirstOrDefaultAsync(cd => cd.CompanyEmail == companyEmail);

                return departments;
            }
            catch (Exception)
            {
                // Handle exceptions if needed
                return null;
            }
        }
    }
}
