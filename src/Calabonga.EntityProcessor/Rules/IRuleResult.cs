namespace Calabonga.EntityProcessor.Rules;

/// <summary>
/// Результат обработки правила <see cref="IRule{TEntity}"/>.
/// </summary>
public interface IRuleResult
{
    /// <summary>
    /// Возвращает да/нет свидетельствую об успехе (или фиаско) выполнения проверки конкретного правила
    /// </summary>
    bool Ok { get; }

    /// <summary>
    /// Если была операция проверки правила завершилась фиаско содержит текст сообщения с ошибкой
    /// </summary>
    string? ErrorMessage { get; }
}