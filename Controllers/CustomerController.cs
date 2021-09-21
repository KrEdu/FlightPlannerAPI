using FlightPlannerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlannerAPI.Controllers
{
    [AllowAnonymous]
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]

        public IActionResult FindAirport(string search)
        {
            Airport [] airport = FlightStorage.GetAirportByPhrase(search);
            if (airport == null)
                return NotFound();
            else
                return Ok(airport);
        }

        [HttpPost]
        [Route("flights/search")]

        public IActionResult FindFlight(FlightRequest request)
        {
            if (!String.IsNullOrEmpty(request.From) && !String.IsNullOrEmpty(request.To) && !String.IsNullOrEmpty(request.DepartureDate)
                && request.From != request.To)
            {
                PageResult result = FlightStorage.GetFlightListByRequest(request);
                return Ok(result);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
    }
}
