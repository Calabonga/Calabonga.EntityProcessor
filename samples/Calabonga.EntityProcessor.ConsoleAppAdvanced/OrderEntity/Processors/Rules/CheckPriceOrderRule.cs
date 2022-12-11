using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Base;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;
using Microsoft.Extensions.Logging;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Rules;

public class CheckPriceOrderRule : RuleBase<Order>
{
    private readonly ILogger<CheckPriceOrderRule> _logger;

    public CheckPriceOrderRule(ILogger<CheckPriceOrderRule> logger)
        => _logger = logger;

    public override async Task<IRuleResult> ValidateAsync(Order entity, EntityProcessorContext context)
    {
        await Task.Delay(320);

        _logger.LogInformation("Checking title");

        return entity.Price <= 0
            ? new ErrorRuleResult("Price")
            : new SuccessRuleResult();
    }
}