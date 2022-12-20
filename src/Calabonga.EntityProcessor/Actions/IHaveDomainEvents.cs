using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Интерфейс для контекста <see cref="EntityProcessorContext"/> определяет возможность
/// регистрировать события домена <see cref="IDomainEvent"/> в процессе обработки команд <see cref="IAction{TEntity}"/>
/// </summary>
/// <remarks>
/// При разработки плагинов для включения их в конвейер обработки команд <see cref="IAction{TEntity}"/> можно
/// наследовать класс <see cref="DomainEventCollectionBase"/>, который предоставить возможность
/// регистрировать события домена для последующей обработки.
/// </remarks>
public interface IHaveDomainEvents
{
    /// <summary>
    /// Зарегистрированные события домена <see cref="IDomainEvent"/>
    /// </summary>
    public IEnumerable<IDomainEvent> DomainEvents { get; }
}