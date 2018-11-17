using System;
using System.Collections.Generic;
using System.Text;

namespace Geocode
{
    public class MapLocation : Address
    {
        public float latitude { get; }
        public float longitude { get; }

        internal MapLocation(GeoObj objs) : base(objs)
        {
            latitude = objs.results[0].geometry.location.lat;
            longitude = objs.results[0].geometry.location.lng;
        }
    }

}
