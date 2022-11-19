using api.Models;

namespace api.Repositories;

public interface IMeasurementRepository
{
    Task Add(Measurement measurement);
    Task<List<Measurement>> Get(MeasurementFilteringParameters filteringParameters, MeasurementSortingParameters sortingParameters);
}