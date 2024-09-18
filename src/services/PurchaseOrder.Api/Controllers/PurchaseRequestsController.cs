using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Api.Application.Features.PORequest;
using PurchaseOrder.Api.Domain.Aggregates.PRAggregate;
using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PurchaseRequestsController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly IPurchaseRequestRepository purchaseRequestRepository;
    private readonly IUnitOfWork unitOfWork;

    public PurchaseRequestsController(IMediator mediator, IPurchaseRequestRepository purchaseRequestRepository, IUnitOfWork unitOfWork)
    {
      this.mediator = mediator;
      this.purchaseRequestRepository = purchaseRequestRepository;
      this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var data = this.purchaseRequestRepository.Find(x => x.Status.Id == PurchaseRequestStatus.Pending.Id).ToList();

      

      return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseRequest request)
    {

      await mediator.Send(request);
    

      return Ok();

    }


    [HttpPost("transformAsOrder")]
    public async Task<IActionResult> TransformAsOrder()
    {
      // AsNoTracking yazarsak ChangeTracker sıfırlanır sıfırlanırsa kayıt etmeden önceki nesneye ait domain eventlere erişmeyiz.
      var entity = purchaseRequestRepository.FindById(new Guid("fc01af36-9621-4bf5-9750-47cfef673aab"));

      entity.TransformAsOrder();

      // mediator.Publish(entity.DomainEvents[0]);


      unitOfWork.SaveChanges();

      return Ok();
    }
  }
}
