namespace NorthWind.Events.Entities.Services;

internal class DomainEventHub<TEventType>(
    IEnumerable<IDomainEventHandler<TEventType>> handlers) : IDomainEventHub<TEventType>
    where TEventType : IDomainEvent
{
    public async Task Raise(TEventType domainEvent)
    {
        foreach (var Handler in handlers)
        {
            await Handler.Handle(domainEvent);
        }
    }
}
