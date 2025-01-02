using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Text.Json.Serialization;
using Amazon.Lambda.Annotations;
using Axo.Functions.FileSegmentation.Common;
using Axo.Functions.FileSegmentation.Models;
using Axo.Functions.FileSegmentation.Services;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.FileSegmentation;

public class Function(ILogger logger, IFileSegmentationService fileSegmentationService)
{
  [LambdaFunction]
  public async Task<List<string>?> FunctionHandler(LambdaInput input, ILambdaContext context)
  {
    var result = await fileSegmentationService.HandleFileSegmentation(input);

    if (result is null)
    {
      logger.LogErrorCustom("The file could not be processed", null);
      return null;
    }
    
    return result;
  }
}