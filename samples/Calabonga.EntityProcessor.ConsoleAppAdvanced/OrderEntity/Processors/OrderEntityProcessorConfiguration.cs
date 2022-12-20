using Calabonga.EntityProcessor;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors;

public class OrderEntityProcessorConfiguration: EntityProcessorConfiguration<Order>
{
    public bool IsProcessingEnabled { get; set; }
}