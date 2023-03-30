using CleanArch.Domain.TodoListAggregate.ValueObjects;
using FluentAssertions;

namespace CleanArch.Domain.Tests.Unit.TodoListAggregate.ValueObjects;

public sealed class TodoListIdTests
{
    [Fact]
    public void Create_ShouldThrowException_WhenGivenAnInvalidValue()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var act = () => TodoListId.Create(id);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(TodoListId.Value).ToLower())
            .WithMessage($"Required input {nameof(TodoListId.Value).ToLower()} was empty.*");
    }

    [Fact]
    public void Create_ShouldNotThrowException_WhenGivenAValidValue()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var act = () => TodoListId.Create(id);

        // Assert
        act.Should()
            .NotThrow<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldCreateTodoListIdWithGivenId_WhenGivenIdIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var sut = TodoListId.Create(id);

        // Assert
        sut.Should().NotBeNull();
        sut.Value.Should().Be(id);
    }

    [Fact]
    public void CreateUnique_ShouldCreateTodoListId_WhenCreateUniqueIsCalled()
    {
        // Act
        var sut = TodoListId.CreateUnique();

        // Assert
        sut.Should().NotBeNull();
        sut.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenGivenTodoListIdsWithSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut1 = TodoListId.Create(id);
        var sut2 = TodoListId.Create(id);

        // Act
        var equals = sut1 == sut2;

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void NotEquals_ShouldReturnTrue_WhenGivenTodoListIdsWithDifferentIds()
    {
        // Arrange
        var sut1 = TodoListId.CreateUnique();
        var sut2 = TodoListId.CreateUnique();

        // Act
        var result = sut1 != sut2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenGivenNull()
    {
        // Arrange
        var sut = TodoListId.CreateUnique();

        // Act
        var result = sut.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenGivenTodoListIdsWithSameValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut1 = TodoListId.Create(id);
        var sut2 = TodoListId.Create(id);

        // Act
        var hash1 = sut1.GetHashCode();
        var hash2 = sut2.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }
}