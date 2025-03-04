using MediatR;

namespace Ordering.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTime OccurredOn => DateTime.UtcNow;
    string? EventType => GetType().AssemblyQualifiedName;
}
