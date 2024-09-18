using MediatR;
using PurchaseOrder.Api.Domain.Aggregates.PRAggregate;
using PurchaseOrder.Api.SeedWork;
using PurchaseOrder.Api.Shared;

namespace PurchaseOrder.Api.Application.Features.PORequest
{
  public class CreatePurchaseRequestCommand : IRequestHandler<CreatePurchaseRequest>
  {
    private readonly IPurchaseRequestRepository purchaseRequestRepository;
    private readonly IUnitOfWork unitOfWork;
    

    public CreatePurchaseRequestCommand(IPurchaseRequestRepository purchaseRequestRepository, IUnitOfWork unitOfWork)
    {
      this.purchaseRequestRepository = purchaseRequestRepository;
      this.unitOfWork = unitOfWork;
    }

    public async Task Handle(CreatePurchaseRequest request, CancellationToken cancellationToken)
    {
      var money = new Money(request.budget.currency, request.budget.amount);

      var aggregate = PurchaseRequest.Create(budget: money);

      // Domain katmanında direkt olarak entity veya aggregate nesneleri ile çalışırız. Dto veya request nesnesi göndermemeye dikkat edelim.
      request.items.ForEach(item => {

        var poItem = PurchaseRequestItem.Create(item.code, item.name, item.quantity, new Money(currency:item.currency, amount:item.listPrice));

        aggregate.AddItem(poItem);
    
      });

      purchaseRequestRepository.Create(aggregate);
      // Bu kısıma geçerken tüm eventler fırlatılmış olmasına dikkat edelim.
      unitOfWork.SaveChanges();
    }
  }
}
