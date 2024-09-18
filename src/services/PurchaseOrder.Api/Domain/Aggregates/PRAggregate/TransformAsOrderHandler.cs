using MediatR;

namespace PurchaseOrder.Api.Domain.Aggregates.PRAggregate
{
  public class TransformAsOrderHandler : INotificationHandler<TransformAsOrder>
  {
    public async Task Handle(TransformAsOrder notification, CancellationToken cancellationToken)
    {
      // eventten aldığı bilgiye göre aggregate dışında servis olarak sürecine devam etsin.
      Console.WriteLine("Order'a Dönüştü");


      // Request Status Güncelleme işlemi
      //Status = PurchaseRequestStatus.Ordered;

      await Task.CompletedTask;
    }
  }
}
