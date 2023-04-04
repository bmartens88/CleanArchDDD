using CleanArch.Domain.TodoListAggregate.Events;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Events;

/// <summary>
/// Handler which handles Notifications of type <see cref="TodoListCompletedEvent"/>.
/// </summary>
internal sealed class TodoListCompletedEventHandler : INotificationHandler<TodoListCompletedEvent>
{
    /// <inheritdoc />
    public Task Handle(TodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received event for TodoList with Id: {notification.TodoListId.Value}");
        return Task.CompletedTask;
    }
}
