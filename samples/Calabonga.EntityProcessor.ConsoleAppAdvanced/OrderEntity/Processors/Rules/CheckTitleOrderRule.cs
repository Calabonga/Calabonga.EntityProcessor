using Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Events;
using Calabonga.EntityProcessor;
using Calabonga.EntityProcessor.Base;
using Calabonga.EntityProcessor.Rules;
using Calabonga.Shared.OrderEntity;

namespace Calabonga.ConsoleAppAdvanced.OrderEntity.Processors.Rules;

public class CheckTitleOrderRule : RuleBase<Order>
{
    public override async Task<IRuleResult> ValidateAsync(Order entity, EntityProcessorContext context)
    {
        var isValid = !string.IsNullOrEmpty(entity.Title);

        await Task.Delay(990);

        // send notification to editor
        context.AddDomainEvent(new OrderTitleVerifiedNotification(entity.Id, isValid));

        return isValid ? new SuccessRuleResult() : new ErrorRuleResult(ErrorMessage!);
    }

    public override string? ErrorMessage => "Отсутствует наименование документа";
    public override string? SuccessMessage => "Проверка наименования успешно завершена";
}