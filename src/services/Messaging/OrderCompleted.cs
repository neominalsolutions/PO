namespace Messaging
{
  public record OrderCompleted(Guid requestId,decimal total,string currency);
  
}
