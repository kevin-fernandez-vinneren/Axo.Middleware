using System.Text.Json;
using Axo.Functions.UploadInfo.Common;
using Axo.Functions.UploadInfo.Serializer;
using Axo.Shared.FileService.Service;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.UploadInfo.Services;

public class UploadInfoService(ILogger logger, IFileService fileService) : IUploadInfoService
{
  public async Task<bool> HandleUploadInfo(LambdaInput lambdaInput)
  {
    var file = await fileService.GetContentFileS3(lambdaInput.FileKey);
    
    if (file is null)
    {
      return false;
    }

    switch (lambdaInput.Type)
    {
      case 1:
        var inventoryResult = await HandleInventoryInfo(file);
        return inventoryResult;
      
      case 2:
        var priceResult = await HandlePriceUpload(file);
        return priceResult;
      
      default:
        return false;
    }
  }

  private async Task<bool> HandlePriceUpload(Stream file)
  {
    var priceInfo = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.ListPriceModel);
    
    logger.LogInformationCustom(priceInfo);
    
    return true;
  }
  
  private async Task<bool> HandleInventoryInfo(Stream file)
  {
    var productInfo = JsonSerializer.Deserialize(file, CustomSerializationContext.Default.ListInventoryModel);
    
    logger.LogInformationCustom(productInfo);
    
    return true;
  }
}