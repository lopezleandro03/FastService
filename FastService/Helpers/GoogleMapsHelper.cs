using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Helpers
{
    public class GoogleMapsHelper
    {
        public string ApiKey { get; set; }
        public string EmbebMapsDirectioApinUrl { get; set; }
        public string AutocompleteApinUrl { get; set; }

        public GoogleMapsHelper()
        {
            ApiKey = "AIzaSyAF44JdvJ96J5WyhIwxFRc646TTjhQ2dIY";
            EmbebMapsDirectioApinUrl = "https://www.google.com/maps/embed/v1/directions";
        }

        public string GetDirectionApiUrl(string origin, string destination, string waypoints)
        {
            if (string.IsNullOrEmpty(waypoints))
                return $"{EmbebMapsDirectioApinUrl}?key={ApiKey}&origin={origin}&destination={destination}";

            return $"{EmbebMapsDirectioApinUrl}?key={ApiKey}&origin={origin}&waypoints={waypoints}&destination={destination}";
        }
    }
}