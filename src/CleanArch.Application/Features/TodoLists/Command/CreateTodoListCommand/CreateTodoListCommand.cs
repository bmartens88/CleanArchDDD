using CleanArch.Domain.TodoListAggregate;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Command.CreateTodoListCommand;

public sealed record class CreateTodoListCommand(
    string Name,
    string Description,
    List<CreateTodoItem>? items = null) : IRequest<TodoList>;

public sealed record class CreateTodoItem(
    string Name,
    string Description);
