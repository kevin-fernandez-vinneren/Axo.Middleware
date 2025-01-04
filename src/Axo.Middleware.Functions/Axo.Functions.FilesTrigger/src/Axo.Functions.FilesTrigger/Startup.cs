using System.Text.Json;
using Amazon.Lambda.Annotations;
using Amazon.StepFunctions;
using Axo.Functions.FilesTrigger.Serializer;
using Logify;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.FilesTrigger;

[LambdaStartup]
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    var jsonSerializerOptions = new JsonSerializerOptions
    {
      TypeInfoResolver = CustomSerializationContext.Default
    };
    
    services.AddLogging(opc =>
    {
      opc.AddLambdaLogger(new LambdaLoggerOptions
      {
        IncludeNewline = false,
        IncludeException = true
      });
    });

    services.AddLogifyService(jsonSerializerOptions);

    services.AddTransient<IAmazonStepFunctions, AmazonStepFunctionsClient>();
  }
}