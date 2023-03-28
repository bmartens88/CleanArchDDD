using CleanArch.Domain.Models;

namespace CleanArch.Domain.Interfaces;

/// <summary>
/// Interface to define an Entity and public methods on an Entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Get a list of all domain event(s) registered to the Entity instance.
    /// </summary>
    IReadOnlyList<DomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears all registered domain event(s) of this Entity instance.
    /// </summary>
    void ClearDomainEvents();
}