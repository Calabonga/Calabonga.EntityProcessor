namespace Calabonga.EntityProcessor.Rules;

public abstract class RuleBase<TEntity> : IRule<TEntity> where TEntity : class
{
    public abstract Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    public virtual string ErrorMessage => $"{GetType().Name} validated with error";

    public virtual string SuccessMessage => $"{GetType().Name} validated with success";

    public virtual string GroupName => "Default";
    public string? Name { get; }
}