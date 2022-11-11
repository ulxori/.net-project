using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class Measurement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    private string? Id { get; set; }

    private string Type { get; set; }
    private double Value { get; set; }
}