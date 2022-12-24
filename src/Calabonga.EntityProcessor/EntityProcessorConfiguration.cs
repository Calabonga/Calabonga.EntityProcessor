using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;
using MediatR;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Общая конфигурация для всех процессоров, зарегистрированных в системе
/// </summary>
public class EntityProcessorConfiguration<TEntity> : IEntityProcessorConfiguration
{
    /// <summary>
    /// Если включено, то обрабатывается каждое и дублирующих правил. Сообщение об ошибке не выдается.  
    /// </summary>
    public bool SkipRuleDuplicates { get; set; }

    /// <summary>
    /// Если включено, то полученные при обработке команды <see cref="IAction{TEntity}"/> события
    /// домена <see cref="IDomainEvent"/> будут автоматически запущены через посредника <see cref="IMediator"/> 
    /// </summary>
    public bool AutoFireDomainEvents { get; set; }


    public string EntityName => typeof(TEntity).Name;
}