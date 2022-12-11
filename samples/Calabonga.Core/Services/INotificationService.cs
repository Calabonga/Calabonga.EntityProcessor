namespace Calabonga.Shared.Services;

public interface INotificationService
{
    Task NotifyErrorAsync(string message);
    
    Task NotifySuccessAsync();
}