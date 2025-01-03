using System.Text;
using System.Text.Json;
using Abercrombie.Application.Contracts.ExternalApis;
using Abercrombie.Application.Contracts.Repositories;
using Abercrombie.Application.Contracts.Services;
using Abercrombie.Domain.Entities;
using Abercrombie.Domain.Models;
using Abercrombie.Infrastructure.Serializer;
using Axo.Shared.FileService.Service;
using Axo.Shared.SecretsManagerService.Service;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Abercrombie.Infrastructure.Services;

public class AbercrombieService(
  ILogger logger,
  IPriceRepository priceRepository,
  IFileService fileService,
  ISecretsManagerService secretsManagerService,
  IKnApiService knApiService) : IAbercrombieService
{
  public async Task<bool> HandlePriceLogic(int merchantId)
  {
    var priceInfo = await priceRepository.GetPriceInfo(merchantId);

    if (priceInfo is null)
    {
      return false;
    }
    
    var obj = new CreationFileModel<PriceModel>
    {
      MerchantId = merchantId,
      ListInfo = priceInfo.Select(x => new PriceModel
      {
        Sku = x.Upc,
        Price = x.PrecioLista,
      }).ToList()
    };

    var jsonPrice = JsonSerializer.Serialize(obj, CustomSerializationContext.Default.CreationFileModelPriceModel);

    var byteArray = Encoding.UTF8.GetBytes(jsonPrice);

    var file = fileService.GenerateFile(byteArray);

    var path = Environment.GetEnvironmentVariable("PricePath");

    var fileName = $"{path}/Abercrombie_Price.json";

    var urlFile = await fileService.SaveFileToS3(file, fileName);

    if (urlFile is null)
    {
      return false;
    }

    var result = await priceRepository.RemovePriceInfo(merchantId);

    return result;
  }

  public async Task<bool> HandleInventoryLogic(int merchantId)
  {
    var request = new KnGetInventoryRequest
    {
      Query = new InventoryRequestQuery
      {
        Parametros = new InventoryRequestParameters
        {
          TipoConsulta = "STOCK_FULL"
        }
      }
    };
    
    var token = await secretsManagerService.GetSecrets("KnToken");

    if (token is null)
    {
      return false;
    }

    var inventory = new List<InventoryItem>();

    var cicle = 1;
    while (true)
    {
      request.Query.Parametros.Ciclo = cicle;
      
      var inventoryInfo = await knApiService.GetInventory(token, request);

      if (!inventoryInfo.IsSuccessful)
      {
        logger.LogErrorCustom("An error occurred while trying to get the inventory info", inventoryInfo.Error);
        return false;
      }
      
      var invenotryItems = inventoryInfo.Content.Results.Articulos.Articulo;
      
      inventory.AddRange(invenotryItems);
      
      if (inventoryInfo.Content.Results.Stock.FinCiclo == cicle)
      {
        break;
      }
      
      cicle++;
    }
    
    var obj = new CreationFileModel<InventoryModel>
    {
      MerchantId = merchantId,
      ListInfo = inventory.Select(x => new InventoryModel
      {
        Sku = x.Ean,
        Qty = x.Stock,
        IsInStock = x.Stock > 0 ? 1 : 0
      }).ToList()
    };
    
    var jsonInventory = JsonSerializer.Serialize(obj, CustomSerializationContext.Default.CreationFileModelInventoryModel);
    
    var byteArray = Encoding.UTF8.GetBytes(jsonInventory);
    
    var file = fileService.GenerateFile(byteArray);
    
    var path = Environment.GetEnvironmentVariable("InventoryPath");
    
    var fileName = $"{path}/Abercrombie_Inventory.json";
    
    var urlFile = await fileService.SaveFileToS3(file, fileName);
    
    return urlFile is not null;
  }
}