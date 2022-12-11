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
    private readonly CreateAction _createAction;

    public OrderService(

        OrderEntityProcessor orderProcessor,
        ChangeStateToPublishedAction changeStateToPublishedAction,
        CreateAction createAction)
    {
        _orderProcessor = orderProcessor;
        _changeStateToPublishedAction = changeStateToPublishedAction;
        _createAction = createAction;
    }

    public async Task<OperationResult<Order>> CreateAsync(Order order)
    {
        var operationResult = OperationResult.CreateResult<Order>();

        var executionResult = await _orderProcessor.CreateOrderAsync(order);
        //var executionResult = await _orderProcessor.ProcessAsync(order, _createAction);
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