namespace Calabonga.EntityProcessor.Rules;

/// <summary>
/// Базовый класс для результата обработки правила <see cref="IRule{TEntity}"/>
/// </summary>
public abstract class RuleResultBase : IRuleResult
{
    /// <summary>
    /// Явно в конструкторе устанавливается да/нет операции
    /// </summary>
    /// <param name="ok"></param>
    protected RuleResultBase(bool ok) => Ok = ok;

    /// <summary>
    /// Возвращает да/нет свидетельствую об успехе (или фиаско) выполнения проверки конкретного правила
    /// </summary>
    public bool Ok { get; }

    /// <summary>
    /// Если была операция проверки правила завершилась фиаско содержит текст сообщения с ошибкой
    /// </summary>
    public abstract string? ErrorMessage { get; }
}