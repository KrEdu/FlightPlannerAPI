using System;

namespace FlightPlannerAPI.Models
{
    public static class AirportValidate
    {
        public static bool AreAirportsViable(Airport airport1, Airport airport2)
        {
            if (airport1 == null || airport2 == null ||
                String.IsNullOrEmpty(airport1.airport) || String.IsNullOrEmpty(airport2.airport) ||
               String.IsNullOrEmpty(airport1.airport) || String.IsNullOrEmpty(airport2.airport) ||
                String.IsNullOrEmpty(airport1.airport) || String.IsNullOrEmpty(airport2.airport))
            {
                return false;
            }
            else if (airport1.airport.ToLower().Trim() == airport2.airport.ToLower().Trim())
            {
                return false;
            }
            else return true;
        }

        public static bool AreAirportsDuplicate(Airport airport1, Airport airport2)
        {
            return airport1.airport.ToLower().Trim() == airport2.airport.ToLower().Trim();
        }
    }
}
