using jobPosting.DbContexts;
using jobPosting.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using jobPosting.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JobPostingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IJobPostingRepository, JobPostingRepository>();
builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host("rabbitmq://localhost", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("SharedContent.Messages:JWTokenJobPosting", ep => {
            ep.Consumer<JWTokenJobPostingConsumer>();
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
