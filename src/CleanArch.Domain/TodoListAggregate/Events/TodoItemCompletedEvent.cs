using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate.Events;

/// <summary>
/// Event fired when a <see cref="TodoItem"/> was marked as 'completed'.
/// </summary>
public sealed class TodoItemCompletedEvent : DomainEvent
{
    /// <summary>
    /// The Id of the Item which was completed.
    /// </summary>
    /// <value>The unique identifier of the completed Item.</value>
    public TodoItemId TodoItemId { get; }

    /// <summary>
    /// Constructor of the <see cref="TodoItemCompletedEvent"/> Event class.
    /// </summary>
    /// <param name="itemId">The unique identifier of the Item for which this Event is fired.</param>
    public TodoItemCompletedEvent(TodoItemId itemId)
    {
        TodoItemId = itemId;
    }
}
