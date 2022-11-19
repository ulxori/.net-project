using api.Models;

namespace api.Services;

public interface IMeasurementService
{
    Task Add(Measurement measurement);
    Task<List<Measurement>> Get(MeasurementFilteringParameters filteringParameters,  
        MeasurementSortingParameters sortingParameters);
}