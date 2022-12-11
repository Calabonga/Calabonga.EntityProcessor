using Calabonga.OperationResults;
using Calabonga.Shared.OrderEntity;
using Calabonga.Shared.Services;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleApp
{
    public class ControllerEmulator
    {
        private readonly ILogger<ControllerEmulator> _logger;
        private readonly IOrderService _orderService;

        public ControllerEmulator(ILogger<ControllerEmulator> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task CreateOrderAsync()
        {
            _logger.LogInformation("[{Name}] begin", nameof(CreateOrderAsync));

            // Order 1
            var order = new Order
            {
                Id = 1,
                CreatedAt = DateTime.UtcNow,
                Description = "No description",
                State = OrderState.Draft,
                Title = "Title 1",
                Price = 10,
                IsEnabled = false
            };

            var executionResult = await _orderService.UpdateStateAsync(OrderState.WaitingPayment, order);
            if (!executionResult.Ok)
            {
                var message = executionResult.GetMetadataMessages();
                _logger.LogError("[Error]:{Error}", message);
            }

            _logger.LogInformation("[{Name}] completed", nameof(CreateOrderAsync));
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _logger.LogInformation("[{Name}] begin", nameof(UpdateOrderAsync));
            var executionResult = await _orderService.UpdateStateAsync(OrderState.WaitingPayment, order);
            if (!executionResult.Ok)
            {
                var message = executionResult.GetMetadataMessages();
                _logger.LogError("[Error]:{Error}", message);
            }

            _logger.LogInformation("[{Name}] completed", nameof(UpdateOrderAsync));
        }
    }
}
