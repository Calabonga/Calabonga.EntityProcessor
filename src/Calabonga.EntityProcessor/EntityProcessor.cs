using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Events;
using Calabonga.EntityProcessor.Exceptions;
using Calabonga.EntityProcessor.Results;
using Calabonga.EntityProcessor.Rules;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Процессор с реализацией базовых механизмов обработки команд <see cref="IAction{TEntity}"/> по правилам <see cref="IRule{TEntity}"/> 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityProcessor<TEntity> : IEntityProcessor
    where TEntity : class
{
    private readonly EntityProcessorContext _context = new();
    private readonly EntityProcessorConfiguration<TEntity>? _configuration;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IEnumerable<IRule<TEntity>> _rules;

    /// <summary>
    /// Создает экземпляр процессора
    /// </summary>
    /// <param name="mediator">https://github.com/jbogard/MediatR</param>
    /// <param name="configuration"><see cref="EntityProcessorConfiguration{TEntity}"/></param>
    /// <param name="logger"></param>
    /// <param name="rules"></param>
    protected EntityProcessor(
        IMediator mediator,
        EntityProcessorConfiguration<TEntity>? configuration,
        ILogger logger,
        IEnumerable<IRule<TEntity>> rules)
    {
        _configuration = configuration;
        _mediator = mediator;
        _logger = logger;
        _rules = rules;
    }

    /// <summary>
    /// Запускает процесс, в котором происходит проверка правил <see cref="IRule{TEntity}"/> и применение действия <see cref="IAction{TEntity}"/>
    /// </summary>
    /// <param name="entity">сущность, над которой производится действие</param>
    /// <param name="actionToExecute">действие, которое будет выполнено если в результате проверки правил не было обнаружено ошибок</param>
    /// <param name="rules">дополнительный список правил, которые надо проверить перед применением действия</param>
    /// <param name="cancellationToken">ключ отмены асинхронной операции</param>
    /// <returns></returns>
    /// <exception cref="EntityProcessorInvalidOperationException">ошибка в процессе выполнения</exception>
    public async Task<ExecutionResultBase<TEntity>> ProcessAsync(TEntity entity, IAction<TEntity> actionToExecute, IEnumerable<IRule<TEntity>>? rules = null, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("[{EntityProcessor}]: Executing {Method}", GetType().Name, nameof(ProcessAsync));

        List<ValidationResult> errors = new();
        var allRules = _rules.Union(rules ?? Enumerable.Empty<IRule<TEntity>>()).ToList();

        if (_configuration?.SkipRuleDuplicates == false)
        {
            var grouped = allRules.GroupBy(x => x.GetType().Name).Select(x => new { Name = x.Key, Total = x.Count() });
            if (grouped.Any(x => x.Total > 1))
            {
                _logger.LogWarning("[{EntityProcessor}]: Attention! Some rules are duplicated", GetType().Name);
                throw new EntityProcessorInvalidOperationException("Duplicated are found, but not allowed");
            }
        }

        var isRulesRequired = allRules.Any();
        var isValid = !isRulesRequired;

        if (isRulesRequired)
        {
            _logger.LogDebug("[{EntityProcessor}]: found {Total} rules", GetType().Name, allRules.Count);
            foreach (var rule in allRules!)
            {
                _logger.LogDebug("[{EntityProcessor}]: Validating {Entity} with rule {RuleName}", GetType().Name, entity.GetType().Name, rule.GetType().Name);
                var check = await rule.ValidateAsync(entity, _context, cancellationToken);
                if (check.Ok)
                {
                    continue;
                }

                errors.Add(new ValidationResult(check.ErrorMessage));
            }

            isValid = !errors.Any();
        }

        if (isRulesRequired && !isValid)
        {
            _logger.LogDebug("[{EntityProcessor}]: Executed {Method}", GetType().Name, nameof(ProcessAsync));
            return new ExecutionErrorResult<TEntity>(entity, errors);
        }

        var result = await actionToExecute.ApplyAsync(entity, _context, cancellationToken);

        if (_configuration?.AutoFireDomainEvents == true)
        {
            _logger.LogDebug("[{EntityProcessor}]: Firing DomainEvents", GetType().Name);
            await FireDomainEventsAsync(result.DomainEvents, cancellationToken);
        }

        _logger.LogDebug("[{EntityProcessor}]: Executed {Method}", GetType().Name, nameof(ProcessAsync));
        return new ExecutionSuccessResult<TEntity>(entity, result);
    }

    private async Task FireDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            switch (domainEvent)
            {
                case IDomainCommand:
                    _logger.LogDebug("[Command fired]: {Name}", domainEvent.GetType().Name);
                    await _mediator.Send(domainEvent, cancellationToken);
                    break;

                case IDomainNotification:
                    _logger.LogDebug("[Notification fired]: {Name}", domainEvent.GetType().Name);
                    await _mediator.Publish(domainEvent, cancellationToken);
                    break;
            }
        }
    }

}