﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Photobook.Models
{
    class GeoData
    {
        public static async Task<string[]> GetCurrentLocation()
        {
            var location = await Geolocation.GetLocationAsync();
            string[] data;
            if (location != null)
            {
                data = new string[] {location.Latitude.ToString("F1"), location.Longitude.ToString("F1")};
            }
            else
            {
                data = new string[] {"null", "null"};
            }

            return data;
        }
    }
}
