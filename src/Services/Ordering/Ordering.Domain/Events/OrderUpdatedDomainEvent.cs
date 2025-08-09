namespace Ordering.Domain.Events;

public sealed record OrderUpdatedDomainEvent(Order Order) : IDomainEvent;
