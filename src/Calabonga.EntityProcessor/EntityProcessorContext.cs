﻿using Calabonga.EntityProcessor.Events;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Контекст для процесса выполнения действия над сущностью. 
/// </summary>
/// <remarks>
/// Используется для передачи между правилами и действиями, которые выполняются в процессов при выполнение операции.
/// В контексте есть возможность сохранить события домена <see cref="IDomainEvent"/> для дальнейшей их обработки.
/// </remarks>
public class EntityProcessorContext : DomainEventCollectionBase
{
    private readonly Dictionary<string, object> _directory = new();

    public object this[string key]
    {
        get => _directory[key];
        set => _directory[key] = value;
    }
}