using MediatR;

namespace Calabonga.EntityProcessor.Events;

/// <summary>
/// Уведомление при обработке сущности в процессоре <see cref="EntityProcessor{TEntity}"/>
/// </summary>
/// <remarks>
///  Уведомления получат все подписчики. Команда <see cref="IDomainCommand"/> будет
/// обработана одним перехватчиком <see cref="IRequestHandler{TRequest,TResponse}"/>.
/// Уведомления является наследником INotification и выполняется через команду PUBLISH.  
/// </remarks>
public interface IDomainNotification : IDomainEvent, INotification { }