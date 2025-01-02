using Axo.Functions.FileSegmentation.Common;

namespace Axo.Functions.FileSegmentation.Services;

public interface IFileSegmentationService
{
  Task<List<string>> HandleFileSegmentation(LambdaInput lambdaInput);
}