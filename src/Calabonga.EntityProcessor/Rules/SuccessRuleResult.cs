using Calabonga.EntityProcessor.Base;

namespace Calabonga.EntityProcessor.Rules;

public class SuccessRuleResult : RuleResultBase
{
    public SuccessRuleResult() : base(true) { }

    public override string? ErrorMessage { get; } = default;
}