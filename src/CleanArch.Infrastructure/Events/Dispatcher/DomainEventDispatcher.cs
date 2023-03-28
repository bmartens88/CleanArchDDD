using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Events.Interfaces;
using MediatR;

namespace CleanArch.Infrastructure.Events.Dispatcher;

/// <summary>
/// Implementation of the <see cref="IEventDispatcher"/> interface.
/// Responsible for dispatching registered Domain Event(s) to notification listener(s).
/// </summary>
internal sealed class DomainEventDispatcher : IEventDispatcher
{
    /// <summary>
    /// Publisher responsible for publishing the Domain Event(s).
    /// </summary>
    private readonly IPublisher _publisher;

    /// <summary>
    /// Constructor of the <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="publisher"><see cref="IPublisher"/> instance for event publishing.</param>
    /// <seealso cref="IPublisher"/>
    public DomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    /// <inheritdoc />
    public async Task DispatchAndClearEvents(IEnumerable<IEntity> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
                await _publisher.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}