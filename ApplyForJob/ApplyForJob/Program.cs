using ApplyForJob.DbContexts;
using ApplyForJob.Repository;
using ApplyForJob.ResumeIDEventConsumer;
using ApplyForJob.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
//using MassTransit;
//using ApplyForJob.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JobApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddSingleton<RabbitMQ.RabbitMQ>();
builder.Services.AddTransient<ITokenReceiverHandler<ApplyForJobJwtEvent>, TokenReceiverHandler>();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SendResumeEventConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("send-resume-event-queue", ep =>
        {
            ep.ConfigureConsumer<SendResumeEventConsumer>(ctx);
        });
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
