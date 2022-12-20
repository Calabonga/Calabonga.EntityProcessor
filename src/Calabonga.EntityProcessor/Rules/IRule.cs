using Calabonga.EntityProcessor.Actions;

namespace Calabonga.EntityProcessor.Rules;

/// <summary>
/// Правило обработки (проверки) для конкретной сущности.
/// </summary>
/// <typeparam name="TEntity">сущность, для которой применяется правило</typeparam>
public interface IRule<in TEntity> : IHaveName where TEntity : class
{
    /// <summary>
    /// Запускает процесс проверки (validation)
    /// </summary>
    /// <param name="entity">объект для проверки</param>
    /// <param name="context">контекст для передачи дополнительных данных</param>
    /// <param name="cancellationToken">маркет отмены</param>
    /// <returns></returns>
    Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    /// <summary>
    /// Сообщение, которое будет выдано на случай неудачи в процессе проверке
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// Сообщение, которое будет выдано на случай успеха в процессе проверке
    /// </summary>
    string? SuccessMessage { get; }

    /// <summary>
    /// Группа для правил проверки.
    /// </summary>
    /// <remarks>
    /// Правила можно группировать для применения для в разных сценариях. Например, для проверки сущности при создании
    /// могут быть применена одна группа проверок, а при изменении сущности - другая.
    /// </remarks>
    string GroupName { get; }
}