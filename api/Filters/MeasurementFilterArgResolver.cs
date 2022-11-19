using api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Filters.Measurement;

public class MeasurementFilterArgResolver : IMeasurementFilteringArgResolver
{
    
    public FilterDefinition<Models.Measurement> GetFilterDefinition(MeasurementFilteringParameters measurementFilteringParameters)
    {
        var builder = Builders<Models.Measurement>.Filter;
        var filter = builder.Empty;
        
        if (measurementFilteringParameters.SensorNumber.HasValue)
        {
            var sensorNumberFilter = builder.Eq(x => x.sensorNumber, measurementFilteringParameters.SensorNumber);
            filter &= sensorNumberFilter;
        }

        if (measurementFilteringParameters.SensorType.HasValue)
        {
            var sensorTypeFilter = builder.Eq(x => x.sensorType, measurementFilteringParameters.SensorType);
            filter &= sensorTypeFilter;
        }

        if (measurementFilteringParameters.StartDate.HasValue)
        {
            var startDateFilter = builder.Gte(x => x.date, measurementFilteringParameters.StartDate);
            filter &= startDateFilter;
        }

        if (measurementFilteringParameters.EndDate.HasValue)
        {
            var endDateFilter = builder.Lte(x => x.date, measurementFilteringParameters.EndDate);
            filter &= endDateFilter;
        }
        
        return filter;
    }
}