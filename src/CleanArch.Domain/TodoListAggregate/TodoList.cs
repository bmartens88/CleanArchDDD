using Ardalis.GuardClauses;
using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.Entities;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate;

public sealed class TodoList : AggregateRoot<TodoListId>
{
    private List<TodoItem> _items = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime DateCreated { get; }

    public DateTime? DateCompleted { get; private set; }

    public bool Completed { get; private set; } = false;

    public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();

    private TodoList(
        TodoListId id,
        string name,
        string description,
        List<TodoItem> items)
        : base(id)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.Length(name, 100);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.Length(description, 100);

        Name = name;
        Description = description;
        _items = items;
        DateCreated = DateTime.UtcNow;
    }

    public static TodoList Create(
        string name,
        string description,
        TodoListId? id = null,
        List<TodoItem>? items = null)
    {
        return new TodoList(
            id ?? TodoListId.CreateUnique(),
            name,
            description,
            items ?? new());
    }

#pragma warning disable CS8618
    /// <summary>
    /// Empty constructor for EF Core.
    /// </summary>
    private TodoList()
    { }
#pragma warning restore CS8618

    public void SetName(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.Length(name, 100);

        Name = name;
    }

    public void SetDescription(string description)
    {
        Guard.Against.NullOrEmpty(description);
        Guard.Against.Length(description, 100);

        Description = description;
    }
}