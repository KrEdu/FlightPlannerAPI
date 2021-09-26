using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightPlannerAPI.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string airport { get; set; }
        
        public bool Contains( string phrase)
        {
           return country.ToLower().Contains(phrase.ToLower().Trim()) 
                || city.ToLower().Contains(phrase.ToLower().Trim()) 
                || airport.ToLower().Contains(phrase.ToLower().Trim());
        }
    }
}
