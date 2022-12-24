namespace Calabonga.EntityProcessor;

public class EntityProcessorConfiguration<TEntity>: IEntityProcessorConfiguration
{
    public bool SkipRuleDuplicates { get; set; }

    public bool AutoFireDomainEvents { get; set; }

    public string EntityName => typeof(TEntity).Name;
}