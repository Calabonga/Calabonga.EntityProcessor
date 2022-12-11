using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Base;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Rules;

public class CheckCreatedDateExistsOrderRule : RuleBase<Order>
{
    public override async Task<IRuleResult> ValidateAsync(Order entity, EntityProcessorContext context)
    {
        var isValid = entity.CreatedAt.Date != default;
        await Task.Delay(1100);
        return isValid ? new SuccessRuleResult() : new ErrorRuleResult(ErrorMessage!);
    }

    public override string ErrorMessage => "Дата создания документа не задана";

    public override string SuccessMessage => "Проверка даты создания успешно завершена";
}