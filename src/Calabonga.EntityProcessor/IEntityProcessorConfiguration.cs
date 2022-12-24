namespace Calabonga.EntityProcessor;

public interface IEntityProcessorConfiguration
{
    string EntityName { get; }
    
    bool SkipRuleDuplicates { get; }
    
    bool AutoFireDomainEvents { get; }
}