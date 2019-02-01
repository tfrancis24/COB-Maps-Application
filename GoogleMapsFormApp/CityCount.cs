using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsFormApp
{
    //Object that holds two geocoding parameters and a count
    public class CityCount
    {
        public string City { get; set; }
        public string State { get; set; }
        public int Count { get; set; }
    }
}
