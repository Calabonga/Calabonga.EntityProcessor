using Calabonga.EntityProcessor.Rules;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Результат проверки правила, который завершился с ошибкой
/// </summary>
public class ErrorRuleResult : RuleResultBase
{
    public ErrorRuleResult(string message) : base(false)
        => ErrorMessage = message ?? throw new ArgumentNullException(nameof(message));

    /// <summary>
    /// Если была операция проверки правила завершилась фиаско содержит текст сообщения с ошибкой
    /// </summary>
    public override string? ErrorMessage { get; }
}