using System.Text.Json.Serialization;
using Ulta.Domain.Models;

namespace Ulta.Infrastructure.Serializer;

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
[JsonSerializable(typeof(List<PostInventoryModel>))]
[JsonSerializable(typeof(List<PostPriceModel>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class ApiSerializationContext : JsonSerializerContext;