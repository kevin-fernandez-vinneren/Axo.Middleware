using Axo.Functions.Save.Entities;

namespace Axo.Functions.Save.Repositories;

public interface IPriceRepository
{
  Task<bool> SavePriceHistory(List<PriceEcommerce> pricesEcommerce);
}