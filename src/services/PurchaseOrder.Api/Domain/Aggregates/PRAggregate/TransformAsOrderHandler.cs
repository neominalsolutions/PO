using DotNetCore.CAP;
using MediatR;
using PurchaseOrder.Api.Domain.Aggregates.POAggregate;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{
  public class TransformAsOrderHandler : INotificationHandler<TransformAsOrder>
  {
    private readonly IPurchaseOrderRepository purchaseOrderRepository;
    private readonly IPurchaseRequestRepository purchaseRequestRepository;


    public TransformAsOrderHandler(IPurchaseOrderRepository purchaseOrderRepository, IPurchaseRequestRepository purchaseRequestRepository, ICapPublisher capPublisher)
    {
      this.purchaseOrderRepository = purchaseOrderRepository;
      this.purchaseRequestRepository = purchaseRequestRepository;
    }

    public async Task Handle(TransformAsOrder notification, CancellationToken cancellationToken)
    {
    

      var purchaseRequest = this.purchaseRequestRepository.Find(x=> x.Id == notification.Id).FirstOrDefault();


      List<PurchaseOrderItem> items = new();

      purchaseRequest.Items.ToList().ForEach(a =>
      {
        items.Add(PurchaseOrderItem.Create(a.Name, a.Code, a.Quantity, a.ListPrice));
      });

      var purchaseOrder = PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrder.Create(notification.Id, items);

      this.purchaseOrderRepository.Create(purchaseOrder); // Added State


      if (purchaseRequest.Status != PurchaseRequestStatus.Pending)
      {
        throw new Exception("Purchase Request Status Pending Durumda Değil");
      }


      await Task.CompletedTask;
    }
  }
}
