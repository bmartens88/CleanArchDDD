using CleanArch.Domain.Interfaces;

namespace CleanArch.Domain.Models;

/// <summary>
/// Base class for an Aggregate Root in the system.
/// An Aggregate Root is the root Entity from which all operations on an Entity (and its child Entities) should occur.
/// It is responsible for keeping itself in a consistent and valid state, and is considered equal to another Aggregate
/// Root when the Id property is equal.
/// </summary>
/// <typeparam name="TId">Type of the strongly-typed Id property.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : notnull
{
    /// <summary>
    /// Constructor of the <see cref="AggregateRoot{TId}"/> class.
    /// </summary>
    /// <param name="id">The Id to set for this Aggregate Root instance.</param>
    /// <typeparam name="TId">Type of the strongly-typed Id property.</typeparam>
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

#pragma warning disable CS8618
    /// <summary>
    /// Empty constructor for EF Core.
    /// </summary>
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
}