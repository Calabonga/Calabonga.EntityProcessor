using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Rules;

namespace Calabonga.EntityProcessor;

/// <summary>
/// Интерфейс процессора для обработки команд <see cref="IAction{TEntity}"/> по правилам <see cref="IRule{TEntity}"/> 
/// </summary>
public interface IEntityProcessor { }