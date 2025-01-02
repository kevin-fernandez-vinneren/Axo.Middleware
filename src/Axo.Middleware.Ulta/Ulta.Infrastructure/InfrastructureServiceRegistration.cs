using System.Text.Json;
using Logify;
using Microsoft.Extensions.DependencyInjection;
using Axo.Shared.FileService;
using Ulta.Infrastructure.Serializer;

namespace Ulta.Infrastructure;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    var jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
      TypeInfoResolver = ApiSerializationContext.Default,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    services.AddLogifyService(jsonSerializerOptions);
    services.AddFileService();

    return services;
  }
}