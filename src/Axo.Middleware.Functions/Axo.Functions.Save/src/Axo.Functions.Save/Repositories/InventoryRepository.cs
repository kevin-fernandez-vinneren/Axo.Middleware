using Axo.Functions.Save.Entities;
using Axo.Shared.SecretsManagerService.Service;
using Dapper;
using Logify.Extensions;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Axo.Functions.Save.Repositories;

[DapperAot]
public class InventoryRepository(ILogger logger, ISecretsManagerService secretsManagerService) : IInventoryRepository
{
  public async Task<bool> SaveInventoryHistory(List<InventoryEcommerce> inventoriesEcommerce)
  {
    var connectionString = await secretsManagerService.GetSecrets("AxoConnectionString");

    await using var conn = new MySqlConnection(connectionString);
    
    try
    {
      var result = await conn.ExecuteAsync(@"INSERT INTO CNTRL_INVENTARIO_ECOMMERCE_HIST
                                             (UPC, CANTIDAD_DISPONIBLE, FECHA_CARGA_MD, FECHA_CARGA_DHW, ID_ECOM, TIENDA_ECOM, FLAG) 
                                             VALUES (@Upc, @CantidadDisponible, @FechaCargaMd, @FechaCargaDhw, @IdEcom, @TiendaEcom, @Flag)", inventoriesEcommerce);
      
      return result > 0;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom("An error occurred while trying to save the inventory history", e);
      return false;
    }
  }
}