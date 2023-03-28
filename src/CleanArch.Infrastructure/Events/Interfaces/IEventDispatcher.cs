using CleanArch.Domain.Interfaces;

namespace CleanArch.Infrastructure.Events.Interfaces;

/// <summary>
/// Interface which defines functionality of a Domain Event dispatcher.
/// </summary>
public interface IEventDispatcher
{
    /// <summary>
    /// Dispatches Domain Event(s) registered to the provided entities.
    /// </summary>
    /// <param name="entitiesWithEvents">Entities with Domain Event(s) registered to them.</param>
    Task DispatchAndClearEvents(IEnumerable<IEntity> entitiesWithEvents);
}