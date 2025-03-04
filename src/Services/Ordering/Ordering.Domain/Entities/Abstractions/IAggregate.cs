using Ordering.Domain.Events;

namespace Ordering.Domain.Entities.Abstractions;

public interface IAggregate<T> : IAggregate, IEntityBase<T>
{
}

public interface IAggregate : IAuditable
{
    IReadOnlyList<IDomainEvent> Events { get; }
    IDomainEvent[] ClearEvents();
}
