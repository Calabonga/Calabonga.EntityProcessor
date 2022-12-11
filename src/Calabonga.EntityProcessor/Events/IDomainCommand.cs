using MediatR;

namespace Calabonga.EntityProcessor.Events;

public interface IDomainCommand : IDomainEvent, IRequest {}
