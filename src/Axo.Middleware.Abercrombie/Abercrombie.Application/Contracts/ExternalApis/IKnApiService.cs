using Abercrombie.Domain.Models;
using Refit;

namespace Abercrombie.Application.Contracts.ExternalApis;

public interface IKnApiService
{
  [Post("/devcic/inventariosnap")]
  Task<IApiResponse<KnInventoryResponseModel>> GetInventory([Header("authorizationToken")] string authorizationToken, [Body] KnGetInventoryRequest request);
}