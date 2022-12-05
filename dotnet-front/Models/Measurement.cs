namespace ProjektSiNet.Models
{
    public class Measurement
    {
        public string SensorType { get; set; }

        public int SensorNumber { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }

        public Measurement(string sensorType, int sensorNumber, DateTime date, double value)
        {
            SensorType = sensorType;
            SensorNumber = sensorNumber;
            Date = date;
            Value = value;
        }
    }
}
