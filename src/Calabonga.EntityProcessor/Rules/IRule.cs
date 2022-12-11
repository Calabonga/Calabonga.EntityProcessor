namespace Calabonga.EntityProcessor.Rules;

public interface IRule<in TEntity> where TEntity: class
{
    Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context);

    string? ErrorMessage { get; }
    
    string? SuccessMessage { get; }
}