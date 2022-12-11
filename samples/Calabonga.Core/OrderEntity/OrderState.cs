namespace Calabonga.Shared.OrderEntity;

public enum OrderState
{
    None,
    Draft,
    WaitingPayment,
    Completed,
    Deleted
}