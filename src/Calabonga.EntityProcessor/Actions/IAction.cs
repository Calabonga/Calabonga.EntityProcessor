using MediatR;

namespace Calabonga.EntityProcessor.Actions;

public interface IAction<in TEntity> : IRequest 
    where TEntity: class
{
    Task<EntityActionResult> ApplyAsync(TEntity entity, EntityProcessorContext context);
    
    bool IsShouldBeHandled { get; }
}