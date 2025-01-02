using System.Text.Json;
using Amazon.SecretsManager.Extensions.Caching;
using Axo.Shared.SecretsManagerService.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Axo.Shared.SecretsManagerService;

public static class SecretsManagerRegistration
{
  public static IServiceCollection AddSecretsManagerService(this IServiceCollection services, JsonSerializerOptions jsonSerializerOptions = null)
  {
    services.AddScoped<ISecretsManagerCache, SecretsManagerCache>();

    services.AddTransient<ISecretsManagerService, Service.SecretsManagerService>();
    
    if (jsonSerializerOptions is not null)
    {
      Service.SecretsManagerService.Configure(jsonSerializerOptions);
    }
      
    return services;
  }
}