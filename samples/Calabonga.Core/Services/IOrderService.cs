using Calabonga.OperationResults;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.Shared.Services;

public interface IOrderService
{
    Task<OperationResult<Order>> CreateAsync(OrderState state, Order order);

    Task<OperationResult<Order>> UpdateStateAsync(OrderState state, Order order);
}