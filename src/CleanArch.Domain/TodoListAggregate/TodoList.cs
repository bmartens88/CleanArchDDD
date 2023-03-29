using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate;

public sealed class TodoList : AggregateRoot<TodoListId>
{
}