using System.Text.Json;
using Amazon.Lambda.Annotations;
using Axo.Functions.Save.Repositories;
using Axo.Functions.Save.Serializer;
using Axo.Functions.Save.Services;
using Axo.Shared.FileService;
using Logify;
using Microsoft.Extensions.DependencyInjection;

namespace Axo.Functions.UploadInfo;

[LambdaStartup]
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    var jsonSerializerOptions = new JsonSerializerOptions
    {
      TypeInfoResolver = CustomSerializationContext.Default
    };

    services.AddLogifyService(jsonSerializerOptions);
    services.AddFileService();

    services.AddTransient<IInventoryRepository, InventoryRepository>();
    services.AddTransient<IPriceRepository, PriceRepository>();
    services.AddScoped<ISaveService, SaveService>();
  }
}