using Calabonga.EntityProcessor.Rules;

namespace Calabonga.EntityProcessor.Base;

public abstract class RuleBase<TEntity> : IRule<TEntity> where TEntity : class
{
    public abstract Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context);

    public virtual string ErrorMessage => $"{GetType().Name} validated with error";

    public virtual string SuccessMessage => $"{GetType().Name} validated with success";
}