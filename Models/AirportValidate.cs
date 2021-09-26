using System;

namespace FlightPlannerAPI.Models
{
    public static class AirportValidate
    {
        public static bool AreAirportsViable(Airport airport1, Airport airport2)
        {
            return AirportHasValidValues(airport1) && AirportHasValidValues(airport2) && !AreAirportsDuplicate(airport1, airport2);
        }

        public static bool AreAirportsDuplicate(Airport airport1, Airport airport2)
        {
            return airport1.airport.ToLower().Trim() == airport2.airport.ToLower().Trim();
        }

        public static bool AirportHasValidValues(Airport airport)
        {
            return (airport != null)
                && !String.IsNullOrEmpty(airport.airport)
                && !String.IsNullOrEmpty(airport.city)
                && !String.IsNullOrEmpty(airport.country);
        }

    }
}