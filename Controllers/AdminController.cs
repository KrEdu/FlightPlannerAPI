using FlightPlannerAPI.DbContext;
using FlightPlannerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightPlannerAPI.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly SemaphoreSlim Mutex = new SemaphoreSlim(1);

        public AdminController (FlightPlannerDbContext context)
        {
            _context = context;
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

        [HttpPut]
        [Route("flights")]
        public async Task <IActionResult> PutFlight(Flight flight)
        {
            if (FlightValidate.IsFlightViable(flight))
            {
                await Mutex.WaitAsync();
                try
                {
                    if (_context.Flights.Include(f => f.From).Include(f => f.To).ToList()   
                        .FirstOrDefault(f =>FlightValidate.AreFlightDuplicates(f, flight)) == null)
                    {
                        _context.Flights.Add(flight);
                        _context.SaveChanges();
                        return Created("", flight);
                    }
                    return Conflict();
                }
                finally
                {
                    Mutex.Release();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public async Task <IActionResult> DeleteFlight(int id)
        {
            await Mutex.WaitAsync();
            try
            {
                var flight = _context.Flights.Include(f => f.To).Include(f => f.From).SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Airports.Remove(flight.To);
                    _context.Airports.Remove(flight.From);
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }
                return Ok();
            }
            finally
            {
                Mutex.Release();
            }
        }
    }
}
