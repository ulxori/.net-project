using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
    public async Task<List<Measurement>> Get([FromQuery] MeasurementFilteringParameters filteringFilteringParameters,
        [FromQuery] MeasurementSortingParameters sortingParameters)
    {

        Console.WriteLine(filteringFilteringParameters.EndDate.HasValue);
        return await _measurementService.Get(filteringFilteringParameters, sortingParameters);
    }
    
    /*[HttpGet(Name = "d")]
    public async Task<FileContentResult> DownloadCSV()
    {
        return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Report123.csv");
    }*/
    
}
