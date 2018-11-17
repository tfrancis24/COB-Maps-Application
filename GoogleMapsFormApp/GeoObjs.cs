using System;
using System.Collections.Generic;
using System.Text;

namespace Geocode
{

    internal class GeoObj
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }

    internal class Result
    {
        public Address_Components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    internal class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    internal class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    internal class Northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    internal class Southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    internal class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    internal class Viewport
    {
        public Northeast1 northeast { get; set; }
        public Southwest1 southwest { get; set; }
    }

    internal class Northeast1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    internal class Southwest1
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    internal class Address_Components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }

}
