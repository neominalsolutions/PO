using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Domain.Aggregates.POAggregate
{
  public class PurchaseOrder : AggregateRoot
  {
    public IReadOnlyList<PurchaseOrderItem> Items { get; set; } //

    public Guid PurchaseRequestId { get; init; } // Hangi requesten geliyor


    public PurchaseOrderStatus Status { get; private set; } // Inital Ordered // Enumeration 

    public PurchaseOrder()
    {

    }

    private PurchaseOrder(Guid purchaseRequestId,List<PurchaseOrderItem> items)
    {
      Status = PurchaseOrderStatus.Ordered;
      Items = items;
      PurchaseRequestId = purchaseRequestId;
    }

    public static PurchaseOrder Create(Guid purchaseRequestId,List<PurchaseOrderItem> items)
    {
      return new PurchaseOrder(purchaseRequestId, items);
    }

  }
}
