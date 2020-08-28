using Xamarin.Forms;

namespace SmartReport.DataService
{
    public static class StaticHandler
    {
        public static string Token { get; set; }

        public static string Email { get; set; }

        public static INavigation Navigation { get; set; }
    }
}
