using System.Text.Json;
using Amazon.Lambda.Annotations;
using Axo.Functions.Save.Repositories;
using Axo.Functions.Save.Serializer;
using Axo.Functions.Save.Services;
using Axo.Shared.FileService;
using Logify;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axo.Functions.Save;

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
    services.AddFileService();

    services.AddTransient<IInventoryRepository, InventoryRepository>();
    services.AddTransient<IPriceRepository, PriceRepository>();
    services.AddScoped<ISaveService, SaveService>();
  }
}