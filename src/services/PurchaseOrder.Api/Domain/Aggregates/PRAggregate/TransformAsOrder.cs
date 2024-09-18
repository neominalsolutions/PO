using MediatR;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{
  // Event
  public record TransformAsOrder(Guid Id):INotification;
 

}
