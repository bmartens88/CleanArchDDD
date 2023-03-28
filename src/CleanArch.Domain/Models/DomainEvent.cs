using MediatR;

namespace CleanArch.Domain.Models;

/// <summary>
/// Base class for a Domain Event.
/// </summary>
public abstract class DomainEvent : INotification
{
    /// <summary>
    /// The date and time at which this event occurred.
    /// </summary>
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}