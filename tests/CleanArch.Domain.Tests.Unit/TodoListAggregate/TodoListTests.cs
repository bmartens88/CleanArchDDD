using CleanArch.Domain.Models;
using CleanArch.Domain.Tests.Unit.Shared;
using CleanArch.Domain.TodoListAggregate;
using CleanArch.Domain.TodoListAggregate.Entities;
using CleanArch.Domain.TodoListAggregate.Events;
using CleanArch.Domain.TodoListAggregate.ValueObjects;
using FluentAssertions;

namespace CleanArch.Domain.Tests.Unit.TodoListAggregate;

public sealed class TodoListTests
{
    [Theory]
    [InlineData("test", null, "description")]
    [InlineData(null, "test", "name")]
    public void Create_ShouldThrowException_WhenGivenAnInvalidParameterValue(
        string name,
        string description,
        string paramName)
    {
        // Act
        var act = () => TodoList.Create(name, description);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(paramName)
            .WithMessage("Value cannot be null.*");
    }

    [Theory]
    [InlineData(Constants.TO_LONG_PARAM_VALUE, "test", "name")]
    [InlineData("test", Constants.TO_LONG_PARAM_VALUE, "description")]
    public void Create_ShouldThrowException_WhenGivenAParameterWithLengthGreaterThan100(
        string name,
        string description,
        string paramName)
    {
        // Act
        var act = () => TodoList.Create(name, description);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(paramName)
            .WithMessage("Should not exceed length of 100*");
    }

    [Fact]
    public void Create_ShouldCreateANewTodoList_WhenGivenValidParameterValues()
    {
        // Arrange
        const string name = "Test Todo List";
        const string description = "Test Todo List description";
        const string itemName = "Name for todo item";
        const string itemDescription = "Description for todo item";
        var item = CreateTodoItem(itemName, itemDescription);
        var items = new List<TodoItem> { item };

        // Act
        var sut = CreateTodoListInstance(name, description, items);

        // Assert
        sut.Should().NotBeNull();
        sut.Name.Should().Be(name);
        sut.Description.Should().Be(description);
        sut.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        sut.Completed.Should().Be(false);
        sut.Items.Should().NotBeEmpty();
        sut.Items.Should().BeEquivalentTo(items);
    }

    [Theory]
    [InlineData(null, "name")]
    [InlineData(Constants.TO_LONG_PARAM_VALUE, "name")]
    [InlineData("", "name")]
    public void SetName_ShouldThrowException_WhenGivenInvalidParameterValue(
        string name,
        string paramName)
    {
        // Arrange
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description");

        // Act
        var act = () => sut.SetName(name);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(paramName);
    }

    [Theory]
    [InlineData(null, "description")]
    [InlineData(Constants.TO_LONG_PARAM_VALUE, "description")]
    [InlineData("", "description")]
    public void SetDescription_ShouldThrowException_WhenGivenInvalidParameterValue(
        string description,
        string paramName)
    {
        // Arrange
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description");

        // Act
        var act = () => sut.SetDescription(description);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(paramName);
    }

    [Fact]
    public void SetName_ShouldUpdateNameProperty_WhenGivenValidParameterValue()
    {
        // Arrange
        var name = "Updated Todo List Name";
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description");

        // Act
        sut.SetName(name);

        // Assert
        sut.Name.Should().Be(name);
    }

    [Fact]
    public void SetDescription_ShouldUpdateDescriptionProperty_WhenGivenValidParameterValue()
    {
        // Arrange
        var description = "Updated Todo List Description";
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description");

        // Act
        sut.SetDescription(description);

        // Assert
        sut.Description.Should().Be(description);
    }

    [Fact]
    public void MarkItemAsCompleted_ShouldUpdatePropertiesProperly_WhenItemWasNotYetCompleted()
    {
        // Arrange
        var itemId = TodoItemId.CreateUnique();
        var item = TodoItem.Create("Todo Item #1", "Todo Item Description", itemId);
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description", new List<TodoItem> { item });

        // Act
        var result = sut.MarkItemAsCompleted(itemId);

        // Assert
        sut.Completed.Should().BeTrue();
        sut.DateCompleted.Should().NotBeNull();
        sut.DateCompleted.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task MarkItemAsCompleted_ShouldNotUpdateProperties_WhenItemWasAlreadyCompleted()
    {
        // Arrange
        var itemId = TodoItemId.CreateUnique();
        var item = CreateTodoItem("Todo Item #1", "Todo Item Description", itemId);
        var sut = CreateTodoListInstance("Todo List Name", "Todo List Description", new List<TodoItem> { item });
        sut.MarkItemAsCompleted(itemId);

        // Act
        await Task.Delay(TimeSpan.FromSeconds(2));
        var result = sut.MarkItemAsCompleted(itemId);

        // Assert
        sut.DateCompleted.Should()
            .NotBeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenGivenTodoListsWithSameIdValue()
    {
        // Arrange
        var id = TodoListId.CreateUnique();
        var list1 = CreateTodoListInstance("Todo List #1 Name", "Todo List #1 Description", id: id);
        var list2 = CreateTodoListInstance("Todo List #2 Name", "Todo List #2 Description", id: id);

        // Act
        var equals = list1 == list2;

        // Assert
        equals.Should()
            .BeTrue();
    }

    [Fact]
    public void TodoList_ShouldRegisterDomainEvent_WhenTheListIsCompleted()
    {
        // Arrange
        var itemId = TodoItemId.CreateUnique();
        var listId = TodoListId.CreateUnique();
        var sut = TodoList.Create("Test", "Test", listId, new() { TodoItem.Create("Test", "Test", itemId) });

        // Act
        sut.MarkItemAsCompleted(itemId);

        // Assert
        sut.DomainEvents.Should().ContainSingle();
        sut.DomainEvents.Should().ContainItemsAssignableTo<DomainEvent>();
        sut.DomainEvents.Should().AllBeOfType<TodoListCompletedEvent>();
        sut.DomainEvents.Should().SatisfyRespectively(
            first =>
            {
                first.As<TodoListCompletedEvent>().TodoListId.Should()
                    .Be(listId);
            });
    }

    [Fact]
    public void MarkAsCompleted_ShouldNotRegisterDomainEvent_WhenListWasAlreadyCompleted()
    {
        // Arrange
        var itemId = TodoItemId.CreateUnique();
        var sut = TodoList.Create("Test", "Test", items: new() { TodoItem.Create("Test", "Test", itemId) });

        // Act
        sut.MarkItemAsCompleted(itemId);
        sut.MarkItemAsCompleted(itemId);

        // Assert
        sut.DomainEvents.Should().ContainSingle();
    }

    private TodoList CreateTodoListInstance(
        string todoListName,
        string todoListDescription,
        List<TodoItem>? items = null,
        TodoListId? id = null)
    {
        var todoList = TodoList.Create(todoListName, todoListDescription, id, items);

        return todoList;
    }

    private TodoItem CreateTodoItem(
        string todoItemName,
        string todoItemDescription,
        TodoItemId? id = null)
    {
        return TodoItem.Create(
            todoItemName,
            todoItemDescription,
            id);
    }
}
