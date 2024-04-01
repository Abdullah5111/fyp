using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CompanyProfile.Repository;
using CompanyProfile.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CompanyContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ICompanyRepository,CompanyRepository>();
builder.Services.AddTransient<ICompanyProfileRepository, CompanyProfileRepository>();
builder.Services.AddTransient<ICompanyDepartmentsRepository, CompanyDepartmentsRepository>();
builder.Services.AddTransient<ICompanyInsightsRepository, CompanyInsightsRepository>();
builder.Services.AddTransient<ICompanyReviewRepository, CompanyReviewRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:7289",          // Set your issuer
            ValidAudience = "http://localhost:7289",      // Set your audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr")) // Set your secret key
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtClass>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JWT:Secret"];
    return new JwtClass(secretKey);
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthorization();
app.MapControllers();
app.Run();
