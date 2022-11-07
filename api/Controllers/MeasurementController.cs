using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private IMeasurementRepository _measurementRepository;
    private readonly ILogger<MeasurementController> _logger;

    public MeasurementController(ILogger<MeasurementController> logger, IMeasurementRepository measurementRepository)
    {
        _logger = logger;
        _measurementRepository = measurementRepository;
    }

    [HttpGet(Name = "measurement")]
    public string Get()
    {
        return _measurementRepository.Get();
    }
}
