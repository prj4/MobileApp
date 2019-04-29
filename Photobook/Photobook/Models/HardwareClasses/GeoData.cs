using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Photobook.Models
{
    internal class GeoData
    {
        public static async Task<string[]> GetCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync();
            string[] data;
            if (location != null)
                data = new[] {location.Latitude.ToString(), location.Longitude.ToString()};
            else
                data = new[] {"null", "null"};

            return data;
        }
    }
}