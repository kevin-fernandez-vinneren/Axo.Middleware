using Axo.Functions.Save.Entities;

namespace Axo.Functions.Save.Repositories;

public interface IInventoryRepository
{
  Task<bool> SaveInventoryHistory(List<InventoryEcommerce> inventoriesEcommerce);
}