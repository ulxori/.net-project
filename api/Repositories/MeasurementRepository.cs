using api.Filters;
using api.Filters.Measurement;
using api.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly ILogger<MeasurementRepository> _logger;
    private readonly IMongoCollection<Measurement> _measurementCollection;
    private IMeasurementFilteringArgResolver _filteringArgResolver;
    private IMeasurementSortingArgResolver _sortingArgResolver;

    public MeasurementRepository(ILogger<MeasurementRepository> logger, MongoSettings mongoSettings, 
        IMeasurementFilteringArgResolver filteringResolver, IMeasurementSortingArgResolver sortingResolver)
    {
        _logger = logger;
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        _measurementCollection = mongoDatabase.GetCollection<Measurement>(mongoSettings.CollectionName);
        _filteringArgResolver = filteringResolver;
        _sortingArgResolver = sortingResolver;
    }

    public async Task Add(Measurement measurement)
    {
        await _measurementCollection.InsertOneAsync(measurement);
    }

    public async Task<List<Measurement>> Get(MeasurementFilteringParameters filteringParameters, MeasurementSortingParameters sortingParameters)
    {
        return await _measurementCollection.Find(_filteringArgResolver.GetFilterDefinition(filteringParameters))
            .Sort(_sortingArgResolver.GetSortDefinition(sortingParameters))
            .ToListAsync();
    }
}