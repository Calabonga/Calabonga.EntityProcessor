namespace Calabonga.EntityProcessor.Rules;

public interface IRuleResult
{
    bool Ok { get; }
    
    string? ErrorMessage { get; }
}