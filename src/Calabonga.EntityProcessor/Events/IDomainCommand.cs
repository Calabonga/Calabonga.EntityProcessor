using MediatR;

namespace Calabonga.EntityProcessor.Events;

/// <summary>
/// Команда для обработки сущности в процессоре <see cref="EntityProcessor{TEntity}"/>
/// </summary>
/// <remarks>
/// Команда имеет одного исполнителя. Уведомления <see cref="IDomainNotification"/> получат все подписчики.
/// Команда является наследником <see cref="IRequest"/> и выполняется через команду SEND <see cref="Mediator"/>  
/// </remarks>
public interface IDomainCommand : IDomainEvent, IRequest { }
