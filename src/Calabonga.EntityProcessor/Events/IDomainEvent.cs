namespace Calabonga.EntityProcessor.Events;

/// <summary>
/// Событие домена <see cref="IDomainEvent"/> подразделяются на команды <see cref="IDomainCommand"/>
/// и уведомления <see cref="IDomainNotification"/>.
/// </summary>
/// <remarks>
/// Команды <see cref="IDomainCommand"/> имеет одного исполнителя. Уведомления <see cref="IDomainNotification"/> получат все подписчики. 
/// </remarks>
public interface IDomainEvent { }