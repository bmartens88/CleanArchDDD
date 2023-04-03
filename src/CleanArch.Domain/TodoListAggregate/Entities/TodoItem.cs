using Ardalis.GuardClauses;
using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate.Entities;

/// <summary>
/// Entity which resembles an item of a <see cref="TodoList"/> Aggregate instance.
/// </summary>
public sealed class TodoItem : Entity<TodoItemId>
{
    /// <summary>
    /// The name of the item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// A short description of the item.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// When the item was created.
    /// </summary>
    public DateTime DateCreated { get; }

    /// <summary>
    /// The date when the item was completed (when completed), null otherwise.
    /// </summary>
    public DateTime? DateCompleted { get; private set; }

    /// <summary>
    /// Whether the item is completed or not.
    /// </summary>
    public bool Completed { get; private set; } = false;

    /// <summary>
    /// Constructor of the <see cref="TodoItem"/> class.
    /// </summary>
    /// <param name="id">Instance of <see cref="TodoItemId"/> value object used as strongly-typed Id.</param>
    /// <param name="name">The name to use for the new item.</param>
    /// <param name="description">A short description for the new item.</param>
    private TodoItem(
        TodoItemId id,
        string name,
        string description)
        : base(id)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.Length(name, 100);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.Length(description, 100);

        Name = name;
        Description = description;
        DateCreated = DateTime.UtcNow;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="TodoItem"/> class.
    /// </summary>
    /// <param name="name">The name to use for the new item.</param>
    /// <param name="description">A short description to use for the new item.</param>
    /// <param name="id">Optional id to set for the new item.</param>
    /// <returns>A new <see cref="TodoItem"/> instance.</returns>
    public static TodoItem Create(
        string name,
        string description,
        TodoItemId? id = null)
    {
        return new TodoItem(
            id ?? TodoItemId.CreateUnique(),
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

    /// <summary>
    /// Update the name of the item.
    /// </summary>
    /// <param name="name">The new name to set for the item.</param>
    public void SetName(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.Length(name, 100);

        Name = name;
    }

    /// <summary>
    /// Update the description of the item.
    /// </summary>
    /// <param name="description">The new description to set for the item.</param>
    public void SetDescription(string description)
    {
        Guard.Against.NullOrEmpty(description);
        Guard.Against.Length(description, 100);

        Description = description;
    }

    /// <summary>
    /// Marks the item as being completed.
    /// </summary>
    public void MarkItemAsCompleted()
    {
        if (Completed) return;
        Completed = true;
        DateCompleted = DateTime.UtcNow;
    }
}