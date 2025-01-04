using System.Net;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.S3Events;
using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;
using Axo.Functions.FilesTrigger.Models;
using Axo.Functions.FilesTrigger.Serializer;
using Logify.Extensions;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.FilesTrigger;

public class Function(ILogger logger, IAmazonStepFunctions amazonStepFunctions)
{
  [LambdaFunction]
  public async Task FunctionHandler(S3Event s3Event, ILambdaContext context)
  {
    logger.LogInformationCustom(s3Event);
    
    var s3Key = s3Event.Records[0].S3.Object.Key;
    
    var stateMachineArn = Environment.GetEnvironmentVariable("STATE_MACHINE_ARN");
    
    var stepFunctionInput = new StepFunctionInput
    {
      FileKey = s3Key,
      Type = s3Key.Contains("inventory", StringComparison.CurrentCultureIgnoreCase) ? 1 : 2
    };

    var request = new StartExecutionRequest
    {
      StateMachineArn = stateMachineArn,
      Input = JsonSerializer.Serialize(stepFunctionInput, CustomSerializationContext.Default.StepFunctionInput)
    };
    
    try
    {
      var responseExecution = await amazonStepFunctions.StartExecutionAsync(request);

      if (responseExecution.HttpStatusCode != HttpStatusCode.OK)
      {
        logger.LogErrorCustom("The Step Function execution failed", null);
        logger.LogErrorCustom(responseExecution, null);
      }
    }
    catch (Exception e)
    {
      logger.LogErrorCustom("There was an error starting the execution", e);
    }
  }
}