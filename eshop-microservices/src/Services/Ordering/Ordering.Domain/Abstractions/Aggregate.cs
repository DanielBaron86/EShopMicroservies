namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomaninEvent> _domainEvents = new();
    public IReadOnlyList<IDomaninEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomaninEvent domaninEvent)
    {
        _domainEvents.Add(domaninEvent);
    }
    public IDomaninEvent[] ClearDomaninEvents()
    {
        IDomaninEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }
}
