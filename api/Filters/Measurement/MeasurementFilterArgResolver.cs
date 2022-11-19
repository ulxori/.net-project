using api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Filters.Measurement;

public class MeasurementFilterArgResolver : IMeasurementFilteringArgResolver
{
    
    public FilterDefinition<Models.Measurement> GetFilterDefinition(MeasurementParameters measurementParameters)
    {
        var builder = Builders<Models.Measurement>.Filter;
        var filter = builder.Empty;
        
        if (measurementParameters.SensorNumber.HasValue)
        {
            var sensorNumberFilter = builder.Eq(x => x.sensorNumber, measurementParameters.SensorNumber);
            filter &= sensorNumberFilter;
        }

        if (measurementParameters.SensorType.HasValue)
        {
            var sensorTypeFilter = builder.Eq(x => x.sensorType, measurementParameters.SensorType);
            filter &= sensorTypeFilter;
        }

        if (measurementParameters.StartDate.HasValue)
        {
            var startDateFilter = builder.Gte(x => x.date, measurementParameters.StartDate);
            filter &= startDateFilter;
        }

        if (measurementParameters.EndDate.HasValue)
        {
            var endDateFilter = builder.Lte(x => x.date, measurementParameters.EndDate);
            filter &= endDateFilter;
        }
        
        return filter;
    }
}