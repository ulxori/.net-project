using api.Models;
using MongoDB.Driver;

namespace api.Filters.Measurement;

public interface IMeasurementSortingArgResolver
{
    SortDefinition<Models.Measurement> GetSortDefinition(MeasurementSortingParameters parameters);
}