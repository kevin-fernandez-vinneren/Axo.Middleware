using Abercrombie.Application.Contracts.Repositories;
using Abercrombie.Domain.Entities;
using Axo.Shared.SecretsManagerService.Service;
using Dapper;
using Logify.Extensions;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Abercrombie.Infrastructure.Repositories;

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