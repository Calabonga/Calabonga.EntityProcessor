﻿using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Base;
using Calabonga.EntityProcessor.Events;
using Calabonga.EntityProcessor.Exceptions;
using Calabonga.EntityProcessor.Results;
using Calabonga.EntityProcessor.Rules;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.EntityProcessor;

public abstract class EntityProcessorBase<TEntity>
    where TEntity : class
{
    private readonly EntityProcessorContext _context = new();
    private readonly EntityProcessorConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IEnumerable<IRule<TEntity>> _rules;

    protected EntityProcessorBase(
        IMediator mediator,
        EntityProcessorConfiguration? configuration,
        ILogger logger,
        IEnumerable<IRule<TEntity>> rules)
    {
        _configuration = configuration ?? new EntityProcessorConfiguration();
        _mediator = mediator;
        _logger = logger;
        _rules = rules;
    }

    public async Task<ExecutionResultBase<TEntity>> ProcessAsync(TEntity entity, IAction<TEntity> actionToExecute, IEnumerable<IRule<TEntity>>? rules = null)
    {
        _logger.LogDebug("[{EntityProcessor}]: Executing {Method}", GetType().Name, nameof(ProcessAsync));

        List<ValidationResult> errors = new();
        var allRules = _rules.Union(rules ?? Enumerable.Empty<IRule<TEntity>>()).ToList();

        if (!_configuration.SkipRuleDuplicates)
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
                var check = await rule.ValidateAsync(entity, _context);
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

        var result = await actionToExecute.ApplyAsync(entity, _context);

        if (_configuration.AutoFireDomainEvents)
        {
            _logger.LogDebug("[{EntityProcessor}]: Firing DomainEvents", GetType().Name);
            await FireDomainEventsAsync(result.DomainEvents);
        }

        _logger.LogDebug("[{EntityProcessor}]: Executed {Method}", GetType().Name, nameof(ProcessAsync));
        return new ExecutionSuccessResult<TEntity>(entity, result);
    }

    private async Task FireDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            switch (domainEvent)
            {
                case IDomainCommand:
                    _logger.LogDebug("[Command fired]: {Name}", domainEvent.GetType().Name);
                    await _mediator.Send(domainEvent);
                    break;
                case IDomainNotification:
                    _logger.LogDebug("[Notification fired]: {Name}", domainEvent.GetType().Name);
                    await _mediator.Publish(domainEvent);
                    break;
            }
        }
    }

}