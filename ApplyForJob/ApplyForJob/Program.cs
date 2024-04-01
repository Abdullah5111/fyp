using ApplyForJob.DbContexts;
using ApplyForJob.Repository;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using ApplyForJob.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JobApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host("rabbitmq://localhost", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("SharedContent.Messages:JWTokenJobPosting", ep =>
        {
            ep.Consumer<JWTokenApplyForJobConsumer>();
        });

        cfg.ReceiveEndpoint("SharedContent.Messages:ResumeId", ep =>
        {
            ep.Consumer<ResumeIdApplyForJobConsumer>();
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
