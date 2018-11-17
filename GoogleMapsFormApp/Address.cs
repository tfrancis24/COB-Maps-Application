using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Geocode
{
    public class Address
    {
        public string Street { get; set; } = "";
        public string Apt { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string getFormattedAddress() { return $"{Street}, {Apt}, {City}, {Region}, {PostalCode}, {Country}"; }

        public Address() { }

        internal Address(GeoObj obj)
        {
            Street = obj.results[0].address_components
                .Where(x => x.types[0] == "street_number")
                .DefaultIfEmpty(new Address_Components() { long_name = null })
                .SingleOrDefault().long_name;

            Street += " " + obj.results[0].address_components
                .Where(x => x.types[0] == "route")
                .DefaultIfEmpty(new Address_Components() { long_name = null })
                .SingleOrDefault().long_name;

            Apt = obj.results[0].address_components
               .Where(x => x.types[0] == "premise")
               .DefaultIfEmpty(new Address_Components() { long_name = null })
               .SingleOrDefault().long_name;

            City = obj.results[0].address_components
              .Where(x => x.types[0] == "locality")
              .DefaultIfEmpty(new Address_Components() { long_name = null })
              .SingleOrDefault().long_name;

            Region = obj.results[0].address_components
               .Where(x => x.types[0] == "administrative_area_level_1")
               .DefaultIfEmpty(new Address_Components() { long_name = null })
               .SingleOrDefault().long_name;

            PostalCode = obj.results[0].address_components
                .Where(x => x.types[0] == "postal_code")
                .DefaultIfEmpty(new Address_Components() { long_name = null })
                .SingleOrDefault().long_name;

            Country = obj.results[0].address_components
               .Where(x => x.types[0] == "country")
               .DefaultIfEmpty(new Address_Components() { long_name = null })
               .SingleOrDefault().long_name;
        }
    }

}



