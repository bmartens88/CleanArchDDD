using CleanArch.Domain.TodoListAggregate;
using MediatR;

namespace CleanArch.Application.Features.TodoLists.Query.GetTodoListsQuery;

public sealed record class GetTodoListsQuery() : IRequest<IReadOnlyList<TodoList>>;
