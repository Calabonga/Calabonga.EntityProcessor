using MediatR;

namespace Calabonga.EntityProcessor.Actions;

public interface IAction<in TEntity> : IRequest, IHaveName where TEntity : class
{
    Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    bool IsShouldBeHandled { get; }

}