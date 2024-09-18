using MediatR;

namespace PurchaseOrder.Api.Application.Features.PORequest
{
  public record PurchaseRequestItemDto(string name,string code,int quantity,decimal listPrice,string currency);
 
  public record PurchaseRequestBudgetDto(string currency,decimal amount);

  public record CreatePurchaseRequest(List<PurchaseRequestItemDto> items, PurchaseRequestBudgetDto budget) : IRequest;
 
}
