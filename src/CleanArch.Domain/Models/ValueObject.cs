namespace CleanArch.Domain.Models;

/// <summary>
/// Base class for a Value Object in the system.
/// A Value Object is an object which only has meaning when multiple properties have a certain value.
/// A Value Object should be immutable. A change in properties must result in a new instance of the Value Object.
/// A Value Object is considered equal with another Value Object when all properties match.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Abstract property which must be implemented by derived classes.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> with property value(s) for equality comparison.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var valueObj = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(valueObj.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Equality operator to compare two instances of <see cref="ValueObject"/>.
    /// </summary>
    /// <param name="left"><see cref="ValueObject"/> instance.</param>
    /// <param name="right"><see cref="ValueObject"/> instance.</param>
    /// <returns>True if the two instances are equal, false otherwise.</returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Not-equality operator to compare two instance of <see cref="ValueObject"/>.
    /// </summary>
    /// <param name="left"><see cref="ValueObject"/> instance.</param>
    /// <param name="right"><see cref="ValueObject"/> instance.</param>
    /// <returns>True if the two instances are not equal, false otherwise.</returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }
}