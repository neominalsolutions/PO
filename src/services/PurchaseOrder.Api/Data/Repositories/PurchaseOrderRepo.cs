using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Api.Domain.Aggregates.POAggregate;
using System.Linq.Expressions;

namespace PurchaseOrder.Api.Data.Repositories
{
  public class PurchaseOrderRepo : IPurchaseOrderRepository
  {
    private readonly PODbContext db;

    public PurchaseOrderRepo(PODbContext db)
    {
      this.db = db;
    }
    public void Create(Domain.Aggregates.POAggregate.PurchaseOrder aggregate)
    {
      this.db.Add(aggregate);
    }

    public List<Domain.Aggregates.POAggregate.PurchaseOrder> Find(Expression<Func<Domain.Aggregates.POAggregate.PurchaseOrder, bool>> lambda)
    {
      return this.db.PoOrders.Include(x=> x.Items).AsNoTracking().Where(lambda).ToList();
    }

    public Domain.Aggregates.POAggregate.PurchaseOrder FindById(Guid id)
    {
      return this.db.PoOrders.Include(x => x.Items).FirstOrDefault(x=> x.Id == id);
    }
  }
}
