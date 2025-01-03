using System.Text.Json;
using Axo.Functions.Save.Common;
using Axo.Functions.Save.Entities;
using Axo.Functions.Save.Repositories;
using Axo.Functions.Save.Serializer;
using Axo.Shared.FileService.Service;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.Save.Services;

public class SaveService(
  IFileService fileService,
  IPriceRepository priceRepository,
  IInventoryRepository inventoryRepository) : ISaveService
{
  public async Task<bool> HandleSaveLogic(LambdaInput lambdaInput)
  {
    var file = await fileService.GetContentFileS3(lambdaInput.FileKey);

    if (file is null)
    {
      return false;
    }

    return lambdaInput.Type switch
    {
      1 => await HandleSaveInventoryLogic(file),
      2 => await HandleSavePriceLogic(file),
      _ => false
    };
  }
  
  private async Task<bool> HandleSaveInventoryLogic(Stream file)
  {
    var fileContent = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.FileContentModelInventoryModel);

    var inventoryList = fileContent.ListInfo
      .Select(x => new InventoryEcommerce
      {
        Upc = x.Sku,
        CantidadDisponible = x.Qty
      }).ToList();
    
    var result = await inventoryRepository.SaveInventoryHistory(inventoryList);
    
    return result;
  }

  private async Task<bool> HandleSavePriceLogic(Stream file)
  {
    var fileContent = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.FileContentModelPriceModel);

    var priceList = fileContent.ListInfo
      .Select(x => new PriceEcommerce
      {
        Upc = x.Sku,
        PrecioLista = x.Price
      }).ToList();
    
    var result = await priceRepository.SavePriceHistory(priceList);
    
    return result;
  }
}