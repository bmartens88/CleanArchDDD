using CleanArch.Domain.TodoListAggregate.Events;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Events;

/// <summary>
/// Handler which handles Notifications of type <see cref="TodoItemCompletedEvent"/>.
/// </summary>
internal sealed class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    /// <inheritdoc />
    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received event for TodoItem with Id: {notification.TodoItemId.Value}");
        return Task.CompletedTask;
    }
}
