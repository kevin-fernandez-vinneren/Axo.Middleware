using Axo.Functions.Save.Entities;
using Axo.Shared.SecretsManagerService.Service;
using Dapper;
using Logify.Extensions;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Axo.Functions.Save.Repositories;

[DapperAot]
public class PriceRepository(ILogger logger, ISecretsManagerService secretsManagerService) : IPriceRepository
{
  public async Task<IEnumerable<PriceEcommerce>?> GetPriceInfo(int merchantId)
  {
    var connectionString = await secretsManagerService.GetSecrets("AxoConnectionString");

    await using var conn = new MySqlConnection(connectionString);
    
    try
    {
      var data = await conn.QueryAsync<PriceEcommerce>(@"SELECT * FROM CNTRL_PRECIOS_ECOMMERCE 
                                                       WHERE ID_ECOM = @merchantId", new { merchantId });
      
      return data;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom($"An error occurred while trying to get the price info for merchantId: {merchantId}", e);
      return null;
    }
  }

  public async Task<bool> SavePriceHistory(List<PriceEcommerce> pricesEcommerce)
  {
    var connectionString = await secretsManagerService.GetSecrets("AxoConnectionString");

    await using var conn = new MySqlConnection(connectionString);
    
    try
    {
      var result = await conn.ExecuteAsync(@"INSERT INTO CNTRL_PRECIOS_ECOMMERCE_HIST
                                             (UPC, PRECIO_LISTA, PRECIO_FINAL, REGALO, FECHA_CARGA_MD) 
                                             VALUES (@Upc, @PrecioLista, @PrecioFinal, @Regalo, @FechaCargaMd)", pricesEcommerce);
      
      return result > 0;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom("An error occurred while trying to save the price history", e);
      return false;
    }
  }

  public async Task<bool> RemovePriceInfo(int merchantId)
  {
    var connectionString = await secretsManagerService.GetSecrets("AxoConnectionString");

    await using var conn = new MySqlConnection(connectionString);
    
    try
    {
      var result = await conn.ExecuteAsync(@"DELETE FROM CNTRL_PRECIOS_ECOMMERCE 
                                             WHERE ID_ECOM = @merchantId", new { merchantId });
      
      return result > 0;
    }
    catch (Exception e)
    {
      logger.LogErrorCustom($"An error occurred while trying to remove the price info for merchantId: {merchantId}", e);
      return false;
    }
  }
}