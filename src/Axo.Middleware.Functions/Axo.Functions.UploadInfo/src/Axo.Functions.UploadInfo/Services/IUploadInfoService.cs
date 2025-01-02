using Axo.Functions.UploadInfo.Common;

namespace Axo.Functions.UploadInfo.Services;

public interface IUploadInfoService
{
  Task<bool> HandleUploadInfo(LambdaInput lambdaInput);
}