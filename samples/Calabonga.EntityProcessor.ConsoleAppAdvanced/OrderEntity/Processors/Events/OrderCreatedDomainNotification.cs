using Calabonga.EntityProcessor.Events;
using Calabonga.Shared.OrderEntity;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Events;

public record OrderCreatedDomainNotification(Order Order) : IDomainNotification;

public partial class OrderCreatedDomainNotificationHandler : INotificationHandler<OrderCreatedDomainNotification>
{

    private readonly ILogger<OrderStateChangedDomainNotificationHandler> _logger;

    public OrderCreatedDomainNotificationHandler(ILogger<OrderStateChangedDomainNotificationHandler> logger)
        => _logger = logger;


    public async Task Handle(OrderCreatedDomainNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Notification handled]: {Name}", notification.GetType().Name);
        _logger.LogInformation("[ORDER_CREATED]: {Name}", JsonSerializer.Serialize(notification.Order, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }
}