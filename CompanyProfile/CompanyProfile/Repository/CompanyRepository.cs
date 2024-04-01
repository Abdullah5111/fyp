using CompanyProfile.Models;
using CompanyProfile.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CompanyRepository : ICompanyRepository
{
    private readonly CompanyContext _context;

    public CompanyRepository(CompanyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company> GetCompanyByEmailAsync(string email)
    {
        return await _context.Companies.FindAsync(email);
    }
    public async Task<Company> AuthenticateAsync(string email, string password)
    {
        //Console.WriteLine(passwr);
        var company = await _context.Companies.SingleOrDefaultAsync(x => x.Email == email);

        // Check if the company exists and if the password matches
        if (company != null && company.Password == password)
        {
            Console.WriteLine("Password matches");
            return company; // Authentication successful
        }

        return null;  // Authentication successful
    }

    public async Task<int> AddCompanyAsync(Company company)
    {
        //Console.WriteLine("Yess here ");
        var companyExists = await _context.Companies.AnyAsync(x => x.Email == company.Email);

        if (companyExists)
        {
            return (307);
        }
        else
        {
            var addedCompany = _context.Companies.Add(company);
            await SaveChangesAsync();
            Console.WriteLine("Yess here ");
            return (200);
        }
        
    }

    public async Task UpdateCompanyAsync(Company company)
    {
        _context.Entry(company).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task DeleteCompanyAsync(string email)
    {
        var company = await _context.Companies
        .Include(c => c.CompanyProfile) // Include the navigation property
        .FirstOrDefaultAsync(c => c.Email == email);

        if (company != null)
        {
            // Remove the company
            _context.Companies.Remove(company);

            await SaveChangesAsync();
        }
    }
    public async Task<bool> UpdateCompanyEmailInCompanyProfileAsync(string existingEmail, string newEmail)
    {
        try
        {
            // Check if the new email already exists in the Company table
            var emailExists = await _context.Companies.AnyAsync(c => c.Email == newEmail);
            if (emailExists)
            {
                // New email already exists
                return false;
            }

            // Check if the existing email exists in the Company table
            var existingCompany = await _context.Companies
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(c => c.Email == existingEmail);

            if (existingCompany == null)
            {
                // Existing email not found in Company table
                return false;
            }

            // Update the email in the Company table
            existingCompany.Email = newEmail;

            // Update the email in the associated CompanyProfile, if available
            if (existingCompany.CompanyProfile != null)
            {
                existingCompany.CompanyProfile.CompanyEmail = newEmail;
            }

            await SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            // Handle exceptions if necessary
            return false;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
