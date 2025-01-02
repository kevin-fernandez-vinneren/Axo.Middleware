using Amazon.S3;
using Axo.Shared.FileService.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Axo.Shared.FileService;

public static class FileServiceRegistration
{
  public static IServiceCollection AddFileService(this IServiceCollection services)
  {
    services.AddScoped<IAmazonS3, AmazonS3Client>();
    services.AddTransient<IFileService, Service.FileService>();
    
    return services;
  }
}