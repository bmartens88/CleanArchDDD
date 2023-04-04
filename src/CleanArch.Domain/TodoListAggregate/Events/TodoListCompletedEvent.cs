using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate.Events;

/// <summary>
/// Event fired when a <see cref="TodoList"/> was marked as 'completed'.
/// </summary>
public sealed class TodoListCompletedEvent : DomainEvent
{
    /// <summary>
    /// The Id of the List which was completed.
    /// </summary>
    /// <value>The unique identifier of the completed List.</value>
    public TodoListId TodoListId { get; }

    /// <summary>
    /// Constructor of the <see cref="TodoListCompletedEvent"/> Event class.
    /// </summary>
    /// <param name="id">The unique identifier of the List for which this Event is fired.</param>
    public TodoListCompletedEvent(TodoListId id)
    {
        TodoListId = id;
    }
}
