using Calabonga.EntityProcessor.Actions;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Результат выполнения команды <see cref="IAction{TEntity}"/>
/// </summary>
/// <typeparam name="TEntity">сущность для которой выполняется действие</typeparam>
public class ExecutionErrorResult<TEntity> : ExecutionResultBase<TEntity>
{
    /// <summary>
    /// Создает экземпляр результат выполнения
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="validationResults"></param>
    public ExecutionErrorResult(TEntity? entity, IEnumerable<ValidationResult> validationResults)
        : base(entity, null)
        => SetValidationResults(validationResults);
}