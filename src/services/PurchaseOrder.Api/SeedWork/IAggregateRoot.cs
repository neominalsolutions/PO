using MediatR;

namespace PurchaseOrder.Api.SeedWork
{
  public interface IAggregateRoot
  {
    // Not INotification setter DB tarafında mapleneceğini düşünüyor.
    List<INotification> DomainEvents { get; }
    void AddDomainEvents(INotification @event);
  }
}
