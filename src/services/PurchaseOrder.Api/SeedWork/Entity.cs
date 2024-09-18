using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Api.SeedWork
{
  public abstract class Entity
  {
  
    public Guid Id { get; set; }
    public DateTime CreateAt { get; init; }


    public Entity()
    {
      Id = Guid.NewGuid();
      CreateAt = DateTime.Now;
    }
  }
}
