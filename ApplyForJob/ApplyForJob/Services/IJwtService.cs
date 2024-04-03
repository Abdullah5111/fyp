namespace ApplyForJob.Services
{
    public interface IJwtService 
    {
       Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);
    }
}
