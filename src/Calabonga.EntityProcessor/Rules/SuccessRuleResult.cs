namespace Calabonga.EntityProcessor.Rules;

/// <summary>
/// Успешный результат операции проверки правила <see cref="IRule{TEntity}"/> для сущности 
/// </summary>
public class SuccessRuleResult : RuleResultBase
{
    /// <summary>
    /// Создает экземпляр успешного результата проверки
    /// </summary>
    public SuccessRuleResult() : base(true) { }

    /// <summary>
    /// Если была операция проверки правила завершилась фиаско содержит текст сообщения с ошибкой
    /// </summary>
    public override string? ErrorMessage { get; } = default;
}