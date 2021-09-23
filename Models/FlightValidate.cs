using System;

namespace FlightPlannerAPI.Models
{
    public static class FlightValidate
    {
        public static bool IsFlightViable(Flight flight)
        {
            if (!AirportValidate.AreAirportsViable(flight.From,flight.To) ||
               String.IsNullOrEmpty(flight.Carrier) || String.IsNullOrEmpty(flight.DepartureTime) || String.IsNullOrEmpty(flight.ArrivalTime))
            {
                return false;
            }
            else if (DateTime.Parse(flight.DepartureTime) > DateTime.Parse(flight.ArrivalTime) ||
                DateTime.Parse(flight.DepartureTime) == DateTime.Parse(flight.ArrivalTime))
            {
                return false;
            }
            else return true;
        }

        public static bool AreFlightDuplicates(Flight flight1, Flight flight2)
        {
            if (flight1 == null || flight2 == null)
                return false;
            return flight1.DepartureTime == flight2.DepartureTime &&
                flight1.ArrivalTime == flight2.ArrivalTime
                && flight1.Carrier == flight2.Carrier
                && AirportValidate.AreAirportsDuplicate(flight1.To, flight2.To)
                && AirportValidate.AreAirportsDuplicate(flight1.From, flight2.From);
        }
    }
}
