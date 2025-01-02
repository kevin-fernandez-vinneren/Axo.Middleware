using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Axo.Functions.UploadInfo.Common;
using Axo.Functions.UploadInfo.Services;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.UploadInfo;

public class Function(ILogger logger, IUploadInfoService uploadInfoService)
{
  [LambdaFunction]
  public async Task<LambdaInput?> FunctionHandler(LambdaInput lambdaInput, ILambdaContext context)
  {
    var response = await uploadInfoService.HandleUploadInfo(lambdaInput);

    if (response) return lambdaInput;
    
    logger.LogErrorCustom("The content could not be processed", null);
    return null;
  }
}