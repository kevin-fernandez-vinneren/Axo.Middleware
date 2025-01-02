using System.Text;
using System.Text.Json;
using Abercrombie.Domain.Models;
using Axo.Shared.FileService.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ulta.Domain.Models;
using Ulta.Infrastructure.Serializer;

namespace Ulta.Api.Apis;

public static class InventoryApi
{
  public static IEndpointRouteBuilder MapInventoryApi(this IEndpointRouteBuilder app)
  {
    var api = app.MapGroup("api/inventory");

    api.MapPost("/postInventory", PostInventory);

    return app;
  }

  private static async Task<Results<Ok<string>, BadRequest<string>>> PostInventory(
    [FromBody] List<PostInventoryModel> model,
    [FromServices] IFileService fileService)
  {
    var obj = new CreationFileModel<InventoryModel>
    {
      MerchantId = model[0].MerchantId,
      ListInfo = model.Select(x => new InventoryModel
      {
        Sku = x.Upc,
        Qty = x.Stock,
        IsInStock = x.Stock > 0 ? 1 : 0
      }).ToList()
    };
    
    var listJson = JsonSerializer.Serialize(obj, ApiSerializationContext.Default.CreationFileModelInventoryModel);

    var byteArray = Encoding.UTF8.GetBytes(listJson);

    var file = fileService.GenerateFile(byteArray);

    var path = Environment.GetEnvironmentVariable("InventoryPath");

    var fileName = $"{path}/Ulta_Inventory.json";

    var urlFile = await fileService.SaveFileToS3(file, fileName);

    if (urlFile is null)
    {
      return TypedResults.BadRequest("Error processing the file");
    }

    return TypedResults.Ok("File processed successfully");
  }
}