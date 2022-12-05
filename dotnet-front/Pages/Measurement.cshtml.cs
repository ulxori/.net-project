using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSiNet.Models;
using Newtonsoft.Json;
using System.Text;

namespace ProjektSiNet.Pages
{
    public class MeasurementModel : PageModel
    {
        public static List<Measurement> AllMeasurements { get; set; }
        public static List<Measurement> Measurements { get; set; }

        public static string CsvData { get; set; }

        public static string JsonData { get; set; }

        [BindProperty]
        public string ChosenType { get; set; }

        [BindProperty]
        public DateTime? ChosenStartDate { get; set; }

        [BindProperty]
        public DateTime? ChosenEndDate { get; set; }

        [BindProperty]
        public int? ChosenInstance { get; set; }

        public static string SensorType { get; set; }

        public static string StartDate { get; set; }

        public static string EndDate { get; set; }

        public static int SensorNumber { get; set; }

        public static string SortType { get; set; }

        public static string Order { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await ConsumeApi();

            SortType = "Type";
            Order = "None";

            return Page();
        }

        private async Task<bool> ConsumeApi() {                
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Config.Config.BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("measurement");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    AllMeasurements = JsonConvert.DeserializeObject<List<Measurement>>(results);
                    Measurements = new List<Measurement>(AllMeasurements);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private async Task<bool> ConsumeData(string type)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Config.Config.BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(type));

                var t = ChosenType;
                var q = ChosenInstance;

                HttpResponseMessage getData = await client.GetAsync("measurement" + GetQueryParams());

                if (getData.IsSuccessStatusCode)
                {
                    if (type == "text/csv")
                        CsvData = getData.Content.ReadAsStringAsync().Result;
                    else
                        JsonData = getData.Content.ReadAsStringAsync().Result;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string GetQueryParams()
        {
            string parameters = GetFilterQuery();
            parameters += GetSortQuery();

            return parameters;
        }

        private string GetFilterQuery()
        {
            string parameters = "?";

            if (!string.IsNullOrEmpty(SensorType))
                parameters += ("&SensorType=" + SensorType);

            if (!string.IsNullOrEmpty(StartDate))
                parameters += ("&StartDate=" + StartDate);

            if (!string.IsNullOrEmpty(EndDate))
                parameters += ("&EndDate=" + EndDate);

            if (SensorNumber > 0)
                parameters += ("&SensorNumber=" + SensorNumber);

            return parameters;
        }

        private string GetSortQuery()
        {
            string parameters = "";

            if (Order != "None")
            {
                parameters += "&SortType=" + SortType;
                parameters += Order == "Asc" ? "&IsAscending=true" : "&IsAscending=false";
            }

            return parameters;
        }

        public async Task<FileResult> OnGetDownloadCsv()
        {
            var isSuccess = await ConsumeData("text/csv");

            string fileName = "data.csv";
            byte[] byteArray = isSuccess ? Encoding.UTF8.GetBytes(CsvData) : Array.Empty<byte>();

            return File(byteArray, "application/octet-stream", fileName);
        }

        public async Task<FileResult> OnGetDownloadJson()
        {
            var isSuccess = await ConsumeData("application/json");

            string fileName = "data.json";
            byte[] byteArray = isSuccess ? Encoding.UTF8.GetBytes(JsonData) : Array.Empty<byte>();

            return File(byteArray, "application/octet-stream", fileName);
        }

        public void OnPostFilterData()
        {
            FilterData();
            SortData();
        }

        private void FilterData()
        {
            Measurements = new List<Measurement>(AllMeasurements);

            if (!string.IsNullOrEmpty(ChosenType))
            {
                Measurements = Measurements.Where(m => m.SensorType == ChosenType).ToList();
                SensorType = ChosenType;
            }      

            if (ChosenStartDate != null)
            {
                Measurements = Measurements.Where(m => DateTime.Compare(m.Date, (DateTime)ChosenStartDate) >= 0).ToList();
                StartDate = ((DateTime)ChosenStartDate).ToString("yyyy-MM-dd");
            }  

            if (ChosenEndDate != null)
            {
                Measurements = Measurements.Where(m => DateTime.Compare(m.Date, (DateTime)ChosenEndDate) <= 0).ToList();
                EndDate = ((DateTime)ChosenEndDate).ToString("yyyy-MM-dd");
            }
                
            if (ChosenInstance != null)
            {
                Measurements = Measurements.Where(m => m.SensorNumber == ChosenInstance).ToList();
                SensorNumber = (int)ChosenInstance;
            }  

            SortData();
        }

        public void OnPostSortByType()
        {
            SortType = "sensorType";
            SetOrder();
            FilterData();
        }

        public void OnPostSortBySensor()
        {
            SortType = "sensorNumber";
            SetOrder();
            FilterData();
        }

        public void OnPostSortByDate()
        {
            SortType = "date";
            SetOrder();
            FilterData();
        }

        public void OnPostSortByValue()
        {
            SortType = "value";
            SetOrder();
            FilterData();
        }

        private void SetOrder()
        {
            if (Order == "None")
                Order = "Asc";
            else if (Order == "Asc")
                Order = "Desc";
            else
                Order = "None";
        }

        private void SortData()
        {
            if (Order != "None")
            {
                int multiplier = Order == "Asc" ? 1 : -1;

                if (SortType == "sensorType")
                    Measurements.Sort((x, y) => multiplier * x.SensorType.CompareTo(y.SensorType));
                else if (SortType == "sensorNumber")
                    Measurements.Sort((x, y) => multiplier * x.SensorNumber.CompareTo(y.SensorNumber));
                else if (SortType == "date")
                    Measurements.Sort((x, y) => multiplier * x.Date.CompareTo(y.Date));
                else
                    Measurements.Sort((x, y) => multiplier * x.Value.CompareTo(y.Value));
            }
        }        
    }
}
