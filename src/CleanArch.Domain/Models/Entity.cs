using System.ComponentModel.DataAnnotations.Schema;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Domain.Models;

/// <summary>
/// Base class for an Entity in the system.
/// Entities can have Domain Event(s) registered to them, and they are considered equal when the Id property is equal.
/// </summary>
/// <typeparam name="TId">The type of the strongly-typed Id property.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity
    where TId : notnull
{
    /// <summary>
    /// Private 'backing field' for registering domain event(s).
    /// </summary>
    private readonly List<DomainEvent> _domainEvents = new();

    /// <inheritdoc />
    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// The Id of this Entity instance.
    /// </summary>
    public TId Id { get; }

    /// <summary>
    /// Constructor of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The Id of type <typeparamref name="TId"/> to set for this Entity instance.</param>
    /// <typeparam name="TId">The type of the strongly-typed Id.</typeparam>
    protected Entity(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    /// <summary>
    /// Empty constructor for EF Core.
    /// </summary>
    protected Entity()
    {
    }
#pragma warning restore CS8618

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Equality operator to compare two instances of <see cref="Entity{TId}"/>.
    /// </summary>
    /// <param name="left"><see cref="Entity{TId}"/> instance.</param>
    /// <param name="right"><see cref="Entity{TId}"/> instance.</param>
    /// <returns>True if the two instances are equal, false otherwise.</returns>
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Not-equality operator to compare two instances of <see cref="Entity{TId}"/>.
    /// </summary>
    /// <param name="left"><see cref="Entity{TId}"/> instance.</param>
    /// <param name="right"><see cref="Entity{TId}"/> instance.</param>
    /// <returns>True if the two instances are not equal, false otherwise.</returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Register a new domain event to this Entity instance.
    /// </summary>
    /// <param name="domainEvent"><seealso cref="DomainEvent"/> instance to register.</param>
    protected void RegisterDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <inheritdoc />
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}