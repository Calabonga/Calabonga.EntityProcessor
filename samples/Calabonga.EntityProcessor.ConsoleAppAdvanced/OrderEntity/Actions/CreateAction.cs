using Calabonga.ConsoleAppAdvanced.OrderEntity.Events;
using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Base;
using Calabonga.Shared.OrderEntity;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Actions;

public class CreateAction : ActionBase<Order>
{
    private readonly ILogger<CreateAction> _logger;

    public CreateAction(ILogger<CreateAction> logger) => _logger = logger;

    public override Task<EntityActionResult> ApplyAsync(Order entity, EntityProcessorContext context)
    {
        _logger.LogInformation("Saving {Name} to database", nameof(Order));

        _logger.LogInformation("Creating required notification and command");
        var result = new EntityActionResult();

        _logger.LogInformation("[Notification registered]: {Name}", nameof(OrderCreatedDomainNotification));
        result.AddDomainEvent(new OrderCreatedDomainNotification(entity));

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