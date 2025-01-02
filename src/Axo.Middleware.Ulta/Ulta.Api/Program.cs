using Amazon.Lambda.Serialization.SystemTextJson;
using Ulta.Api.Apis;
using Ulta.Infrastructure;
using Ulta.Infrastructure.Serializer;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi,
  new SourceGeneratorLambdaJsonSerializer<ApiSerializationContext>());

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.TypeInfoResolver = new ApiSerializationContext();
});

builder.Services.AddLogging(x =>
{
  x.ClearProviders();

  x.AddLambdaLogger(new LambdaLoggerOptions
  {
    IncludeNewline = false,
    IncludeException = true
  });
});

builder.Services.AddInfrastructureServices();

var app = builder.Build();

app.MapInventoryApi();
app.MapPriceApi();

app.Run();
