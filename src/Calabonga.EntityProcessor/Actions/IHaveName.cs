namespace Calabonga.EntityProcessor.Actions;

/// <summary>
/// Интерфейс реализует свойство Name, для возможности фильтрации и поиска
/// </summary>
public interface IHaveName
{
    /// <summary>
    /// Наименование
    /// </summary>
    string? Name { get; }
}