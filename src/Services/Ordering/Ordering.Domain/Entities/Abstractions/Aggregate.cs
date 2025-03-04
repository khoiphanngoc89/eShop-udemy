using Ordering.Domain.Events;

namespace Ordering.Domain.Entities.Abstractions;

public abstract class Aggregate<TId> : EntityBase<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    public void AddEvents(params IDomainEvent[] events)
    {
        _events.AddRange(events);
    }

    public IDomainEvent[] ClearEvents()
    {
        var results = _events.ToArray();
        _events.Clear();
        return results;
    }
}
