﻿using Calabonga.EntityProcessor.Results;

namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Результат применения действия <see cref="ActionBase{TEntity}"/> для конкретной сущности.
/// </summary>
public class EntityActionResult<T> : ResultBase
{
    public T? Entity { get; set; }
}