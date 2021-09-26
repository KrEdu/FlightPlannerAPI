using FlightPlannerAPI.DbContext;
using FlightPlannerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FlightPlannerAPI.Controllers
{
    [AllowAnonymous]
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("airports")]

        public IActionResult FindAirport(string search)
        {
            var airportList = _context.Airports.ToList().Where(airport => airport.Contains(search));
            if (airportList == null)
                return NotFound();
            return Ok(airportList);
        }

        [HttpPost]
        [Route("flights/search")]

        public IActionResult FindFlight(FlightRequest request)
        {
            if (!String.IsNullOrEmpty(request.From) && !String.IsNullOrEmpty(request.To) 
                && !String.IsNullOrEmpty(request.DepartureDate)
                && request.From != request.To)
            {
                var result = _context.Flights.Include(f => f.To).Include(f => f.From).Where(f => f.From.airport == request.From
                                                                                            && f.To.airport == request.To
                                                                                            && f.DepartureTime.Contains(request.DepartureDate)).ToList();
                return Ok(new PageResult(0, result.Count(), result));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(flight => flight.Id == id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
    }
}
