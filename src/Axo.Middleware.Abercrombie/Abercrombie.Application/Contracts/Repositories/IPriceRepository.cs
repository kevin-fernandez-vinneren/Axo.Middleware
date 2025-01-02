using Abercrombie.Domain.Entities;

namespace Abercrombie.Application.Contracts.Repositories;

public interface IPriceRepository
{
  Task<IEnumerable<PriceEcommerce>?> GetPriceInfo(int merchantId);
  Task<bool> RemovePriceInfo(int merchantId);
}