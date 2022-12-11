using Calabonga.EntityProcessor.Events;
using Calabonga.Shared.OrderEntity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Events;

public record OrderUpdatedStartCalculationCommand(Order Order) : IDomainCommand;

public class OrderUpdatedStartCalculationCommandHandler : IRequestHandler<OrderUpdatedStartCalculationCommand, Unit>
{
    private readonly ILogger<OrderUpdatedStartCalculationCommandHandler> _logger;

    public OrderUpdatedStartCalculationCommandHandler(ILogger<OrderUpdatedStartCalculationCommandHandler> logger) => _logger = logger;

    public Task<Unit> Handle(OrderUpdatedStartCalculationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Command handled]: {ActionName} for Order {Id} with title {Name}", this.GetType().Name, request.Order.Id, request.Order.Title);
        return Unit.Task;
    }
}