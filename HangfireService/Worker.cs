using DbOptimizer.Core.Interfaces;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IDoctorRepository _doctorRepository;

    public Worker(ILogger<Worker> logger, IDoctorRepository doctorRepository)
    {
        _logger = logger;
        _doctorRepository = doctorRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // updsate doc status
                await _doctorRepository.UpdateHighFeeDoctorsStatusAsync();
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("High fee doctors status updated at: {time}", DateTimeOffset.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating high fee doctors status.");
            }

            await Task.Delay(1000 * 60 * 5, stoppingToken);
        }
    }
}
