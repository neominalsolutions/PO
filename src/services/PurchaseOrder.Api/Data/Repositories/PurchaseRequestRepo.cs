using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Api.Domain.Aggregates.PRAggregate;
using System.Linq.Expressions;

namespace PurchaseOrder.Api.Data.Repositories
{
  public class PurchaseRequestRepo : IPurchaseRequestRepository
  {
    private readonly PODbContext db;

    public PurchaseRequestRepo(PODbContext db)
    {
      this.db = db;
    }
    public void Create(PurchaseRequest aggregate)
    {
      this.db.PoRequests.Add(aggregate);
      // Not save hemen altın yazmadık.
    }

    public List<PurchaseRequest> Find(Expression<Func<PurchaseRequest, bool>> lambda)
    {
      return this.db.PoRequests.Where(lambda).AsNoTracking().Include(x=> x.Items).ToList();

      
    }

    public PurchaseRequest FindById(Guid id)
    {
      return this.db.PoRequests.Include(x => x.Items).FirstOrDefault(x => x.Id == id);
    }
  }
}
