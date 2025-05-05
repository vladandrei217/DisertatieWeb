using DisertatieWeb.Models.DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SenzoriStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SenzoriStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Senzor>>> Get()
        {
            var result = await _context.TrafficSensors
                .Select(s => new Senzor
                {
                    Id = s.Id,
                    Nume = s.Nume,
                    Status = s.Status,
                    Detalii = s.Detalii
                }).ToListAsync();

            return Ok(result);
        }
    }
}
