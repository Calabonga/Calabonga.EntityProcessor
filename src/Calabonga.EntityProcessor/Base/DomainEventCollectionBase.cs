using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor.Base;

public abstract class DomainEventCollectionBase: IHaveDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;
    
    public void AddDomainEvent(IDomainEvent domainCommand) => _domainEvents.Add(domainCommand);
}