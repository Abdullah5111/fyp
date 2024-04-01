using MassTransit;

using Authentication.Contexts;
using Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;

namespace Authentication
{
    public class Program
    {
        private static byte[] GenerateEncryptionKey()
        {
            using (var deriveBytes = new Rfc2898DeriveBytes("Jobverse", saltSize: 16, iterations: 1000))
            {
                return deriveBytes.GetBytes(32);
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 

            var encryptionKey = GenerateEncryptionKey();

            builder.Services.AddSingleton(encryptionKey);

            builder.Services.AddScoped<ITokenEncryptionService, TokenEncryptionService>();

            builder.Services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(new Uri("rabbitmq://localhost"), h => {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });

            // Add services to the container.
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection(
                    "ConnectionStrings:DefaultConnection").Value);
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                };
            });

            builder.Services.AddTransient<IAuthService, AuthService>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}