using Calabonga.EntityProcessor.Base;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Результат проверки правила, который завершился с ошибкой
/// </summary>
public class ErrorRuleResult : RuleResultBase
{
    public ErrorRuleResult(string message) : base(false)
        => ErrorMessage = message ?? throw new ArgumentNullException(nameof(message));

    /// <inheritdoc />
    public override string? ErrorMessage { get; }
}