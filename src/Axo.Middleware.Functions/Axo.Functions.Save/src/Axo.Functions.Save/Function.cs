using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Text.Json.Serialization;
using Amazon.Lambda.Annotations;

namespace Axo.Functions.Save;

public class Function
{
  [LambdaFunction]
  public string FunctionHandler(string input, ILambdaContext context)
  {
    return input.ToUpper();
  }
}