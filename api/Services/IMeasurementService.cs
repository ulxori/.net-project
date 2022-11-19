using api.Models;

namespace api.Services;

public interface IMeasurementService
{
    Task Add(Measurement measurement);
    Task<List<Measurement>> Get(MeasurementParameters parameters);
}