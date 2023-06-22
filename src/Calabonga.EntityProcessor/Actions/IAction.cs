using MediatR;

namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Базовый класс реализующий действия над сущностью. Действия могут быть получены из контейнера, так и созданы явно в месте запуска.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAction<TEntity> : IRequest, IHaveName where TEntity : class
{
    /// <summary>
    /// Возвращает результат применения действия <see cref="EntityActionResult{TEntity}"/> для сущности. Реализация этого метода
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>результат обработки сущности</returns>
    Task<EntityActionResult<TEntity>> ApplyAsync(TEntity? entity, CancellationToken cancellationToken);

    /// <summary>
    /// Должно ли действие быть перехвачено
    /// </summary>
    bool IsShouldBeHandled { get; }

    EntityProcessorContext Context { get; }

    void SetContext(EntityProcessorContext context);
}