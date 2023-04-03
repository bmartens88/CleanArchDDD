using CleanArch.Domain.TodoListAggregate.Entities;
using CleanArch.Domain.TodoListAggregate.ValueObjects;
using FluentAssertions;
using CleanArch.Domain.Tests.Unit.Shared;

namespace CleanArch.Domain.Tests.Unit.TodoListAggregate.Entities;

public sealed class TodoItemTests
{
    [Theory]
    [InlineData("test", null, "description")]
    [InlineData(null, "test", "name")]
    public void Create_ShouldThrowException_WhenGivenAnInvalidParameterValue(string name, string description,
        string paramName)
    {
        // Act
        var act = () => TodoItem.Create(name, description);

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
        var act = () => TodoItem.Create(name, description);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(paramName)
            .WithMessage("Should not exceed length of 100*");
    }

    [Fact]
    public void Create_ShouldCreateANewTodoItem_WhenGivenValidParameterValues()
    {
        // Arrange
        const string name = "Test Name";
        const string description = "Test Description";

        // Act
        var sut = TodoItem.Create(name, description);

        // Assert
        sut.Should().NotBeNull();
        sut.Name.Should().Be(name);
        sut.Description.Should().Be(description);
        sut.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        sut.DateCompleted.Should().BeNull();
    }

    [Fact]
    public void SetName_ShouldThrowException_WhenGivenInvalidParameterValue()
    {
        // Arrange
        var sut = TodoItem.Create("test", "test");

        // Act
#pragma warning disable CS8625
        var act = () => sut.SetName(null);
#pragma warning restore CS8625

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("name")
            .WithMessage("Value cannot be null.*");
    }

    [Fact]
    public void SetName_ShouldUpdateNameProperty_WhenGivenAValidParameterValue()
    {
        // Arrange
        const string name = "Updated";
        var sut = TodoItem.Create("test", "test");

        // Act
        sut.SetName(name);

        // Assert
        sut.Name.Should().Be(name);
    }

    [Fact]
    public void SetDescription_ShouldThrowException_WhenGivenInvalidParameterValue()
    {
        // Arrange
        var sut = TodoItem.Create("test", "test");

        // Act
#pragma warning disable CS8625
        var act = () => sut.SetDescription(null);
#pragma warning restore CS8625

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName("description")
            .WithMessage("Value cannot be null.*");
    }

    [Fact]
    public void SetDescription_ShouldUpdateDescriptionProperty_WhenGivenAValidParameterValue()
    {
        // Arrange
        const string description = "Updated";
        var sut = TodoItem.Create("test", "test");

        // Act
        sut.SetDescription(description);

        // Assert
        sut.Description.Should().Be(description);
    }

    [Fact]
    public void MarkItemAsCompleted_ShouldUpdatePropertiesProperly_WhenItemWasNotYetCompleted()
    {
        // Arrange
        var sut = TodoItem.Create("Test", "Test");

        // Act
        sut.MarkItemAsCompleted();

        // Assert
        sut.Completed.Should().BeTrue();
        sut.DateCompleted.Should().NotBeNull();
        sut.DateCompleted.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task MarkItemAsCompleted_ShouldNotUpdateProperties_WhenItemWasAlreadyCompleted()
    {
        // Arrange
        var sut = TodoItem.Create("Test", "Test");
        sut.MarkItemAsCompleted();

        // Act
        await Task.Delay(2000);
        sut.MarkItemAsCompleted();

        // Assert
        sut.DateCompleted.Should()
            .NotBeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenGivenTodoItemsWithSameIdValue()
    {
        // Arrange
        var id = TodoItemId.CreateUnique();
        var sut1 = TodoItem.Create("Test1", "Description1", id);
        var sut2 = TodoItem.Create("Test2", "Description2", id);

        // Act
        var equals = sut1 == sut2;

        // Assert
        equals.Should()
            .BeTrue();
    }
}