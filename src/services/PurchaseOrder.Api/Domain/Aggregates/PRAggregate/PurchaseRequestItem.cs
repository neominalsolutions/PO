using PurchaseOrder.Api.SeedWork;
using PurchaseOrder.Api.Shared;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{
  // Child
  public class PurchaseRequestItem:Entity
  {

    public string Code { get; init; }
    public Money ListPrice { get; init; }
    public string Name { get; init; }
    public int Quantity { get; set; }

    private PurchaseRequestItem(string code,string name,int quantity,Money listPrice)
    {
      Code = code;
      Name = name;
      ListPrice = listPrice;
      Quantity = quantity;
    }

    public PurchaseRequestItem()
    {

    }

    public static PurchaseRequestItem Create(string code, string name,int quantity, Money listPrice)
    {
      return new PurchaseRequestItem(code, name,quantity, listPrice);
    }

  }
}
