using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjektSiNet.Models;

namespace ProjektSiNet.Pages
{
    public class ChartModel : PageModel
    {
        public static List<Measurement> AllMeasurements { get; set; }
        public static List<Measurement> Measurements { get; set; }

        [BindProperty]
        public string ChosenType { get; set; }

        [BindProperty]
        public DateTime? ChosenStartDate { get; set; }

        [BindProperty]
        public DateTime? ChosenEndDate { get; set; }

        [BindProperty]
        public int? ChosenInstance { get; set; }

        // for charts

        public static List<string> Labels { get; set; }

        public static List<double> Values { get; set; }

        public static string Title { get; set; }

        public static bool ShowGraph { get; set; }

        public static string Color { get; set; }


        public async Task<IActionResult> OnGet()
        {
            await ConsumeApi();
            ShowGraph = false;
            return Page();
        }

        private async Task<bool> ConsumeApi()
        {
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

        public void OnPostFilterData()
        {
            FilterData();
            

            if (!string.IsNullOrEmpty(ChosenType))
            {
                CreateChartData();
                ShowGraph = true;
            }
            else
            {
                ShowGraph = false;
            }
        }

        private void FilterData()
        {
            Measurements = new List<Measurement>(AllMeasurements);

            if (!string.IsNullOrEmpty(ChosenType))
                Measurements = Measurements.Where(m => m.SensorType == ChosenType).ToList();

            if (ChosenStartDate != null)
                Measurements = Measurements.Where(m => DateTime.Compare(m.Date, (DateTime)ChosenStartDate) >= 0).ToList();

            if (ChosenEndDate != null)
                Measurements = Measurements.Where(m => DateTime.Compare(m.Date, (DateTime)ChosenEndDate) <= 0).ToList();

            if (ChosenInstance != null)
                Measurements = Measurements.Where(m => m.SensorNumber == ChosenInstance).ToList();
        }

        private void CreateChartData()
        {
            Measurements.Sort((x, y) => x.Date.CompareTo(y.Date));

            Labels = new List<string>();
            Values = new List<double>();

            Measurements.ForEach(m => {
                if (!Labels.Contains(m.Date.ToShortDateString()))
                {
                    Labels.Add(m.Date.ToShortDateString());
                    Values.Add(m.Value);
                }
            });

            SetTitle();
        }

        private void SetTitle()
        {
            Title = "";
            if (!string.IsNullOrEmpty(ChosenType))
            {
                if (ChosenType == "ozone")
                {
                    Title = "Pomiar stê¿enia ozonu";
                    Color = "#3e95cd";
                }
                else if (ChosenType == "temperature")
                {
                    Title = "Pomiar temperatury gleby";
                    Color = "#e8c3b9";
                }
                else if (ChosenType == "insolation")
                {
                    Title = "Pomiar nas³onecznienia";
                    Color = "#c45850";
                }
                else
                {
                    Title = "Pomiar wilgotnoœci gleby";
                    Color = "#3cba9f";
                }  
            }

            if (ChosenInstance != null)
            {
                Title += " - czujnik numer ";
                Title += ChosenInstance.ToString();
            }
        }
    }
}
