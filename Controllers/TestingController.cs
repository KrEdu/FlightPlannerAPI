using FlightPlannerAPI.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace FlightPlannerAPI.Controllers
{
    [AllowAnonymous]
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Flights, RESEED, 0)");//Resets id's to 0
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Airports, RESEED, 0)");
            _context.SaveChanges();
            return Ok(); 
        }
    }
}
