namespace api.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private string _measurement = "Not recorded";
    
    public string Get()
    {
        return _measurement;
    }

    public void Add(string measurement)
    {
        _measurement = measurement;
    }
}