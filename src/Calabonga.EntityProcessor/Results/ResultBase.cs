using Calabonga.EntityProcessor.Actions;
using Calabonga.EntityProcessor.Rules;

namespace Calabonga.EntityProcessor.Results;

/// <summary>
/// Базовый класс результата обработки правил <see cref="IRule{TEntity}"/> и/или действий <see cref="ActionBase{TEntity}"/>,
/// который поддерживает возможность регистрировать события в конвейере обработки действий.
/// </summary>
/// <remarks>
/// При обработке правил <see cref="IRule{TEntity}"/> и/или действий <see cref="ActionBase{TEntity}"/> может возникнуть потребность
/// запустить какое-нибудь события домена. Этот базовый класс имеет возможность зарегистрировать события домена для дальнейшей обработки или, непосредственно, их запуска.
/// <see cref="DomainEventCollectionBase"/>
/// </remarks>
public abstract class ResultBase : DomainEventCollectionBase { }