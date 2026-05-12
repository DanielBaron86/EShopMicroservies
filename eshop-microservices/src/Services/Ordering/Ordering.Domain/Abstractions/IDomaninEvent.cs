using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomaninEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}