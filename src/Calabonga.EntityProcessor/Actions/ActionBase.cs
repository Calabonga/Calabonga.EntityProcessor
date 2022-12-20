namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Базовый класс реализующий действия над сущностью. Действия могут быть получены из контейнера, так и созданы явно в месте запуска.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class ActionBase<TEntity> : IAction<TEntity> where TEntity : class
{
    /// <summary>
    /// Возвращает результат применения действия <see cref="EntityActionResult"/> для сущности. Реализация этого метода
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    public virtual bool IsShouldBeHandled { get; } = false;

    public virtual string? Name { get; } = null;
}