namespace NorthWind.Events.Entities.Interfaces;

public interface IDomainEvent;

public interface IDomainEventHandler<in TEventType> where TEventType : IDomainEvent
{
    Task Handle(TEventType domainEvent);
}

public interface IDomainEventHub<in TEventType> where TEventType : IDomainEvent
{
    Task Raise(TEventType domainEvent);
}
