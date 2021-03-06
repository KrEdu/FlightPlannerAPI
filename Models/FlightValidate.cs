using System;

namespace FlightPlannerAPI.Models
{
    public static class FlightValidate
    {
        public static bool IsFlightViable(Flight flight)
        {
                return AirportValidate.AreAirportsViable(flight.From, flight.To)
                    && FlightHasValidValues(flight)
                    && DateTime.Parse(flight.DepartureTime) < DateTime.Parse(flight.ArrivalTime);
        }

        public static bool AreFlightDuplicates(Flight flight1, Flight flight2)
        {
                return (flight1 == null || flight2 == null) ||
                    flight1.DepartureTime == flight2.DepartureTime &&
                    flight1.ArrivalTime == flight2.ArrivalTime
                    && flight1.Carrier == flight2.Carrier
                    && AirportValidate.AreAirportsDuplicate(flight1.To, flight2.To)
                    && AirportValidate.AreAirportsDuplicate(flight1.From, flight2.From);
        }
        public static bool FlightHasValidValues(Flight flight)
        {
                return !String.IsNullOrEmpty(flight.Carrier) && !String.IsNullOrEmpty(flight.DepartureTime)
                    && !String.IsNullOrEmpty(flight.ArrivalTime);
        }
    }
}
