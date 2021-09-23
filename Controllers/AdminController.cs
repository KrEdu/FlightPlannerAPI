﻿using FlightPlannerAPI.DbContext;
using FlightPlannerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerAPI.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
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

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            if (FlightValidate.IsFlightViable(flight))
            {
                if (!FlightStorage.IsFlightDuplicate(flight))
                {
                    FlightStorage.AddFlight(flight);
                    return Created("", flight);
                }
                else return Conflict();
            }
            else
                return BadRequest();
        }
        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}
