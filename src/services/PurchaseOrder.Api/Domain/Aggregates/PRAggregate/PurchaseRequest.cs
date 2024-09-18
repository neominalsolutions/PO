using PurchaseOrder.Api.SeedWork;
using PurchaseOrder.Api.Shared;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{
  // Aggregate Root
  public class PurchaseRequest:AggregateRoot
  {

    public Money Budget { get; init; }

    private readonly List<PurchaseRequestItem> items = new List<PurchaseRequestItem>();

    public IReadOnlyList<PurchaseRequestItem> Items => items;
    public PurchaseRequestStatus Status { get; private set; }

    public PurchaseRequest()
    {
      
    }

    private PurchaseRequest(Money budget)
    {
      Budget = budget;
      Status = PurchaseRequestStatus.Pending;
    }

    public static PurchaseRequest Create(Money budget)
    {
      return new PurchaseRequest(budget);
    }


    public void AddItem(PurchaseRequestItem item)
    {
      // Eklenen itemlar butceyi aşıyor mu kontrolü yapalım

      var total =  items.Sum(x => x.ListPrice.amount * x.Quantity) + (item.Quantity * item.ListPrice.amount);

      if(total < Budget.amount)
      {
        items.Add(item);
      }
      else
      {
        throw new Exception("Bütçe aşıldı");
      }
    }

    public void Rejected()
    {
      Status = PurchaseRequestStatus.Rejected;
    }

    public void TransformAsOrder()
    {
      // Event fırlatacağız. Bu sayede Purchase Order otomatik oluşacak.

      DomainEvents.Add(new TransformAsOrder(Id));
      Status = PurchaseRequestStatus.Ordered;

    }




  }
}
