using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Text.Json.Serialization;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.S3Events;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.FilesTrigger;

public class Function(ILogger logger)
{
  [LambdaFunction]
  public void FunctionHandler(S3Event s3Event, ILambdaContext context)
  {
    logger.LogInformationCustom(s3Event);
  }
}