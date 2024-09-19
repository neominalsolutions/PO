using PurchaseOrder.Api.SeedWork;
using PurchaseOrder.Api.Shared;

namespace PurchaseOrder.Api.Domain.Aggregates.POAggregate
{
  public class PurchaseOrderItem : Entity
  {

    public string Code { get; init; }
    public Money ListPrice { get; init; } // Value Object
    public string Name { get; init; }
    public int Quantity { get; set; }

    public PurchaseOrderItem()
    {

    }

    private PurchaseOrderItem(string name, string code, int quantity, Money listPrice)
    {
      Name = name;
      Code = code;
      Quantity = quantity;
      ListPrice = listPrice;
    }

    public static PurchaseOrderItem Create(string name, string code, int quantity, Money listPrice)
    {
      return new PurchaseOrderItem(name, code, quantity, listPrice);
    }



  }
}
