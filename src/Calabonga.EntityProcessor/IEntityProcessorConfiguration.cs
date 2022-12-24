using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;
using MediatR;

namespace Calabonga.EntityProcessor;

public interface IEntityProcessorConfiguration
{
    /// <summary>
    /// Название сущности, для которой создан <see cref="EntityProcessor{TEntity}"/>
    /// </summary>
    string EntityName { get; }

    /// <summary>
    /// Если включено, то обрабатывается каждое и дублирующих правил. Сообщение об ошибке не выдается.  
    /// </summary>
    bool SkipRuleDuplicates { get; }

    /// <summary>
    /// Если включено, то полученные при обработке команды <see cref="IAction{TEntity}"/> события
    /// домена <see cref="IDomainEvent"/> будут автоматически запущены через посредника <see cref="IMediator"/> 
    /// </summary>
    bool AutoFireDomainEvents { get; }
}