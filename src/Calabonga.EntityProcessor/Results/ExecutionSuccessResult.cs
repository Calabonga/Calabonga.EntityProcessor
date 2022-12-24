using Calabonga.EntityProcessor.Actions;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Успешный результат выполнения операции применения действия <see cref="IAction{TEntity}"/> над сущностью.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class ExecutionSuccessResult<TEntity> : ExecutionResultBase<TEntity>
{
    public ExecutionSuccessResult(TEntity entity, EntityActionResult result) : base(entity, result)
    {
    }
}