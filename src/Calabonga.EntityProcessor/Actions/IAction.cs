using MediatR;

namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Базовый класс реализующий действия над сущностью. Действия могут быть получены из контейнера, так и созданы явно в месте запуска.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAction<in TEntity> : IRequest, IHaveName where TEntity : class
{
    /// <summary>
    /// Возвращает результат применения действия <see cref="EntityActionResult"/> для сущности. Реализация этого метода
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>результат обработки сущности</returns>
    Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    /// <summary>
    /// Должно ли действие быть перехвачено
    /// </summary>
    bool IsShouldBeHandled { get; }
}