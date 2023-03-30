using CleanArch.Domain.TodoListAggregate.ValueObjects;
using FluentAssertions;

namespace CleanArch.Domain.Tests.Unit.TodoListAggregate.ValueObjects;

public sealed class TodoItemIdTests
{
    [Fact]
    public void Create_ShouldThrowException_WhenGivenAnInvalidValue()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var act = () => TodoItemId.Create(id);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(TodoItemId.Value).ToLower())
            .WithMessage($"Required input {nameof(TodoItemId.Value).ToLower()} was empty.*");
    }

    [Fact]
    public void Create_ShouldNotThrowException_WhenGivenAValidValue()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => TodoItemId.Create(id);

        // Assert
        act.Should()
            .NotThrow<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldCreateTodoItemId_WhenGivenIdIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var sut = TodoItemId.Create(id);

        // Assert
        sut.Should().NotBeNull();
        sut.Value.Should().Be(id);
    }

    [Fact]
    public void CreateUnique_ShouldCreateTodoItemId_WhenCreateUniqueIsCalled()
    {
        // Act
        var sut = TodoItemId.CreateUnique();

        // Assert
        sut.Should().NotBeNull();
        sut.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenGivenTodoItemIdsWithSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut1 = TodoItemId.Create(id);
        var sut2 = TodoItemId.Create(id);

        // Act
        var equals = sut1 == sut2;

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenGivenNull()
    {
        // Arrange
        var sut = TodoItemId.CreateUnique();

        // Act
        var equals = sut.Equals(null);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenGivenTodoItemIdsWithSameValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut1 = TodoItemId.Create(id);
        var sut2 = TodoItemId.Create(id);

        // Act
        var hash1 = sut1.GetHashCode();
        var hash2 = sut2.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }
}