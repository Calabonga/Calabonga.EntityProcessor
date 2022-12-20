namespace Calabonga.EntityProcessor.Exceptions;

/// <summary>
/// Исключение, которое выкидывает процессор <see cref="EntityProcessorBase{TEntity}"/>, когда операция не может
/// быть выполнено. 
/// </summary>
/// <remarks>
/// Исключение могут быть при проверке правил или при применение действия, а также если не было найдено зарегистрированных в DI-контейнере, например. 
/// </remarks>
public class EntityProcessorInvalidOperationException : Exception
{
    /// <summary>
    /// Создает экземпляр класса исключения.
    /// </summary>
    /// <param name="message">сообщение об ошибке</param>
    public EntityProcessorInvalidOperationException(string? message) : base(message)
    {

    }
}