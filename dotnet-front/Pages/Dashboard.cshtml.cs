using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektSiNet.Models;

namespace ProjektSiNet.Pages
{
    public class DashboardModel : PageModel
    {
        public static List<Measurement> Measurements { get; set; }

        public void OnGet()
        {
            
        }
    }
}
