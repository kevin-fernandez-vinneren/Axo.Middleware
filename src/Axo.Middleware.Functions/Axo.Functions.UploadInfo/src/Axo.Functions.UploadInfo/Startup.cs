using System.Text.Json;
using Amazon.Lambda.Annotations;
using Axo.Functions.UploadInfo.Services;
using Axo.Shared.FileService;
using Logify;
using Microsoft.Extensions.DependencyInjection;
using CustomSerializationContext = Axo.Functions.UploadInfo.Serializer.CustomSerializationContext;

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

    services.AddTransient<IUploadInfoService, UploadInfoService>();
  }
}