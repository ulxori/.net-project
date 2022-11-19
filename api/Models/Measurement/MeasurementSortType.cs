using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeasurementSortType
{
    [BsonRepresentation(BsonType.String)]
    date,
    [BsonRepresentation(BsonType.String)]
    value,
    [BsonRepresentation(BsonType.String)]
    sensorNumber,
}