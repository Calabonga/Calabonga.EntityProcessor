using Calabonga.EntityProcessor.Actions;

namespace Calabonga.EntityProcessor.Rules;

public interface IRule<in TEntity> : IHaveName where TEntity : class
{
    Task<IRuleResult> ValidateAsync(TEntity entity, EntityProcessorContext context, CancellationToken cancellationToken);

    string? ErrorMessage { get; }

    string? SuccessMessage { get; }

    string GroupName { get; }
}