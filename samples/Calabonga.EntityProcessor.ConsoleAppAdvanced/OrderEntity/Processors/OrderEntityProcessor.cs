using Calabonga.Shared.OrderEntity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors;

public class OrderEntityProcessor : EntityProcessorBase<Order>
{
    private readonly IEnumerable<IAction<Order>> _actions;

    public OrderEntityProcessor(
        IEnumerable<IAction<Order>> actions,
        IMediator mediator,
        EntityProcessorConfiguration? configuration,
        ILogger<OrderEntityProcessor> logger,
        IEnumerable<IRule<Order>> rules) : base(mediator,
        configuration,
        logger,
        rules)
        => _actions = actions;

    public Task<ExecutionResultBase<Order>> CreateOrderAsync(Order order)
    {
        var action = _actions.SingleOrDefault(x => x.Name == "Create");
        if (action is null)
        {
            throw new EntityProcessorInvalidOperationException("Action not found");
        }

        return ProcessAsync(order, action);
    }
}