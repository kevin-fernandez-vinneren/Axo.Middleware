using Amazon.Lambda.Core;
using Abercrombie.Application.Contracts.Services;
using Abercrombie.Domain.Common;
using Amazon.Lambda.Annotations;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Abercrombie.Lambda;

public class Function(ILogger logger, IAbercrombieService abercrombieService)
{
  [LambdaFunction]
  public async Task<string> FunctionHandler(LambdaInput input, ILambdaContext context)
  {
    switch (input.Type)
    {
      case 1:
        var resultInventory = await abercrombieService.HandleInventoryLogic(input.MerchantId);
        
        if (!resultInventory)
        {
          logger.LogErrorCustom("Error processing inventory logic", null);
          return "Error processing inventory logic";
        }
        break;
      
      case 2:
        var resultPrice = await abercrombieService.HandlePriceLogic(input.MerchantId);
        
        if (!resultPrice)
        {
          logger.LogErrorCustom("Error processing price logic", null);
          return "Error processing price logic";
        }
        break;
      
      default:
        return "There is no logic for the type provided";
    }
    
    return "Lambda Function executed successfully";
  }
}