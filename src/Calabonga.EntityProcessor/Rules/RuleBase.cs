namespace Calabonga.EntityProcessor.Rules;

/// <summary>
/// Базовый класс реализующий правило <see cref="IRule{TEntity}"/>.
/// В классе реализованы сообщения по умолчанию для выполнения правила с ошибкой и успешного выполнения.
/// Значение сообщений можно переопределить в своих правилах.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class RuleBase<TEntity> : IRule<TEntity> where TEntity : class
{
    /// <summary>
    /// Запускает процесс проверки (validation)
    /// </summary>
    /// <param name="entity">объект для проверки</param>
    /// <param name="context">контекст для передачи дополнительных данных</param>
    /// <param name="cancellationToken">маркет отмены</param>
    /// <returns></returns>
    public abstract Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    /// <summary>
    /// Сообщение, которое будет выдано на случай неудачи в процессе проверке
    /// </summary>
    public virtual string ErrorMessage => $"{GetType().Name} validated with error";

    /// <summary>
    /// Сообщение, которое будет выдано на случай успеха в процессе проверке
    /// </summary>
    public virtual string SuccessMessage => $"{GetType().Name} validated with success";

    /// <summary>
    /// Группа для правил проверки.
    /// </summary>
    /// <remarks>
    /// Правила можно группировать для применения для в разных сценариях. Например, для проверки сущности при создании
    /// могут быть применена одна группа проверок, а при изменении сущности - другая.
    /// </remarks>
    public virtual string GroupName => "Default";

    /// <summary>
    /// Наименование
    /// </summary>
    public abstract string? Name { get; }
}