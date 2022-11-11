using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private IMeasurementService _measurementService;
    private readonly ILogger<MeasurementController> _logger;

    public MeasurementController(ILogger<MeasurementController> logger, IMeasurementService measurementService)
    {
        _logger = logger;
        _measurementService = measurementService;
    }

    [HttpGet(Name = "measurement")]
    public string Get()
    {
        return "temp";
    }
}
