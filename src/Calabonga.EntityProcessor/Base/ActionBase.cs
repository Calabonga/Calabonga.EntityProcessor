using Calabonga.EntityProcessor.Actions;

namespace Calabonga.EntityProcessor.Base;

public abstract class ActionBase<TEntity> : IAction<TEntity> where TEntity : class
{
    public abstract Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context);

    public virtual bool IsShouldBeHandled { get; } = false;
}