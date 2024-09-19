using Contracts;
using DotNetCore.CAP;

namespace Billing.Api.Consumers
{
  public class OrderCompletedConsumer : ICapSubscribe
  {
    [CapSubscribe(QueueTypes.OrderCompleted)]
    public void Consume(OrderCompleted message)
    {
      Console.WriteLine("Consumed" + message.Id);

      // Git Bunu Invoiced olarak işaretle
      // Publish Net Core Cap. Gönderim sağla.
    }
  }
}
