using MediatR;

namespace PurchaseOrder.Api.SeedWork
{
  public abstract class AggregateRoot :Entity, IAggregateRoot
  {

    private List<INotification> _domainEvents = new List<INotification>();
    public List<INotification> DomainEvents => _domainEvents;
    public void AddDomainEvents(INotification @event)
    {
      _domainEvents.Add(@event);
    }
  }
}
