using Microsoft.Extensions.Logging;

namespace Calabonga.Shared.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger) => _logger = logger;

    public async Task NotifyErrorAsync(string message)
    {
        _logger.LogInformation("[{Name}]: Sending error notification: {Message}", GetType().Name, message);
        await Task.Delay(1200);
        _logger.LogInformation("[{Name}]: Error notification sent", GetType().Name);
    }

    public async Task NotifySuccessAsync()
    {
        _logger.LogInformation("[{Name}]: Sending success notification", GetType().Name);
        await Task.Delay(1200);
        _logger.LogInformation("[{Name}]: Success notification sent", GetType().Name);
    }
}