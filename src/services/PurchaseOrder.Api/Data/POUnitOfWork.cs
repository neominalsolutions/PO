using PurchaseOrder.Api.SeedWork;

namespace PurchaseOrder.Api.Data
{
  public class POUnitOfWork : IUnitOfWork
  {
    private readonly PODbContext db;

    public POUnitOfWork(PODbContext db)
    {
      this.db = db;
    }
    public int SaveChanges()
    {
       return this.db.SaveChanges();
    }
  }
}
