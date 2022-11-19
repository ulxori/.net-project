namespace api.Models;

public class MeasurementParameters
{
    public SensorType? SensorType { get; set; }
    public int? SensorNumber { get; set; }
    public DateTime? StartDate { get; set;  }
    public DateTime? EndDate { get; set; }
    public SortType? SortType { get; set; }
    public bool? IsAscending { get; set; }
}