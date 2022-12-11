using Calabonga.EntityProcessor.Events;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Events;

public record OrderStateChangedDomainNotification(OrderState State) : IDomainNotification;

public partial class OrderStateChangedDomainNotificationHandler : INotificationHandler<OrderStateChangedDomainNotification>
{
    private readonly IStatisticService _statisticService;

    private readonly ILogger<OrderStateChangedDomainNotificationHandler> _logger;

    public OrderStateChangedDomainNotificationHandler(
        ILogger<OrderStateChangedDomainNotificationHandler> logger,
        IStatisticService statisticService)
    {
        _logger = logger;
        _statisticService = statisticService;
    }

    public async Task Handle(OrderStateChangedDomainNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Notification handled]: {Name}", notification.GetType().Name);
        await _statisticService.StartRecalculationAsync();
    }
}