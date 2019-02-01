using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Geocode
{
    /// <summary>
    /// Contains methods for interacting with the Google Maps GeoCordinates API
    /// </summary>
    public class GeocodeClient
    {
        private string _apiKey;
        private readonly string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json?";

        public GeocodeClient(string apiKey = null)
        {
            if (apiKey != null)
                _apiKey = apiKey;
            else 
                Console.WriteLine("[Warning] API key Missing. Your requests may be throttled or limited");  
        }

        //Calls an asynchronous method to make an HTTP GET request
        private GeoObj WebRequest(string arg) => GetRequest(arg).GetAwaiter().GetResult();

        private async Task<GeoObj> GetRequest(string arg)
        {
               /*Google wants a '+' beween arguments in it's API
                * The line below uses a regular expression to replace any spaces with a '+' */
               arg = Regex.Replace(arg, "\\s", "+");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"{BASE_URL}{arg}&key={_apiKey}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode(); //throws an expection if there was an error
        
            var geo = new GeoObj();
            //Deserializes the JSON 
            JsonConvert.PopulateObject(await response.Content.ReadAsStringAsync(), geo);

               if (geo.status == "OK")
                    return geo;
               else //API returned an error message 
                    throw new Exception("Server returned status: " + geo.status);
        }

        /// <summary>
        /// Converts an address object into a set of coordinates
        /// </summary>
        public MapLocation GetMapLocation(Address address)
        {
            return new MapLocation(
                WebRequest($"address={address.Street}+{address.Apt}+{address.City}+{address.Region}+{address.PostalCode}+{address.Country}"));

        }

        /// <summary>
        /// Converts a string address to a set of Coordinates
        /// </summary>
        public MapLocation GetMapLocation(string address)
        { 
            return new MapLocation(
                WebRequest($"address={address}"));
        }

        /// <summary>
        /// Converts Coordinates into an address object
        /// </summary>
        public MapLocation GetMapLocation(float lat,float lng)
        {
            return new MapLocation(
                WebRequest($"latlng={lat},{lng}"));
        }

    }
}
