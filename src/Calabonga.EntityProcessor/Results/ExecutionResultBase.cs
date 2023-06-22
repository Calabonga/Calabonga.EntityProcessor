using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Rules;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Базовый класс. Результат исполнения команды в процессоре
/// </summary>
/// <typeparam name="TEntity">сущность для которой выполняется действие</typeparam>
public abstract class ExecutionResultBase<TEntity>
{
    protected ExecutionResultBase(TEntity? entity, EntityActionResult<TEntity> result)
    {
        Entity = entity;
        Result = result;
    }

    /// <summary>
    /// Результат выполнения действия <see cref="IAction{TEntity}"/>
    /// </summary>
    public EntityActionResult<TEntity>? Result { get; }

    /// <summary>
    /// Ошибки, возникшие в процессе выполнения валидации.
    /// </summary>
    /// <remarks>
    /// Правила валидации сущности <seealso cref="IRule{TEntity}"/>
    /// </remarks>
    public IEnumerable<ValidationResult>? Validations { get; private set; }

    /// <summary>
    /// Возвращает True/False, в зависимости от того, есть результат применения действия и экземпляр сущности, на котором было произведено действие
    /// </summary>
    public bool Ok => Result is not null && Entity is not null;

    /// <summary>
    /// Экземпляр сущности, на котором было произведено действие
    /// </summary>
    public TEntity? Entity { get; }

    /// <summary>
    /// Установка результатов валидации
    /// </summary>
    /// <param name="validationResults"></param>
    protected void SetValidationResults(IEnumerable<ValidationResult> validationResults)
        => Validations = validationResults;
}