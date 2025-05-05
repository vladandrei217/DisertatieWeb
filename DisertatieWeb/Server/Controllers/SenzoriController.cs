using DisertatieWeb.Models;
using DisertatieWeb.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisertatieWeb.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SenzoriController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SenzoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SenzorTrafic>>> GetSenzori()
        {
            var senzori = await _context.TrafficSensors
                .Include(s => s.Comentarii)
                .OrderBy(s => s.Nume)
                .Select(s => new SenzorTrafic
                {
                    Id = s.Id,
                    Nume = s.Nume,
                    Latitudine = s.Latitudine,
                    Longitudine = s.Longitudine,
                    Status = s.Status,
                    Comentarii = s.Comentarii.Select(c => new ComentariuSenzor
                    {
                        Id = c.Id,
                        Autor = c.Autor,
                        Text = c.Text,
                        Data = c.Data
                    }).ToList()
                })
                .ToListAsync();

            return Ok(senzori);
        }
    }
}
