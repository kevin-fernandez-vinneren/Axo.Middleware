using System.Text;
using System.Text.Json;
using Axo.Functions.FileSegmentation.Common;
using Axo.Functions.FileSegmentation.Models;
using Axo.Functions.FileSegmentation.Serializer;
using Axo.Shared.FileService.Service;

namespace Axo.Functions.FileSegmentation.Services;

public class FileSegmentationService(IFileService fileService) : IFileSegmentationService
{
  public async Task<List<string>?> HandleFileSegmentation(LambdaInput lambdaInput)
  {
    var file = await fileService.GetContentFileS3(lambdaInput.FileKey);

    if (file is null)
    {
      return null;
    }

    switch (lambdaInput.Type)
    {
     case 1:
       return await InventorySegmentation(file);
     
      case 2:
        return await PriceSegmentation(file);
      
      default:
        return null;
    } 
  }

  private async Task<List<string>?> InventorySegmentation(Stream file)
  {
    var fileContent = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.FileContentModelInventoryModel);

    var segmentedInventory = new List<List<InventoryModel>>();
    for (var i = 0; i < fileContent.ListInfo.Count; i += 1000)
    {
      segmentedInventory.Add(fileContent.ListInfo.Skip(i).Take(1000).ToList());
    }
    
    var fileNumber = 1;
    var uploadTasks = new List<Task<string?>>();
    
    foreach (var item in segmentedInventory)
    {
      var obj = new FileContentModel<InventoryModel>
      {
        MerchantId = fileContent.MerchantId,
        ListInfo = item
      };
      
      var jsonInventory = JsonSerializer.Serialize(obj, CustomSerializationContext.Default.FileContentModelInventoryModel);
      
      var byteArray = Encoding.UTF8.GetBytes(jsonInventory);
      
      var fileGenerated = fileService.GenerateFile(byteArray);
      
      var path = Environment.GetEnvironmentVariable("InventoryPath");
      
      var fileName = $"{path}/Segmentation/Ulta_Inventory_{fileNumber}.json";
      
      uploadTasks.Add(fileService.SaveFileToS3(fileGenerated, fileName));
      
      fileNumber++;
    }
    
    var uploadResults = await Task.WhenAll(uploadTasks);
    
    return uploadResults.Any(url => url is null) ? null : uploadResults.ToList();
  }

  private async Task<List<string>?> PriceSegmentation(Stream file)
  {
    var fileContent = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.FileContentModelPriceModel);

    var segmentedPrice = new List<List<PriceModel>>();
    for (var i = 0; i < fileContent.ListInfo.Count; i += 1000)
    {
      segmentedPrice.Add(fileContent.ListInfo.Skip(i).Take(1000).ToList());
    }

    var fileNumber = 1;
    var uploadTasks = new List<Task<string?>>();

    foreach (var item in segmentedPrice)
    {
      var obj = new FileContentModel<PriceModel>
      {
        MerchantId = fileContent.MerchantId,
        ListInfo = item
      };
      
      var jsonPrice = JsonSerializer.Serialize(obj, CustomSerializationContext.Default.FileContentModelPriceModel);

      var byteArray = Encoding.UTF8.GetBytes(jsonPrice);

      var fileGenerated = fileService.GenerateFile(byteArray);

      var path = Environment.GetEnvironmentVariable("PricePath");

      var fileName = $"{path}/Segmentation/Ulta_Price_{fileNumber}.json";

      uploadTasks.Add(fileService.SaveFileToS3(fileGenerated, fileName));

      fileNumber++;
    }

    var uploadResults = await Task.WhenAll(uploadTasks);

    return uploadResults.Any(url => url is null) ? null : uploadResults.ToList();
  }
}