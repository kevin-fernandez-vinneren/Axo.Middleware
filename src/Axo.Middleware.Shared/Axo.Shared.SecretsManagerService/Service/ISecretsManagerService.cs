namespace Axo.Shared.SecretsManagerService.Service;

public interface ISecretsManagerService
{
  Task<string?> GetSecrets(string key);
}