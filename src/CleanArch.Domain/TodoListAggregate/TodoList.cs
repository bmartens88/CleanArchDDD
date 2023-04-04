using Ardalis.GuardClauses;
using CleanArch.Domain.Models;
using CleanArch.Domain.TodoListAggregate.Entities;
using CleanArch.Domain.TodoListAggregate.Events;
using CleanArch.Domain.TodoListAggregate.ValueObjects;

namespace CleanArch.Domain.TodoListAggregate;

public sealed class TodoList : AggregateRoot<TodoListId>
{
    /// <summary>
    /// Internal list of <see cref="TodoItem"/> entities belonging to this list.
    /// </summary>
    private List<TodoItem> _items = new();

    /// <summary>
    /// The name of the List.
    /// </summary>
    /// <value>The name as given to this Todo List.</value>
    public string Name { get; private set; }

    /// <summary>
    /// The Description of the List.
    /// </summary>
    /// <value>The description as given to this Todo List.</value>
    public string Description { get; private set; }

    /// <summary>
    /// Date on which this List was created.
    /// </summary>
    /// <value>Date of creation.</value>
    public DateTime DateCreated { get; }

    /// <summary>
    /// Date on which this List was marked as 'completed'.
    /// </summary>
    /// <value>Date of completion.</value>
    public DateTime? DateCompleted { get; private set; }

    /// <summary>
    /// Indicates whether this Todo List is completed.
    /// </summary>
    /// <value>Whether the List is completed.</value>
    public bool Completed { get; private set; } = false;

    /// <summary>
    /// Property through which the items can be accessed.
    /// </summary>
    public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Constructor of the <see cref="TodoList"/> Entity class.
    /// </summary>
    /// <param name="id">The id of this Entity class instance.</param>
    /// <param name="name">The name to give to the List instance.</param>
    /// <param name="description">The description to give to the List instance.</param>
    /// <param name="items">The items to add to the (internal) list.</param>
    private TodoList(
        TodoListId id,
        string name,
        string description,
        List<TodoItem> items)
        : base(id)
    {
        Guard.Against.NullOrEmpty(name);
        Name = Guard.Against.Length(name, 100);
        Guard.Against.NullOrEmpty(description);
        Description = Guard.Against.Length(description, 100);
        _items = Guard.Against.Null(items);
        DateCreated = DateTime.UtcNow;
    }

    /// <summary>
    /// Returns a new instance of the <see cref="TodoList"/> Entity class.
    /// </summary>
    /// <param name="name">The name to give to the List instance.</param>
    /// <param name="description">The description to give to the List instance.</param>
    /// <param name="id">The id to give to the new instance (if any).</param>
    /// <param name="items">The items to add to the (internal) list.</param>
    /// <returns>A new instance of the <see cref="TodoList"/> Entity class.</returns>
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

    /// <summary>
    /// Update the name of the List.
    /// </summary>
    /// <param name="name">The new name to set.</param>
    public void SetName(string name)
    {
        Guard.Against.NullOrEmpty(name);
        Name = Guard.Against.Length(name, 100);
    }

    /// <summary>
    /// Update the description of the List.
    /// </summary>
    /// <param name="description">The new description to set.</param>
    public void SetDescription(string description)
    {
        Guard.Against.NullOrEmpty(description);
        Description = Guard.Against.Length(description, 100);
    }

    /// <summary>
    /// Mark an item on the List as completed.
    /// </summary>
    /// <param name="id">The id of the item to mark as completed.</param>
    public bool MarkItemAsCompleted(TodoItemId id)
    {
        var item = _items.FirstOrDefault(item => item.Id == id);
        if (item is null) return false;
        item.MarkItemAsCompleted();
        SetListCompletionStatus();
        return true;
    }

    /// <summary>
    /// Sets the 'completed' status of the List.
    /// </summary>
    private void SetListCompletionStatus()
    {
        if (Completed) return;
        if (_items.All(item => item.Completed))
        {
            Completed = true;
            DateCompleted = DateTime.UtcNow;
            RegisterDomainEvent(new TodoListCompletedEvent(Id));
        }
    }
}