using Ardalis.GuardClauses;
using CleanArch.Domain.Models;

namespace CleanArch.Domain.TodoListAggregate.ValueObjects;

/// <summary>
/// Value Object which is used as a strongly-typed Id type for the <see cref="TodoList"/> Aggregate.
/// </summary>
public sealed class TodoListId : ValueObject
{
    /// <summary>
    /// Internal primitive value.
    /// </summary>
    /// <value>The primitive value for the Id.</value>
    public Guid Value { get; }

    /// <summary>
    /// Constructor of the <see cref="TodoListId"/> Value Object.
    /// </summary>
    /// <param name="value">The value to store in this Value Object.</param>
    private TodoListId(Guid value)
    {
        Value = Guard.Against.NullOrEmpty(value);
    }

    /// <summary>
    /// Generates a new instance of the <see cref="TodoListId"/> Value Object.
    /// </summary>
    /// <returns>New <see cref="TodoListId"/> instance.</returns>
    public static TodoListId CreateUnique()
    {
        return new TodoListId(Guid.NewGuid());
    }

    /// <summary>
    /// Generates a new instance of the <see cref="TodoListId"/> Value Object, with the
    /// given value as the internal primitive value.
    /// </summary>
    /// <param name="value">Primitive value to store in this Value Object.</param>
    /// <returns>New <see cref="TodoListId"/> instance.</returns>
    public static TodoListId Create(Guid value)
    {
        return new TodoListId(value);
    }

    /// <inheritdoc />
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}