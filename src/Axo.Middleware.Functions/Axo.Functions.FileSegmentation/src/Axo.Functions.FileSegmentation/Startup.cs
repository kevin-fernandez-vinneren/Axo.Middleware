using System.Text.Json;
using Amazon.Lambda.Annotations;
using Axo.Functions.FileSegmentation.Serializer;
using Axo.Functions.FileSegmentation.Services;
using Axo.Shared.FileService;
using Logify;
using Microsoft.Extensions.DependencyInjection;

namespace Axo.Functions.FileSegmentation;

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
    
    services.AddTransient<IFileSegmentationService, FileSegmentationService>();
  }
}