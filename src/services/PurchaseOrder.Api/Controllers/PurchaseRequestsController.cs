using Contracts;
using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Api.Application.Features.PORequest;
using PurchaseOrder.Api.Data;
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
    private readonly ICapPublisher capPublisher;
    private readonly PODbContext db;

    public PurchaseRequestsController(IMediator mediator, IPurchaseRequestRepository purchaseRequestRepository, IUnitOfWork unitOfWork, PODbContext db, ICapPublisher capPublisher)
    {
      this.mediator = mediator;
      this.purchaseRequestRepository = purchaseRequestRepository;
      this.unitOfWork = unitOfWork;
      this.db = db;
      this.capPublisher = capPublisher;
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


    [HttpPost("transformAsOrder/{id}")]
    public async Task<IActionResult> TransformAsOrder(Guid id)
    {
      // AsNoTracking yazarsak ChangeTracker sıfırlanır sıfırlanırsa kayıt etmeden önceki nesneye ait domain eventlere erişmeyiz.



      var entity = purchaseRequestRepository.FindById(id);

      if (entity is not null)
      {
        // appliation katmanında tutalım.
        using (var tran = db.Database.BeginTransaction(capPublisher,autoCommit:true))
        {
          entity.TransformAsOrder();
          var total = entity.Items.Sum(x => x.ListPrice.amount);

          var @event = new OrderCompleted(entity.Id, total, entity.Budget.currency);

          // veri tabanına düzgün kaydedebilirse aynı zamanda published tablosunada kayıt atabilecek.
          unitOfWork.SaveChanges();

          await this.capPublisher.PublishAsync("OrderCompleted3", @event);

          return Ok();
        }

      }


      return BadRequest("Purchase Request Order işlemi gerçekleşmedi");

    }
  }
}
