using System.Text.Json.Serialization;
using Axo.Functions.FileSegmentation.Common;
using Axo.Functions.FileSegmentation.Models;

namespace Axo.Functions.FileSegmentation.Serializer;

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
[JsonSerializable(typeof(List<InventoryModel>))]
[JsonSerializable(typeof(List<PriceModel>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class CustomSerializationContext : JsonSerializerContext;