using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerAPI.Models
{
    public class FlightRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
