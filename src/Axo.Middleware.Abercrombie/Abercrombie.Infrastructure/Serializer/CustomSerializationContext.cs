using System.Text.Json.Serialization;
using Abercrombie.Domain.Common;
using Abercrombie.Domain.Entities;
using Abercrombie.Domain.Models;

namespace Abercrombie.Infrastructure.Serializer;

[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(char))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(byte))]
[JsonSerializable(typeof(uint))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(sbyte))]
[JsonSerializable(typeof(short))]
[JsonSerializable(typeof(Array))]
[JsonSerializable(typeof(ulong))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(ushort))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(LambdaInput))]
[JsonSerializable(typeof(List<InventoryItem>))]
[JsonSerializable(typeof(CreationFileModel<PriceModel>))]
[JsonSerializable(typeof(CreationFileModel<InventoryModel>))]
[JsonSerializable(typeof(KnGetInventoryRequest))]
[JsonSerializable(typeof(KnInventoryResponseModel))]
[JsonSerializable(typeof(IEnumerable<PriceEcommerce>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class CustomSerializationContext : JsonSerializerContext;