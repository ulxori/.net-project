using System.Text.Json.Serialization;

namespace api.Models;

public class MeasurementSortingParameters
{
    public MeasurementSortType? SortType { get; set; }
    public bool? IsAscending { get; set; }
}