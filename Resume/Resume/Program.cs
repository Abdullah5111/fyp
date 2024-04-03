using Microsoft.EntityFrameworkCore;
using Resume.RabbitMQ;
using Resume.Repository;
using MassTransit;
using EventBus.Events;
using MassTransit.Transports.Fabric;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework Core and SQL Server support
builder.Services.AddDbContext<Resume.ResumeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IResumeRepository,
ResumeRepository>();

builder.Services.AddScoped<IResumeIdProducer, ResumeIdProducer>();

builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(new Uri("rabbitmq://localhost"), h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
