using System.Text.Json;
using Abercrombie.Application.Contracts.Repositories;
using Abercrombie.Application.Contracts.Services;
using Abercrombie.Infrastructure.Repositories;
using Abercrombie.Infrastructure.Serializer;
using Abercrombie.Infrastructure.Services;
using Axo.Shared.FileService;
using Axo.Shared.SecretsManagerService;
using Logify;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Abercrombie.Infrastructure;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
  {
    var jsonSerializerOptions = new JsonSerializerOptions
    {
      TypeInfoResolver = CustomSerializationContext.Default
    };

    services.AddLogifyService(jsonSerializerOptions);
    services.AddFileService();
    services.AddSecretsManagerService(jsonSerializerOptions);

    services.AddTransient<IPriceRepository, PriceRepository>();
    services.AddScoped<IAbercrombieService, AbercrombieService>();

    services.AddLogging(opc =>
    {
      opc.AddLambdaLogger(new LambdaLoggerOptions
      {
        IncludeNewline = false,
        IncludeException = true
      });
    });

    return services;
  }
}