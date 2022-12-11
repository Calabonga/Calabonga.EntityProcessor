using Calabonga.OperationResults;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.Shared.Services;

public interface IOrderService
{
    Task<OperationResult<Order>> CreateAsync(Order order);

    Task<OperationResult<Order>> UpdateStateAsync(OrderState state, Order order);
}