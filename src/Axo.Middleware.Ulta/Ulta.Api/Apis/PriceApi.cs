using System.Text;
using System.Text.Json;
using Axo.Shared.FileService.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ulta.Domain.Models;
using Ulta.Infrastructure.Serializer;

namespace Ulta.Api.Apis;

public static class PriceApi
{
  public static IEndpointRouteBuilder MapPriceApi(this IEndpointRouteBuilder app)
  {
    var api = app.MapGroup("api/price");

    api.MapPost("/postPrice", PostPrice);

    return app;
  }

  private static async Task<Results<Ok<string>, BadRequest<string>>> PostPrice(
    [FromBody] List<PostPriceModel> model,
    [FromServices] IFileService fileService)
  {
    var listJson = JsonSerializer.Serialize(model, ApiSerializationContext.Default.ListPostPriceModel);

    var byteArray = Encoding.UTF8.GetBytes(listJson);

    var file = fileService.GenerateFile(byteArray);

    var path = Environment.GetEnvironmentVariable("PricePath");

    var fileName = $"{path}/Ulta_Price.json";

    var urlFile = await fileService.SaveFileToS3(file, fileName);

    if (urlFile is null)
    {
      return TypedResults.BadRequest("Error processing the file");
    }

    return TypedResults.Ok("File processed successfully");
  }
}