using Calabonga.EntityProcessor.Rules;

namespace Calabonga.EntityProcessor.Base;

public abstract class RuleResultBase : IRuleResult
{
    protected RuleResultBase(bool ok) => Ok = ok;

    public bool Ok { get; }
    
    public abstract string? ErrorMessage { get; }
}