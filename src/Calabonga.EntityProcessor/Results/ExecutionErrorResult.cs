using Calabonga.EntityProcessor.Base;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor.Results;

public class ExecutionErrorResult<TEntity> : ExecutionResultBase<TEntity>
{
    public ExecutionErrorResult(TEntity? entity, List<ValidationResult> validationResults) : base(entity, null) 
        => SetValidationResults(validationResults);
}