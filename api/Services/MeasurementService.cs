using api.Models;
using api.Repositories;
using MongoDB.Driver;

namespace api.Services;

public class MeasurementService : IMeasurementService
{
    private readonly ILogger<MeasurementService> _logger;
    private IMeasurementRepository _measurementRepository;

    public MeasurementService(ILogger<MeasurementService> logger, IMeasurementRepository measurementRepository)
    {
        _logger = logger;
        _measurementRepository = measurementRepository;
    }

    public async Task Add(Measurement measurement)
    {
        await _measurementRepository.Add(measurement);
    }

    public async Task<List<Measurement>> Get(MeasurementParameters parameters)
    {
        return await _measurementRepository.Get(parameters);
    }
    
}