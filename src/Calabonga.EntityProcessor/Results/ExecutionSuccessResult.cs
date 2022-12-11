using Calabonga.EntityProcessor.Actions;

namespace Calabonga.EntityProcessor.Results;

public class ExecutionSuccessResult<TEntity> : ExecutionResultBase<TEntity>
{
    public ExecutionSuccessResult(TEntity entity, EntityActionResult result) : base(entity, result)
    {
    }
}