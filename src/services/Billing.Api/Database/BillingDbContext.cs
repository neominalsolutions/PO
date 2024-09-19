using Microsoft.EntityFrameworkCore;

namespace Billing.Api.Database
{
  public class BillingDbContext:DbContext
  {
        public BillingDbContext(DbContextOptions<BillingDbContext> opt):base(opt)
        {
              
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
