using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{

  public class PurchaseRequestStatus
    : Enumeration
  {
    public static PurchaseRequestStatus Pending = new(1, nameof(Pending));
    public static PurchaseRequestStatus Ordered = new(2, nameof(Ordered));
    public static PurchaseRequestStatus Rejected = new(3, nameof(Rejected));

    public PurchaseRequestStatus(int id, string name)
        : base(id, name)
    {
    }
  }
}
