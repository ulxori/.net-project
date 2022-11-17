using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class Measurement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    private string? Id { get; set; }
    public SensorType sensorType { get; set; }
    public int sensorNumber { get; set; } 
    public double value { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)] 
    public DateTime date { get; set; }
}