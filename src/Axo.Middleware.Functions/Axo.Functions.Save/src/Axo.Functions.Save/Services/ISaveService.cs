using Axo.Functions.Save.Common;

namespace Axo.Functions.Save.Services;

public interface ISaveService
{
  Task<bool> HandleSaveLogic(LambdaInput lambdaInput);
}