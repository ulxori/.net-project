using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly ILogger<MeasurementRepository> _logger;
    private readonly IMongoCollection<Measurement> _measurementCollection;

    public MeasurementRepository(ILogger<MeasurementRepository> logger,
        MongoSettings mongoSettings)
    {
        _logger = logger;
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        _measurementCollection = mongoDatabase.GetCollection<Measurement>(mongoSettings.CollectionName);
    }

    public async Task Add(Measurement measurement)
    {
        await _measurementCollection.InsertOneAsync(measurement);
    }

    public async Task<List<Measurement>> Get()
    {
        return await _measurementCollection.Find(_ => true).ToListAsync();
    }
}