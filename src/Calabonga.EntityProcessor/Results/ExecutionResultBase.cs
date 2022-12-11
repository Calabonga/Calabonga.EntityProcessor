using Calabonga.EntityProcessor.Actions;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor.Results;

public abstract class ExecutionResultBase<TEntity>
{
    protected ExecutionResultBase(TEntity? entity, EntityActionResult? result)
    {
        Entity = entity;
        Result = result;
    }

    public EntityActionResult? Result { get; }

    public IEnumerable<ValidationResult>? Validations { get; private set; }

    public bool Ok => Result is not null && Entity is not null;

    public TEntity? Entity { get; }


    protected void SetValidationResults(IEnumerable<ValidationResult> validationResults)
        => Validations = validationResults;
}