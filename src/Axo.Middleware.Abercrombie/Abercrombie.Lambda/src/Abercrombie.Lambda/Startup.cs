using Abercrombie.Infrastructure;
using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Abercrombie.Lambda;

[LambdaStartup]
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddInfrastructureServices();
  }
}