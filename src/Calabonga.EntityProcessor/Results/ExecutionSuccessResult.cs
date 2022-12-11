using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Base;

namespace Calabonga.EntityProcessor.Results;

public class ExecutionSuccessResult<TEntity> : ExecutionResultBase<TEntity>
{
    public ExecutionSuccessResult(TEntity entity, EntityActionResult result) : base(entity, result)
    {
    }
}