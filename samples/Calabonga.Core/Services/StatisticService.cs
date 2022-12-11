using Microsoft.Extensions.Logging;

namespace Calabonga.Shared.Services;

public class StatisticService : IStatisticService
{
    private readonly ILogger<StatisticService> _logger;

    public StatisticService(ILogger<StatisticService> logger) => _logger = logger;

    public async Task StartRecalculationAsync()
    {
        _logger.LogInformation("[{Name}]: Re-calculation is started", GetType().Name);
        await Task.Delay(5400);
        _logger.LogInformation("[{Name}]: Re-calculation is Completed", GetType().Name);
    }
}