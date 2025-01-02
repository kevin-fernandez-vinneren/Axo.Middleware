using Axo.Functions.Save.Entities;

namespace Axo.Functions.Save.Repositories;

public interface IPriceRepository
{
  Task<IEnumerable<PriceEcommerce>?> GetPriceInfo(int merchantId);
  Task<bool> SavePriceHistory(List<PriceEcommerce> pricesEcommerce);
  Task<bool> RemovePriceInfo(int merchantId);
}