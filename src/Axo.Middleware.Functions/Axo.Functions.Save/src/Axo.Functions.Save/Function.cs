using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Text.Json.Serialization;
using Amazon.Lambda.Annotations;
using Axo.Functions.Save.Common;
using Axo.Functions.Save.Services;

namespace Axo.Functions.Save;

public class Function(ISaveService saveService)
{
  [LambdaFunction]
  public async Task FunctionHandler(LambdaInput input, ILambdaContext context)
  {
    await saveService.HandleSaveLogic(input);
  }
}