using System.Linq.Expressions;

namespace PurchaseOrder.Api.SeedWork
{
  public interface IRepository<T>
    where T:IAggregateRoot
  {
    void Create(T aggregate);

    List<T> Find(Expression<Func<T, bool>> lambda);

    T FindById(Guid id);
  }
}
