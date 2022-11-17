using api.Models;

namespace api.Repositories;

public interface IMeasurementRepository
{
    Task Add(Measurement measurement);
    Task<List<Measurement>> Get();
}