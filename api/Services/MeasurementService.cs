using api.Repositories;

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
}