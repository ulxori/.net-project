using api.Models;
using MongoDB.Driver;

namespace api.Filters;

public interface IMeasurementFilteringArgResolver
{
   FilterDefinition<Models.Measurement> GetFilterDefinition(MeasurementParameters measurementParameters);
}