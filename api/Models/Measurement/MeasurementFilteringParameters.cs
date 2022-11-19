namespace api.Models;

public class MeasurementFilteringParameters
{
    public SensorType? SensorType { get; set; }
    public int? SensorNumber { get; set; }
    public DateTime? StartDate { get; set;  }
    public DateTime? EndDate { get; set; }
}