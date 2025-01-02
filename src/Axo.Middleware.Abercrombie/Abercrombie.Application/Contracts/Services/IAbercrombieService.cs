namespace Abercrombie.Application.Contracts.Services;

public interface IAbercrombieService
{
  Task<bool> HandlePriceLogic(int merchantId);
  Task<bool> HandleInventoryLogic(int merchantId);
}