using MediatR;
using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Data
{
  public static class MediatorExtension
  {
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
    {
      var domainEntities = ctx.ChangeTracker
                              .Entries<IAggregateRoot>()
                              .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

      var domainEvents = domainEntities
          .SelectMany(x => x.Entity.DomainEvents)
          .ToList();

      //domainEntities.ToList()
      //          .ForEach(entity => entity.Entity.ClearDomainEvents());

      foreach (var domainEvent in domainEvents)
      {
        await mediator.Publish(domainEvent);
      }

    }
  }
}
