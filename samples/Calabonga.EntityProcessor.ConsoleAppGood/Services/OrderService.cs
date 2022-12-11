using Calabonga.OperationResults;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;

namespace Calabonga.ConsoleApp.Services;

public class OrderService : IOrderService
{
    public async Task<OperationResult<Order>> CreateAsync(Order order)
    {
        var operationResult = OperationResult.CreateResult<Order>();

        if (string.IsNullOrEmpty(order.Title))
        {
            // send notification to editor?
            var message = $"[NO_TITLE]: {nameof(Order)} title is empty";
            var exception = new ArgumentException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.CreatedAt.Date == default)
        {
            // send notification to editor?
            var message = $"[NO_DATE]: {nameof(Order)} creation date is not provided";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.CreatedAt.Date > DateTime.UtcNow.Date)
        {
            // send notification to administrator?
            var message = $"[FUTURE_DATE]: {nameof(Order)} creation date is in the future";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.Price <= 0)
        {
            // send notification to administrator?
            const string message = $"[NO_PRICE]: {nameof(Order)} has no price information";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        // SaveChangesAsync
        await Task.Delay(1000);

        operationResult.Result = order;
        return operationResult;
    }

    public async Task<OperationResult<Order>> UpdateStateAsync(OrderState state, Order order)
    {
        var operationResult = OperationResult.CreateResult<Order>();

        if (string.IsNullOrEmpty(order.Title))
        {
            // send notification to editor?
            var message = $"[NO_TITLE]: {nameof(Order)} title is empty";
            var exception = new ArgumentException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.CreatedAt.Date == default)
        {
            // send notification to editor?
            var message = $"[NO_DATE]: {nameof(Order)} creation date is not provided";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.CreatedAt.Date > DateTime.UtcNow.Date)
        {
            // send notification to administrator?
            var message = $"[FUTURE_DATE]: {nameof(Order)} creation date is in the future";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        if (order.Price <= 0)
        {
            // send notification to administrator?
            const string message = $"[NO_PRICE]: {nameof(Order)} has no price information";
            var exception = new InvalidOperationException(message);
            operationResult.AddError(exception);
            return operationResult;
        }

        order.State = state;

        // SaveChangesAsync
        await Task.Delay(1000);

        operationResult.Result = order;
        return operationResult;
    }
}