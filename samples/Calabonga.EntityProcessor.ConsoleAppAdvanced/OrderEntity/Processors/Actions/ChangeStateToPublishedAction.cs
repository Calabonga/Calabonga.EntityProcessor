using Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Events;
using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Actions;
using Calabonga.Shared.OrderEntity;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Actions;

public class ChangeStateToPublishedAction : ActionBase<Order>
{
    private readonly ILogger<ChangeStateToPublishedAction> _logger;

    public ChangeStateToPublishedAction(ILogger<ChangeStateToPublishedAction> logger) => _logger = logger;

    public override string? Name => "UpdateState";

    public override Task<EntityActionResult> ApplyAsync(Order entity, EntityProcessorContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Changing state from {OldState} to Published for {Name}, {ID}", entity.State, nameof(Order), entity.Id);
        entity.State = OrderState.WaitingPayment;
        _logger.LogInformation("Changed to Published successful");

        _logger.LogInformation("Creating required notification and command");
        var result = new EntityActionResult();

        _logger.LogInformation("[Notification registered]: {Name}", nameof(OrderStateChangedDomainNotification));
        result.AddDomainEvent(new OrderStateChangedDomainNotification(entity.State));
        _logger.LogInformation("[Command registered]: {Name}", nameof(OrderUpdatedStartCalculationCommand));
        result.AddDomainEvent(new OrderUpdatedStartCalculationCommand(entity));

        if (true != context.DomainEvents.Any())
        {
            _logger.LogInformation("There are not other domain events found");
            return Task.FromResult(result);
        }

        // collecting events from EntityProcessorContext
        foreach (var domainEvent in context.DomainEvents)
        {
            _logger.LogInformation("[Events from context registered]: {Name}", domainEvent.GetType().Name);
            result.AddDomainEvent(domainEvent);
        }

        return Task.FromResult(result);
    }
}