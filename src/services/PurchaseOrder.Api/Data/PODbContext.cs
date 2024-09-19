using MediatR;
using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Api.Domain.Aggregates.POAggregate;
using PurchaseOrder.Api.Domain.Aggregates.PRAggregate;
using PurchaseOrder.Api.Shared;

namespace PurchaseOrder.Api.Data
{
  public class PODbContext : DbContext
  {
    // EF Core ile birlikte Aggregate Root nenslerinin DbSetlerini yazıyoruz. Alt Entitylerde Migrationda oluşuyor.
    public DbSet<PurchaseRequest> PoRequests { get; set; }
    public DbSet<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrder> PoOrders { get; set; }
    private IMediator mediator;

    public PODbContext(DbContextOptions<PODbContext> opt, IMediator mediator) : base(opt)
    {
      this.mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Enumerations ile ValueObject mapping işlemi

      //modelBuilder.Entity<PurchaseRequest>().HasKey(x => x.Id);

      modelBuilder.Entity<PurchaseRequest>().OwnsOne(x => x.Status).Property(x => x.Id).HasColumnName("RequestStatus_Code");
      modelBuilder.Entity<PurchaseRequest>().OwnsOne(x => x.Status).Property(x => x.Name).HasColumnName("RequestStatus_Name");






      modelBuilder.Entity<PurchaseRequest>().OwnsOne(x => x.Budget).Property(x => x.currency).HasColumnName("Budget_Currency");
      modelBuilder.Entity<PurchaseRequest>().OwnsOne(x => x.Budget).Property(x => x.amount).HasColumnName("Budget_Amount");


      modelBuilder.Entity<PurchaseRequest>().HasMany(x => x.Items);



      //modelBuilder.Entity<PurchaseRequestItem>().HasKey(x => x.Id);

      modelBuilder.Entity<PurchaseRequestItem>().OwnsOne(x => x.ListPrice).Property(x => x.currency).HasColumnName("ListPrice_Currency");
      modelBuilder.Entity<PurchaseRequestItem>().OwnsOne(x => x.ListPrice).Property(x => x.amount).HasColumnName("ListPrice_Amount");


      modelBuilder.Entity<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrder>().OwnsOne(x => x.Status).Property(x => x.Id).HasColumnName("OrderStatus_Code");
      modelBuilder.Entity<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrder>().OwnsOne(x => x.Status).Property(x => x.Name).HasColumnName("OrderStatus_Name");


      modelBuilder.Entity<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrderItem>().OwnsOne(x => x.ListPrice).Property(x => x.currency).HasColumnName("ListPrice_Currency");
      modelBuilder.Entity<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrderItem>().OwnsOne(x => x.ListPrice).Property(x => x.amount).HasColumnName("ListPrice_Amount");

      modelBuilder.Entity<PurchaseOrder.Api.Domain.Aggregates.POAggregate.PurchaseOrder>().HasMany(x => x.Items);

      base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
      this.mediator.DispatchDomainEventsAsync(this).GetAwaiter().GetResult();
      // save öncesi ne kadar event var tetiklensin.
      return base.SaveChanges();
    }
  }
}
