using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Base;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Rules;

public class CheckCreatedInThePastOrderRule : RuleBase<Order>
{
    public CheckCreatedInThePastOrderRule()
    {

    }
    public override async Task<IRuleResult> ValidateAsync(Order entity, EntityProcessorContext context)
    {
        var isValid = entity.CreatedAt.Date <= DateTime.UtcNow.Date;
        await Task.Delay(900);


        return isValid ? new SuccessRuleResult() : new ErrorRuleResult(ErrorMessage!);
    }

    public override string? ErrorMessage => "Дата создания документа в будущем";
    public override string? SuccessMessage => "Проверка даты создания успешно завершена";
}