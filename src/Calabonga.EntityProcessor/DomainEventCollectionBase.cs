using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Коллекция зарегистрированных событий домена для контекста <see cref="EntityProcessorContext"/> определяет возможность
/// регистрировать события домена <see cref="IDomainEvent"/> в процессе обработки команд <see cref="IAction{TEntity}"/>.
/// Реализация интерфейса <seealso cref="IHaveDomainEvents"/>
/// </summary>
/// <remarks>
/// При разработки плагинов для включения их в конвейер обработки команд <see cref="IAction{TEntity}"/> можно
/// наследовать класс <see cref="DomainEventCollectionBase"/>, который предоставить возможность
/// регистрировать события домена для последующей обработки.
/// </remarks>
public abstract class DomainEventCollectionBase : IHaveDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Зарегистрированные события домена <see cref="IDomainEvent"/>
    /// </summary>
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    /// <summary>
    /// Регистрирует новое событие домена в списке событий на исполнения после выполнения <see cref="IAction{TEntity}"/> при условии,
    /// что включена обработка в конфигурации <see cref="EntityProcessorConfiguration.AutoFireDomainEvents"/>
    /// </summary>
    /// <param name="domainEvent">событие домена</param>
    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}