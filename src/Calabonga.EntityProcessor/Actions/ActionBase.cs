namespace Calabonga.EntityProcessor.Actions;

public abstract class ActionBase<TEntity> : IAction<TEntity> where TEntity : class
{
    public abstract Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    public virtual bool IsShouldBeHandled { get; } = false;

    public virtual string? Name { get; } = null;
}