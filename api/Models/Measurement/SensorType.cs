using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SensorType
{
    [BsonRepresentation(BsonType.String)]
    ozone,
    [BsonRepresentation(BsonType.String)]
    insolation,
    [BsonRepresentation(BsonType.String)]
    temperature,
    [BsonRepresentation(BsonType.String)]
    moisture
}