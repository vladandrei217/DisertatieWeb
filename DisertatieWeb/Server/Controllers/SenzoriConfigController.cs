using DisertatieWeb.Models.DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SenzoriConfigController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SenzoriConfigController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Senzor>>> Get()
        {
            var senzori = await _context.TrafficSensors
                .Select(s => new Senzor
                {
                    Id = s.Id,
                    Nume = s.Nume,
                    Status = s.Status,
                    Detalii = s.Detalii
                }).ToListAsync();

            return Ok(senzori);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Senzor model)
        {
            var senzor = await _context.TrafficSensors.FindAsync(id);
            if (senzor == null) return NotFound();

            senzor.Nume = model.Nume;
            senzor.Status = model.Status;
            senzor.Detalii = model.Detalii;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
