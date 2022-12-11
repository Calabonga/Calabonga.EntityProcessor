namespace Calabonga.Shared.OrderEntity;

public class Order
{
    public int Id { get; set; }

    public OrderState State { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Title { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public bool IsEnabled { get; set; }

    public decimal Price { get; set; }
}