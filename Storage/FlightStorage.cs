using System.Collections.Generic;
using System.Linq;


namespace FlightPlannerAPI.Models
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight> ();
        private static object myLock = new object();

        public static Flight GetFlightById(int id)
        {
            lock (myLock)
            {
                return _flights.SingleOrDefault(flight => flight.Id == id);
            }
        }

        public static Airport[] GetAirportByPhrase(string phrase)
        {
            lock (myLock)
            {
                List<Airport> airportsList = new List<Airport> { };
                foreach (Flight f in _flights)
                {
                    if (f.To.Contains(phrase) && !airportsList.Any(airport => airport == f.To))
                        airportsList.Add(f.To);
                    else if (f.From.Contains(phrase) && !airportsList.Any(airport => airport == f.From))
                        airportsList.Add(f.From);
                }
                Airport[] airports = new Airport[airportsList.Count];
                airportsList.CopyTo(airports);
                return airports;
            }
        }

        public static PageResult GetFlightListByRequest(FlightRequest request)
        {
            lock (myLock)
            {
                List<Flight> flights = new List<Flight> { };
                foreach (Flight f in _flights)
                {
                    if (f.From.airport == request.From && f.To.airport == request.To && f.DepartureTime.Contains(request.DepartureDate))
                    {
                        flights.Add(f);
                    }
                }
                Flight[] flightArray = new Flight[flights.Count];
                flights.CopyTo(flightArray);
                return new PageResult(0, flights.Count, flightArray);
            }
        }

        public static void ClearFlights()
        {
            lock (myLock)
            {
                _flights.Clear();
            }
        }

        public static void AddFlight(Flight newFlight)
        {
            lock (myLock)
            {
                newFlight.Id = _flights.Count;
                _flights.Add(newFlight);
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (myLock)
            {
                _flights.RemoveAll(f => f.Id == id);
                for (int i = 0; i < _flights.Count; i++)
                {
                    _flights[i].Id = i;
                }
            }
        }

        public static bool IsFlightDuplicate(Flight flight)
        {
            lock (myLock)
            {
                if (!_flights.Any())
                    return false;
                else 
                    return _flights.Any(f => FlightValidate.AreFlightDuplicates(f, flight));
            }

        }
    }
}
