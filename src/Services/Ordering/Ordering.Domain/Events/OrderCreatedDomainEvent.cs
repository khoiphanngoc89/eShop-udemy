using Ordering.Domain.Entities;

namespace Ordering.Domain.Events;

public  sealed record OrderCreatedDomainEvent(Order Order) : IDomainEvent;
