using System.Text.Json;
using Amazon.SecretsManager.Extensions.Caching;

namespace Axo.Shared.SecretsManagerService.Service;

public class SecretsManagerService(ISecretsManagerCache secretsManagerCache) : ISecretsManagerService
{
  private static JsonSerializerOptions _jsonSerializerOptions = new();
  
  internal static void Configure(JsonSerializerOptions options)
  {
    _jsonSerializerOptions = options;
  }
  
  public async Task<string?> GetSecrets(string key)
  {
    var secretId = Environment.GetEnvironmentVariable("NadroSecretId");

    var response = await secretsManagerCache.GetSecretString(secretId);

    var secrets = JsonSerializer.Deserialize<Dictionary<string, string>>(response, _jsonSerializerOptions);

    var secret = secrets?[key];

    return secret;
  }
}