using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor.Actions;

public interface IHaveDomainEvents
{
    public IEnumerable<IDomainEvent> DomainEvents { get; }
}