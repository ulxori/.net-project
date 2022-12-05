namespace ProjektSiNet.Config
{
    public static class Config
    {
        public static string BASE_URL =  Environment.GetEnvironmentVariable("BASE_URL") ?? "http://api2:80"; 
    }
}
