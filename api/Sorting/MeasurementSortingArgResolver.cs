using api.Models;
using MongoDB.Driver;

namespace api.Filters.Measurement;

public class MeasurementSortingArgResolver : IMeasurementSortingArgResolver
{
    public SortDefinition<Models.Measurement> GetSortDefinition(MeasurementSortingParameters parameters)
    {
        var builder = Builders<Models.Measurement>.Sort;
        var sort = builder.Combine(); 
        if (parameters.IsAscending.HasValue && parameters.SortType.HasValue)
        {
            Console.WriteLine("Default sorting enabled");
            if (parameters.IsAscending.Value)
            {
                sort = builder.Ascending(parameters.SortType.ToString());
                Console.WriteLine("sorting enabled");
                //sort = builder.Ascending("sensorNumber");
            }
            else
            {
                sort = builder.Descending(parameters.SortType.ToString());
            }
        }

        return sort;
    }
}