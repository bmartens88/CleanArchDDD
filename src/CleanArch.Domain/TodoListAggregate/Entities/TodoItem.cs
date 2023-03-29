using Ardalis.GuardClauses;
using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate.Entities;

/// <summary>
/// Entity which resembles an item of a <see cref="TodoList"/> Aggregate instance.
/// </summary>
public sealed class TodoItem : Entity<TodoItemId>
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime DateCreated { get; }

    public DateTime? DateCompleted { get; private set; }

    public bool Completed { get; private set; } = false;

    private TodoItem(
        TodoItemId id,
        string name,
        string description)
        : base(id)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.NullOrEmpty(description);

        Name = name;
        Description = description;
        DateCreated = DateTime.UtcNow;
    }

    public static TodoItem Create(
        string name,
        string description)
    {
        return new TodoItem(
            TodoItemId.CreateUniquie(),
            name,
            description);
    }

#pragma warning disable CS8618
    /// <summary>
    /// Empty constructor for EF Core.
    /// </summary>
    private TodoItem()
    {
    }
#pragma warning restore CS8618
}