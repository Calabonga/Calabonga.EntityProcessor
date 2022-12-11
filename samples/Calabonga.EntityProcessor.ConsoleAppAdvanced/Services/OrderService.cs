using Calabonga.ConsoleAppAdvanced.OrderEntity.Processors;
using Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Actions;
using Calabonga.OperationResults;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;

namespace Calabonga.ConsoleAppAdvanced.Services;

public class OrderService : IOrderService
{
    private readonly OrderEntityProcessor _orderProcessor;
    private readonly ChangeStateToPublishedAction _changeStateToPublishedAction;

    public OrderService(
        OrderEntityProcessor orderProcessor,
        ChangeStateToPublishedAction changeStateToPublishedAction)
    {
        _orderProcessor = orderProcessor;
        _changeStateToPublishedAction = changeStateToPublishedAction;
    }

    public async Task<OperationResult<Order>> CreateAsync(OrderState state, Order order)
    {
        var operationResult = OperationResult.CreateResult<Order>();
        var executionResult = await _orderProcessor.ProcessAsync(order, _changeStateToPublishedAction);
        if (executionResult.Ok)
        {
            operationResult.Result = executionResult.Entity!;
            return operationResult;
        }

        var errors = string.Join(" ", executionResult.Validations!.Select(x => x.ErrorMessage));
        operationResult.AddError(errors);
        return operationResult;
    }

    public async Task<OperationResult<Order>> UpdateStateAsync(OrderState state, Order order)
    {
        var operationResult = OperationResult.CreateResult<Order>();
        var executionResult = await _orderProcessor.ProcessAsync(order, _changeStateToPublishedAction);
        if (executionResult.Ok)
        {
            operationResult.Result = executionResult.Entity!;
            return operationResult;
        }

        var errors = string.Join(" ", executionResult.Validations!.Select(x => x.ErrorMessage));
        operationResult.AddError(errors);
        return operationResult;
    }
}