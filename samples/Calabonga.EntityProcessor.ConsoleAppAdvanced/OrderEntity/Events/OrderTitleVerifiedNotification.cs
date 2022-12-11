using Calabonga.EntityProcessor.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Events;

public record OrderTitleVerifiedNotification(int OrderId, bool IsValid) : IDomainNotification;

public class OrderTitleVerifiedNotificationHandler : INotificationHandler<OrderTitleVerifiedNotification>
{
    private readonly ILogger<OrderTitleVerifiedNotificationHandler> _logger;

    public OrderTitleVerifiedNotificationHandler(ILogger<OrderTitleVerifiedNotificationHandler> logger) => _logger = logger;

    public async Task Handle(OrderTitleVerifiedNotification notification, CancellationToken cancellationToken)
    {
            _logger.LogInformation("[Notification handled]: {Name}", notification.GetType().Name);
            await Task.Delay(400, cancellationToken);
            // send notification to editor?
    }
}