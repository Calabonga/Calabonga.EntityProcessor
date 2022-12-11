using MediatR;

namespace Calabonga.EntityProcessor.Events;

public interface IDomainNotification : IDomainEvent, INotification {} 