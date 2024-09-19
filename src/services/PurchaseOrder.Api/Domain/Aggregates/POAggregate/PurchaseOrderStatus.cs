using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Domain.Aggregates.POAggregate
{


  public class PurchaseOrderStatus
   : Enumeration
  {
    // Requesten Ordera dönerken Ordered sayısın.
    public static PurchaseOrderStatus Ordered = new(1, nameof(Ordered));
    // Billing.Api Kayıt sonrası Invoiced olarak işaretlensin
    public static PurchaseOrderStatus Invoiced = new(2, nameof(Invoiced));

    public PurchaseOrderStatus(int id, string name)
        : base(id, name)
    {
    }
  }
}
