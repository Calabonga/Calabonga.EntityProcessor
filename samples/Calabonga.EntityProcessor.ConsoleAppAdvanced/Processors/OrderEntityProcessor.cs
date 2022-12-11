using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.Processors;

public class OrderEntityProcessor : EntityProcessorBase<Order>
{
    public OrderEntityProcessor(
        IMediator mediator,
        EntityProcessorConfiguration? configuration,
        ILogger<OrderEntityProcessor> logger,
        IEnumerable<IRule<Order>> rules) : base(mediator,
        configuration,
        logger,
        rules)
    {
    }
}