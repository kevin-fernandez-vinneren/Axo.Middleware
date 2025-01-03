using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Serialization.SystemTextJson;
using Axo.Functions.FilesTrigger.Serializer;

[assembly:LambdaGlobalProperties(GenerateMain = true)]
[assembly:LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<CustomSerializationContext>))]