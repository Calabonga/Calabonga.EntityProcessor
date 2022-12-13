using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Коллекция зарегистрированных событий домена для контекста <see cref="EntityProcessorContext"/> определяет возможность
/// регистрировать события домена <see cref="IDomainEvent"/> в процессе обработки команд <see cref="IAction{TEntity}"/>
/// </summary>
public abstract class DomainEventCollectionBase : IHaveDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <inheritdoc />
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    /// <summary>
    /// Регистрирует новое событие домена в списке событий на исполнения после выполнения <see cref="IAction{TEntity}"/> при условии, что включена обработка в конфигурации <see cref="EntityProcessorConfiguration.AutoFireDomainEvents"/>
    /// </summary>
    /// <param name="domainEvent">событие домена</param>
    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}