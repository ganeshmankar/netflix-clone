using MediatR;

namespace NetflixClone.Domain.Events;

/// <summary>
/// Base class for all domain events
/// Domain events represent something that happened in the domain
/// </summary>
public abstract class BaseDomainEvent : INotification
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
